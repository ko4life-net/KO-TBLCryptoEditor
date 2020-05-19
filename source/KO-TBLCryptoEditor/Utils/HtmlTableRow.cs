using System;
using System.Text;

namespace KO.TBLCryptoEditor.Utils
{
    public class HtmlTableRow : IDisposable
    {
        private StringBuilder _data;
        private string _id;
        private string _class;
        private bool _isHeader;

        public string Id
        {
            get => _id;
            set => SetId(value);
        }

        public string Class
        {
            get => _class;
            set => SetClass(value);
        }

        public HtmlTableRow(StringBuilder sb, bool isHeader = false, string id = "", string @class = "")
        {
            _data = sb;
            _id = id;
            _class = @class;

            _isHeader = isHeader;
            if (_isHeader)
            {
                _data.Append("<thead>\n");
            }
            _data.Append($"\t<tr id=\"{id}\" class=\"{@class}\">\n");
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

        private void SetId(string id)
        {
            _data.Replace($"<tr id=\"{_id}\" class=\"{_class}\">", $"<tr id=\"{id}\" class=\"{_class}\">");
        }

        private void SetClass(string @class)
        {
            _data.Replace($"<tr id=\"{_id}\" class=\"{_class}\">", $"<tr id=\"{_id}\" class=\"{@class}\">");
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
