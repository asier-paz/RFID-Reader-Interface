using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronomur_WRI
{
	class Util
	{
		public static bool IsCorrenctIP(string ip)
		{
			string pattrn = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
			if (System.Text.RegularExpressions.Regex.IsMatch(ip.Trim(), pattrn))
			{
				return true;
			}
			else
			{
				return false;

			}
		}

		public static bool IsCorrectPortNumber(string port)
		{
			int _point;
			try
			{
				_point = int.Parse(port);
				if (_point > 65535 || _point < 1)
				{
					return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
