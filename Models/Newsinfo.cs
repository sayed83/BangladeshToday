using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangladeshToday.Models
{
    public partial class Newsinfo
    {
        [Display(Name = "News Serial")]
        public int Newsserial { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }

        [Display(Name ="Key Word")]
        public string Keyword { get; set; }

        public string CaptionPicture { get; set; }
        public string Editor { get; set; }
        public string FeatureNews { get; set; }
    }
}
