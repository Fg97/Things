using System.Drawing;

namespace Kontur.ImageTransformer.ServiceMethods
{
	public abstract class BaseTransform
	{
		public abstract string Name { get; }

		public abstract Bitmap Transform(Bitmap source);
	}
}