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
        }

        public string FormatDate(DateTime? date)
        {
            return date?.ToShortDateString() ?? "-";
        }

        public string FormatPrice(decimal price)
        {
            return (price > 0) ? price.ToString("c", new CultureInfo("en-US")) : "Free";
        }

        public string FormatMaturity(bool isMature)
        {
            return isMature ? "Mature" : "Not mature";
        }
    }
}
