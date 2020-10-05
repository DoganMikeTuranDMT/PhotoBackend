using System;
using System.Collections.Generic;
using PhotoSystem.Models;

namespace PhotoSystem.DTO
{
    public class ImageTempPassDTO
       
    {
        public string ImageLink { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string OriginalFileName { get; set; }
        public string UserCompanyName { get; set; }
        public int TempPasswordId { get; set; }
    }
}
