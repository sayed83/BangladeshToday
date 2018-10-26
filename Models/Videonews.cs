using System;
using System.Collections.Generic;

namespace BangladeshToday.Models
{
    public partial class Videonews
    {
        public Videonews()
        {
            Allvideo = new HashSet<Allvideo>();
        }

        public int VideoNewsId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Keyword { get; set; }
        public DateTime Datetime { get; set; }
        public string Path { get; set; }

        public ICollection<Allvideo> Allvideo { get; set; }
    }
}
