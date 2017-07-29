/*
* DDDN.Localization.OpenXML.OpenXMLStringResource
* 
* Copyright(C) 2017 Lukasz Jaskiewicz
* Author: Lukasz Jaskiewicz (lukasz@jaskiewicz.de)
*
* This program is free software; you can redistribute it and/or modify it under the terms of the
* GNU General Public License as published by the Free Software Foundation; version 2 of the License.
*
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
* warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License along with this program; if not, write
* to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using DDDN.Logging.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DDDN.Localization.OpenXML
{
	public class OpenXMLStringResource
	{
		private CultureInfo ResourceCulturInfo;
		private bool FallBackToNaturalCulture;

		public OpenXMLStringResource(string filePathAndName, string cultureName, bool fallBackToNaturalCulture)
		{
			if (string.IsNullOrWhiteSpace(filePathAndName))
			{
				throw new ArgumentException(LogMsg.StrArgNullOrWhite, nameof(filePathAndName));
			}

			if (string.IsNullOrWhiteSpace(cultureName))
			{
				throw new ArgumentException(LogMsg.StrArgNullOrWhite, nameof(cultureName));
			}

			ResourceCulturInfo = new CultureInfo(cultureName);
			FallBackToNaturalCulture = fallBackToNaturalCulture;
		}

		public Dictionary<string, string> GetStrings()
		{
			var ret = new Dictionary<string, string>();

			return ret;
		}
	}
}
