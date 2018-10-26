using System;
using System.Collections.Generic;

namespace BangladeshToday.Models
{
    public partial class Newsinfo
    {
        public int Newsserial { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Datetime { get; set; }
        public string Keyword { get; set; }
    }
}
