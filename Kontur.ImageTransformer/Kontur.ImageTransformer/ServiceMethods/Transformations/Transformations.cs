using System.Drawing;

namespace Kontur.ImageTransformer.ServiceMethods
{
	internal sealed class RotateClockwiseTransform : BaseTransform
	{
		public override string Name => "rotate-cw";

		public override Bitmap Transform(Bitmap source)
		{
			source.RotateFlip(RotateFlipType.Rotate90FlipNone);
			return source;
		}
	}
	internal sealed class RotateCounterClockwiseTransform : BaseTransform
	{
		public override string Name => "rotate-ccw";

		public override Bitmap Transform(Bitmap source)
		{
			source.RotateFlip(RotateFlipType.Rotate270FlipNone);
			return source;
		}
	}

	internal sealed class FlipHorizontallyTransform : BaseTransform
	{
		public override string Name => "flip-h";

		public override Bitmap Transform(Bitmap source)
		{
			source.RotateFlip(RotateFlipType.RotateNoneFlipX);
			return source;
		}
	}
	internal sealed class FlipVerticallyTransform : BaseTransform
	{
		public override string Name => "flip-v";

		public override Bitmap Transform(Bitmap source)
		{
			source.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return source;
		}
	}
}