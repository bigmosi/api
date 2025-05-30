using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int StockId { get; set; }

        // Navigation property
        // This property is used to establish a relationship with the Stock class
        public Stock? Stock { get; set; }
        
    }
}