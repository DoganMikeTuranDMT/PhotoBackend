using System;
using System.Collections.Generic;

namespace PhotoSystem.Models
{
    public partial class ImageTempPass
    {
        public string ImageLink { get; set; }
        public string OriginalFileName { get; set; }
        public int TempPasswordId { get; set; }

        public virtual TempPass TempPassword { get; set; }
    }
}
