using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Services
{
    public class FormattingService
    {
        public FormattingService()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        public string FormatDate(DateTime? date)
        {
            return date?.ToString("D") ?? "-";
        }

        public string FormatPrice(decimal price)
        {
            return (price > 0) ? price.ToString("c") : "Free";
        }
    }
}
