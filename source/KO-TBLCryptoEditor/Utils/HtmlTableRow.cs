using System;
using System.Text;

namespace KO.TBLCryptoEditor.Utils
{
    public class HtmlTableRow : IDisposable
    {
        private StringBuilder _data;
        private bool _isHeader;

        public HtmlTableRow(StringBuilder sb, bool isHeader = false)
        {
            _data = sb;
            _isHeader = isHeader;
            if (_isHeader)
            {
                _data.Append("<thead>\n");
            }
            _data.Append("\t<tr>\n");
        }

        public void AddCell(string innerText)
        {
            if (_isHeader)
            {
                _data.Append("\t\t<th>\n");
                _data.Append("\t\t\t" + innerText);
                _data.Append("\t\t</th>\n");
            }
            else
            {
                _data.Append("\t\t<td>\n");
                _data.Append("\t\t\t" + innerText);
                _data.Append("\t\t</td>\n");
            }
        }

        public void Dispose()
        {
            _data.Append("\t</tr>\n");
            if (_isHeader)
            {
                _data.Append("</thead>\n");
            }
        }
    }
}
