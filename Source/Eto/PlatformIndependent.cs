using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Eto
{

	public static class PlatformIndependent
	{

		// Can't use RegexOptions.Compiled to speedup
		private static Regex EtoMnemonic = new Regex(@"(?<=([^_](?:[_]{2})*)|^)[_](?![_])");
		private static Regex PlatformMnemonic = new Regex(@"(?<=([^&](?:[&]{2})*)|^)[&](?![&])");

		public static string ToPlatformMnemonic(this string value)
		{
			if (value == null)
				return string.Empty;

			value = value.Replace("_", "__");

			Match match = PlatformMnemonic.Match(value);
			if (match.Success)
			{
				var sb = new StringBuilder(value);
				sb[match.Index] = '_';
				sb.Replace("&&", "&");
				return sb.ToString();
			}

			return value.Replace("&&", "&");
		}

		public static string ToEtoMnemonic(this string value)
		{
			if (value == null)
				return null;

			Match match = EtoMnemonic.Match(value);
			if (match.Success)
			{
				var sb = new StringBuilder(value);
				sb[match.Index] = '&';
				sb.Replace("__", "_");
				return sb.ToString();
			}

			return value.Replace("__", "_");
		}

	}

}
