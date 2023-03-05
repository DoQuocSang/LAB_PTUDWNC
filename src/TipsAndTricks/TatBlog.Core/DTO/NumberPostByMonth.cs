using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;

namespace TatBlog.Core.DTO
{
    public class NumberPostByMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int PostCount { get; set; }
    }
}
