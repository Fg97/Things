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
	internal sealed class ProcessTransformations : IIdentifyUrl
	{
		enum QueryGroup { Transform, Coords }
		
		string regexTransform;
		Dictionary<string, BaseTransform> transformations;

		//config
		int maxContentLength;
		Size imageMaxSize;
		string imageFormat;

		public ProcessTransformations()
		{
			regexTransform = "";
			transformations = new Dictionary<string, BaseTransform>();

			UpdateMaxContentLength();
			UpdateImageMaxSize();
			UpdateImageFormat();
		}

		public void AddTransform(BaseTransform transform)
		{
			if(transform == null || transform.Name == null) throw new ArgumentNullException();
			if(transformations.ContainsKey(transform.Name)) throw new ArgumentException("Преобразование уже есть в списке");

			if(transformations.Count > 0)
				regexTransform += "|";

			regexTransform += transform.Name;

			transformations.Add(transform.Name, transform);
		}
		public void RemoveTransform(BaseTransform transform)
		{
			if(transformations.Count == 0) throw new ArgumentException("Список преобразований пуст");
			if(transform == null || transform.Name == null) throw new ArgumentNullException();
			if(!transformations.ContainsKey(transform.Name)) throw new ArgumentException("Преобразования нет в списке");

			int index = regexTransform.IndexOf(transform.Name, 0, StringComparison.Ordinal);
			int count = transform.Name.Length;

			if(transformations.Count > 1)
			{
				count++;
				if(index != 0) index--;
			}

			regexTransform = regexTransform.Remove(index, count);

			transformations.Remove(transform.Name);
		}

		public void UpdateMaxContentLength()
		{
			Interlocked.Exchange(ref maxContentLength,
				int.Parse(ConfigurationManager.AppSettings["ProcessTransformMaxContentLength"]));
		}
		public void UpdateImageMaxSize()
		{
			var size = ConfigurationManager.AppSettings["ProcessTransformImageMaxSize"].Split(';');
			imageMaxSize.Width = int.Parse(size[0]);
			imageMaxSize.Height = int.Parse(size[1]);
		}
		public void UpdateImageFormat()
		{
			imageFormat = ConfigurationManager.AppSettings["ProcessTransformImageFormat"];
		}

		void IIdentifyUrl.CorrectUrl(HttpListenerContext listenerContext, Match match, CancellationToken cancellationToken)
		{
			Image image = ImageMethods.LoadPngImage(listenerContext.Request.InputStream, listenerContext.Request.ContentLength64, true);
			
			cancellationToken.ThrowIfCancellationRequested();

			if(image != null &&
				image.Width * image.Height <= imageMaxSize.Width * imageMaxSize.Height)
			{
				using(var png = new Bitmap(image))
				{
					image.Dispose();
					
					var transform = transformations[match.Groups[QueryGroup.Transform.ToString()].Value];

					var coords = match.Groups[QueryGroup.Coords.ToString()].Value.Split(',');
					var bounds = new Rectangle
					{
						X = int.Parse(coords[0]),
						Y = int.Parse(coords[1]),
						Width = int.Parse(coords[2]),
						Height = int.Parse(coords[3])
					}; // RotatedBounds
					
					// Convert to Normal Bounds
					var normalBounds = transform.BoundsTransform(bounds, png.Size, true);
					normalBounds = png.Intersect(normalBounds);

					if(!normalBounds.IsEmpty) // !noContent
					{
						using(var clippedPng = new Bitmap(normalBounds.Width, normalBounds.Height, png.PixelFormat))
						{
							using(var graphics = Graphics.FromImage(clippedPng))
							{
								graphics.DrawImage(png, new Rectangle(Point.Empty, normalBounds.Size),
									normalBounds.X, normalBounds.Y, normalBounds.Width, normalBounds.Height, GraphicsUnit.Pixel);
								
								cancellationToken.ThrowIfCancellationRequested();

								using(var boundedPngInMemory = new MemoryStream())
								{
									transform.Transform(clippedPng).Save(boundedPngInMemory, ImageFormat.Png);
									
									listenerContext.Response.ContentLength64 = boundedPngInMemory.Length;

									boundedPngInMemory.Position = 0;
									boundedPngInMemory.CopyTo(listenerContext.Response.OutputStream, (int)boundedPngInMemory.Length);
								}

								listenerContext.Response.ContentType = "image/png";
								listenerContext.Response.StatusCode = (int)HttpStatusCode.OK;
							}
						}
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
					"(?<" + QueryGroup.Transform.ToString() + ">" + regexTransform + ")" +
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