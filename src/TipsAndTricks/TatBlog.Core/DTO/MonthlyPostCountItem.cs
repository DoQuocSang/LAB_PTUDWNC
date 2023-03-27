using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;

namespace TatBlog.Core.DTO
{
    public class MonthlyPostCountItem
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int PostCount { get; set; }

        public string ConvertNumberToMonthName()
        {
            //return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(this.Month);
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.Month);
        }
    }
}
