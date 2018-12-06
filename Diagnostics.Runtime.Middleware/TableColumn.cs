namespace Diagnostics.Runtime.Middleware
{

    internal class TableColumn
    {
        private readonly object _item;
        private string _format;

        private string _link;

        public TableColumn(object item)
        {
            _item = item;
        }

        public TableColumn Link(string link)
        {
            _link = link;
            return this;
        }

        public TableColumn Format(string format)
        {
            _format = format;
            return this;
        }

        public override string ToString()
        {
            string value = _item.ToString();

            if (!string.IsNullOrWhiteSpace(_format))
            {
                value = string.Format(_format, _item);
            }

            if (!string.IsNullOrWhiteSpace(_link))
            {
                value = $"<a href='{_link}'>{value}</a>";
            }

            return value;
        }

        public static TableColumn Wrap(object item)
        {
            return new TableColumn(item);
        }
    }
}
