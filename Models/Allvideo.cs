using System;
using System.Collections.Generic;

namespace BangladeshToday.Models
{
    public partial class Allvideo
    {
        public int VideoId { get; set; }
        public string VideoSerial { get; set; }
        public string VideoPath { get; set; }

        public Videonews Video { get; set; }
    }
}
