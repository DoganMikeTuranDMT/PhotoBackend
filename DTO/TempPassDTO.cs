using System;
using System.Collections.Generic;
using PhotoSystem.Models;

namespace PhotoSystem.DTO
{
    public class TempPassDTO
    {
        public TempPassDTO()
        {
            ImageTempPass = new HashSet<ImageTempPass>();
            
        }

        public string ImageLink { get; set; }
        public string UserCompanyName { get; set; }
        public string OriginalFileName { get; set; }
        public int TempPasswordId { get; set; }


        public virtual TempPass TempPassword { get; set; }
        public virtual ICollection<ImageTempPass> ImageTempPass { get; set; }
    }
}
