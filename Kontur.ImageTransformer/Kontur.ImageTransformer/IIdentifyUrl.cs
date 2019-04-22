using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace Kontur.ImageTransformer
{
	public interface IIdentifyUrl
	{
		Regex RegexForParseUrl { get; }
		bool IsCorrectRequest(HttpListenerRequest listenerRequest);
		void CorrectUrl(HttpListenerContext listenerContext, Match match, CancellationToken cancellationToken);
	}
}