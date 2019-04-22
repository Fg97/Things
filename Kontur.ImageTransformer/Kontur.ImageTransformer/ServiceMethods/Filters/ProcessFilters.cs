using System;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

namespace Kontur.ImageTransformer.ServiceMethods
{
	internal sealed class ProcessFilters : IIdentifyUrl
	{
		enum QueryGroup { Filter, Coords }
		
		string regexFilter;
		Dictionary<string, BaseFilter> filters;

		//config
		int maxContentLength;

		public ProcessFilters()
		{
			regexFilter = "";
			filters = new Dictionary<string, BaseFilter>();

			UpdateMaxContentLength();
		}

		public void AddFilter(BaseFilter filter)
		{
			if(filter == null || filter.Name == null || filter.Parameters == null) throw new ArgumentNullException();
			if(filters.ContainsKey(filter.Name)) throw new ArgumentException("Фильтр уже есть в списке");

			if(filters.Count > 0)
				regexFilter += "|";

			regexFilter += filter.Name;

			if(filter.Parameters.Length > 0)
				regexFilter += "\\(" + filter.Parameters + "\\)";

			filters.Add(filter.Name, filter);
		}
		public void RemoveFilter(BaseFilter filter)
		{
			if(filters.Count == 0) throw new ArgumentException("Список фильтров пуст");
			if(filter == null || filter.Name == null || filter.Parameters == null) throw new ArgumentNullException();
			if(!filters.ContainsKey(filter.Name)) throw new ArgumentException("Фильтра нет в списке");

			int index = regexFilter.IndexOf(filter.Name, 0, StringComparison.Ordinal);
			int count = filter.Name.Length;

			if(filter.Parameters.Length > 0)
				count += 2 + filter.Parameters.Length + 2;

			if(filters.Count > 1)
			{
				count++;
				if(index != 0) index--;
			}

			regexFilter = regexFilter.Remove(index, count);

			filters.Remove(filter.Name);
		}

		public void UpdateMaxContentLength()
		{
			Interlocked.Exchange(ref maxContentLength,
				int.Parse(ConfigurationManager.AppSettings["ProcessFilterMaxContentLength"]));
		}

		void IIdentifyUrl.CorrectUrl(HttpListenerContext listenerContext, Match match, CancellationToken cancellationToken)
		{
			Image image = ImageMethods.LoadPngImage(listenerContext.Request.InputStream, listenerContext.Request.ContentLength64, true);

			cancellationToken.ThrowIfCancellationRequested();

			if(image != null)
			{
				using(var png = new Bitmap(image))
				{
					image.Dispose();

					var coords = match.Groups[QueryGroup.Coords.ToString()].Value.Split(',');
					var bounds = new Rectangle
					{
						X = int.Parse(coords[0]),
						Y = int.Parse(coords[1]),
						Width = int.Parse(coords[2]),
						Height = int.Parse(coords[3])
					};

					var imageBounds = png.Intersect(bounds);
					if(!imageBounds.IsEmpty) // !noContent
					{
						var filterQueryGroup = match.Groups[QueryGroup.Filter.ToString()].Value.Split('(', ')');
						var filter = filters[filterQueryGroup[0]];

						string[] parameters = null;
						if(filter.Parameters.Length > 0)
							parameters = filterQueryGroup[1].Split(',');

						using(var boundedPng = new Bitmap(imageBounds.Width, imageBounds.Height, png.PixelFormat))
						{
							using(var graphics = Graphics.FromImage(boundedPng))
							{
								graphics.DrawImage(png, new Rectangle(Point.Empty, imageBounds.Size),
									imageBounds.X, imageBounds.Y, imageBounds.Width, imageBounds.Height,
									GraphicsUnit.Pixel, filter.Filter(parameters));

								cancellationToken.ThrowIfCancellationRequested();
							}
							using(var boundedPngInMemory = new MemoryStream())
							{
								boundedPng.Save(boundedPngInMemory, ImageFormat.Png);

								listenerContext.Response.ContentLength64 = boundedPngInMemory.Length;

								boundedPngInMemory.Position = 0;
								boundedPngInMemory.CopyTo(listenerContext.Response.OutputStream, (int)boundedPngInMemory.Length);
							}
						}

						listenerContext.Response.ContentType = "image/png";
						listenerContext.Response.StatusCode = (int)HttpStatusCode.OK;
					}
					else // noContent
						listenerContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
				}
			}
			else listenerContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
		}

		Regex IIdentifyUrl.RegexForParseUrl
		{
			get
			{
				return new Regex(
					"^/" +
					"process" +
					"/" +
					"(?<" + QueryGroup.Filter.ToString() + ">" + regexFilter + ")" +
					"/" +
					"(?<" + QueryGroup.Coords.ToString() + @">(?:[-+]?\d+,){3}[-+]?\d+)" +
					"$");
			}
		}

		bool IIdentifyUrl.IsCorrectRequest(HttpListenerRequest listenerRequest)
		{
			return listenerRequest.HttpMethod == "POST" && listenerRequest.ContentLength64 <= maxContentLength;
		}
	}
}