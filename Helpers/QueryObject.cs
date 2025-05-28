using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stock_api.Helpers
{
    public class QueryObject
    {
        public string? Symbole { get; set; } = null;
        public string? CompanyName { get; set; } = null;

       public string? SortBy { get; set; }
        public bool IsDesending { get; set; } = false;
    }
}
