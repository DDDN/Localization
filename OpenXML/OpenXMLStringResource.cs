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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DDDN.Localization.OpenXML
{
    public class OpenXMLStringResource : IOpenXMLStringResource
    {
        private string ResourceKey;
        private string FilePath;

        public OpenXMLStringResource(string resourceKey, string resourceFolderPath)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                throw new ArgumentException(LogMsg.StrArgNullOrWhite, nameof(resourceKey));
            }

            if (string.IsNullOrWhiteSpace(resourceFolderPath))
            {
                throw new ArgumentException(LogMsg.StrArgNullOrWhite, nameof(resourceFolderPath));
            }

            ResourceKey = resourceKey;
            FilePath = resourceFolderPath;
        }

        public Dictionary<string, string> GetStrings()
        {
            var ret = new Dictionary<string, string>();
            var resourcefileFullPaths = Directory.GetFiles(FilePath, $"{ResourceKey}.*");

            foreach (var fileFullPath in resourcefileFullPaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(fileFullPath);
                var cultureNameFromFileName = fileName.Replace($"{ResourceKey}.", "");

                using (WordprocessingDocument openXMLDoc = WordprocessingDocument.Open(fileFullPath, false))
                {
                    var firstTable = openXMLDoc.MainDocumentPart.Document.Body.Elements<Table>().First();

                    foreach (var row in firstTable.Elements<TableRow>().Skip(1))
                    {
                        var translationKey = row.Elements<TableCell>().First().InnerText;
                        var translation = row.Elements<TableCell>().Skip(1).First().InnerText;
                        ret.Add($"{translationKey}.{cultureNameFromFileName}", translation);
                    }
                }
            }

            return ret;
        }
    }
}
