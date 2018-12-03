using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Diagnostics.Runtime.Middleware
{
    internal static class TableBuilder
    {
        public static string CreateDataTable<T>(string title, IEnumerable<T> data)
        {
            bool createHeader = true;
            PropertyInfo[] properties = null;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"<h1>{title}</h1><br />");

            builder.AppendLine("<table><thead><tr>");
            foreach (var record in data)
            {
                if (createHeader)
                {
                    properties = record.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    foreach (var header in properties)
                    {
                        builder.Append($"<td><strong>{header.Name}</strong></td>");
                    }

                    builder.AppendLine("</thead></tr>");
                    createHeader = false;
                }


                builder.AppendLine("<tr>");
                foreach (var property in properties)
                {
                    object value = property.GetValue(record);
                    if (value is TableColumn)
                    {
                        builder.Append($"<td>{value.ToString()}</td>");
                    }
                    else
                    {
                        builder.Append($"<td>{value}</td>");
                    }
                }
                builder.AppendLine("</tr>");
            }

            builder.AppendLine("</table>");


            return builder.ToString();
        }
    }
}
