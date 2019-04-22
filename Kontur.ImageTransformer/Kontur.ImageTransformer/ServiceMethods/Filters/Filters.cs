using System.Drawing.Imaging;

namespace Kontur.ImageTransformer.ServiceMethods
{
	internal sealed class GrayscaleFilter : BaseFilter
	{
		public override string Name => "grayscale";

		public override ImageAttributes Filter(string[] filterParams)
		{
			const float intensity = 1f / 3;

			var attr = new ImageAttributes();

			attr.SetColorMatrix(
				new ColorMatrix(new float[5][]
				{
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { 0, 0, 0, 1, 0 },
					new float[5] { 0, 0, 0, 0, 1 }
				}));

			return attr;
		}
	}
	internal sealed class SepiaFilter : BaseFilter
	{
		public override string Name => "sepia";

		public override ImageAttributes Filter(string[] filterParams)
		{
			var attr = new ImageAttributes();

			attr.SetColorMatrix(
				new ColorMatrix(new float[5][]
				{
					new float[5] { .393f, .349f, .272f, 0, 0 },
					new float[5] { .769f, .686f, .534f, 0, 0 },
					new float[5] { .189f, .168f, .131f, 0, 0 },
					new float[5] { 0, 0, 0, 1, 0 },
					new float[5] { 0, 0, 0, 0, 1 }
				}));

			return attr;
		}
	}
	internal sealed class ThresholdFilter : BaseFilter
	{
		public override string Name => "threshold";
		public override string Parameters => @"(?:100|\d\d?)";

		public override ImageAttributes Filter(string[] filterParams)
		{
			const float intensity = 1f / 3;
			int param = int.Parse(filterParams[0]);

			var attr = new ImageAttributes();
			
			attr.SetColorMatrix(
				new ColorMatrix(new float[5][]
				{
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { intensity, intensity, intensity, 0, 0 },
					new float[5] { 0, 0, 0, 1, 0 },
					new float[5] { 0, 0, 0, 0, 1 }
				}));
			attr.SetThreshold(param / 100f);
			
			return attr;
		}
	}
}