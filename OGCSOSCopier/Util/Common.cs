using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGCSOSCopier.Util
{
	public class Common
	{

		public static SOSVerisonComboboxItem[] SOSVerisonComboboxItemGenerator()
		{
			List<SOSVerisonComboboxItem> list = new List<SOSVerisonComboboxItem>();
			foreach (var item in Constants.SOS_VERSIONS)
			{
				list.Add(new SOSVerisonComboboxItem(item, item));
			}

			return list.ToArray();
		}

		public static bool IsValidUrl(string url)
		{
			Uri uriResult;
			bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			return result;
		}

	}
}
