using System.Drawing.Imaging;

namespace Kontur.ImageTransformer.ServiceMethods
{
	public abstract class BaseFilter
	{
		public abstract string Name { get; }
		public virtual string Parameters => "";

		public abstract ImageAttributes Filter(string[] filterParams);
	}
}