using System;
using System.Drawing;

namespace Kontur.ImageTransformer.ServiceMethods
{
	static class ImageMethodsExtensions
	{
		public static Rectangle Intersect(this Image image, Rectangle bounds)
		{
			if(bounds.Width < 0)
			{
				bounds.X += bounds.Width;
				bounds.Width = -bounds.Width;
			}
			if(bounds.Height < 0)
			{
				bounds.Y += bounds.Height;
				bounds.Height = -bounds.Height;
			}

			int x1 = Math.Max(bounds.X, 0);
			int x2 = Math.Min(bounds.X + bounds.Width, image.Width);
			int y1 = Math.Max(bounds.Y, 0);
			int y2 = Math.Min(bounds.Y + bounds.Height, image.Height);

			if(x2 > x1 && y2 > y1)
				return new Rectangle(x1, y1, x2 - x1, y2 - y1);

			return Rectangle.Empty;
		}

		public static Rectangle BoundsTransform(this BaseTransform transform, Rectangle bounds, Size imageSize, bool transformToNormalForm)
		{
			int swap;

			if(transform is RotateClockwiseTransform)
			{
				if(transformToNormalForm) //0
				{
					swap = bounds.X;
					bounds.X = bounds.Y;
					bounds.Y = imageSize.Height - bounds.Width - swap;
				}
				else //+90
				{
					swap = bounds.Y;
					bounds.Y = bounds.X;
					bounds.X = imageSize.Height - bounds.Height - swap;
				}

				_BoundsTransform_Swap(ref bounds);
			}
			else if(transform is RotateCounterClockwiseTransform)
			{
				if(transformToNormalForm) //0
				{
					swap = bounds.Y;
					bounds.Y = bounds.X;
					bounds.X = imageSize.Width - bounds.Height - swap;
				}
				else //-90
				{
					swap = bounds.X;
					bounds.X = bounds.Y;
					bounds.Y = imageSize.Width - bounds.Width - swap;
				}

				_BoundsTransform_Swap(ref bounds);
			}
			else if(transform is FlipHorizontallyTransform)
			{
				bounds.X = imageSize.Width - bounds.Width - bounds.X;
			}
			else if(transform is FlipVerticallyTransform)
			{
				bounds.Y = imageSize.Height - bounds.Height - bounds.Y;
			}

			return bounds;
		}
		static void _BoundsTransform_Swap(ref Rectangle bounds)
		{
			int swap = bounds.Width;
			bounds.Width = bounds.Height;
			bounds.Height = swap;
		}
	}
}