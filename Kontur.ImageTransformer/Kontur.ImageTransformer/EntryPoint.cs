#define FIRST1

using System;
using System.Configuration;

using Kontur.ImageTransformer.ServiceMethods;

namespace Kontur.ImageTransformer
{
	public class EntryPoint
	{
		public static void Main(string[] args)
		{
			using(var server = new HttpServer(Environment.ProcessorCount))
			{
				#if FIRST
				var processFilter = new ProcessFilters();
				processFilter.AddFilter(new GrayscaleFilter());
				processFilter.AddFilter(new SepiaFilter());
				processFilter.AddFilter(new ThresholdFilter());

				server.AddContextHandler(processFilter);
				#endif

				var processTransform = new ProcessTransformations();
				processTransform.AddTransform(new RotateClockwiseTransform());
				processTransform.AddTransform(new RotateCounterClockwiseTransform());
				processTransform.AddTransform(new FlipHorizontallyTransform());
				processTransform.AddTransform(new FlipVerticallyTransform());

				server.AddContextHandler(processTransform);

				server.Start(ConfigurationManager.AppSettings["HttpServerPrefix"]);

				Console.ReadKey(true);
			}
		}
	}
}