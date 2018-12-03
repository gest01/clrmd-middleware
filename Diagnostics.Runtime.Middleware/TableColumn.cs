namespace Diagnostics.Runtime.Middleware
{
    internal class TableColumn
    {
        private readonly object _item;
        private string _format;

        public TableColumn(object item)
        {
            _item = item;
        }

        public TableColumn Format(string format)
        {
            _format = format;
            return this;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(_format))
            {
                return _item.ToString();
            }
            else
            {
                return string.Format(_format, _item);
            }
        }

        public static TableColumn Wrap(object item)
        {
            return new TableColumn(item);
        }
    }
}
