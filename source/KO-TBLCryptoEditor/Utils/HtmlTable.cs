using System;
using System.Text;

namespace KO.TBLCryptoEditor.Utils
{
    public class HtmlTable : IDisposable
    {
        private StringBuilder _data;
        private string _id;
        private string _class;
        private string _caption;

        public string Caption
        { 
            get => _caption;
            set => SetCaption(value);
        }

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


        public HtmlTable(StringBuilder sb, string caption = "", string id = "", string @class = "")
        {
            _data = sb;
            _caption = caption;
            _id = id;
            _class = @class;

            _data.Append($"<table id=\"{id}\" class=\"{@class}\">\n");
            _data.Append($"<caption>{caption}</caption>");
        }

        public HtmlTableRow AddRow()
        {
            return new HtmlTableRow(_data);
        }

        public HtmlTableRow AddHeaderRow()
        {
            return new HtmlTableRow(_data, true);
        }

        public void StartTableBody()
        {
            _data.Append("<tbody>");
        }

        public void EndTableBody()
        {
            _data.Append("</tbody>");
        }

        private void SetCaption(string caption)
        {
            _data.Replace($"<caption>{_caption}</caption>", $"<caption>{caption}</caption>");
        }

        private void SetId(string id)
        {
            _data.Replace($"<table id=\"{_id}\" class=\"{_class}\">", $"<table id=\"{id}\" class=\"{_class}\">");
        }

        private void SetClass(string @class)
        {
            _data.Replace($"<table id=\"{_id}\" class=\"{_class}\">", $"<table id=\"{_id}\" class=\"{@class}\">");
        }

        public void Dispose()
        {
            _data.Append("</table>");
        }
    }
}
