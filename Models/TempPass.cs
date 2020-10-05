using System;
using System.Collections.Generic;

namespace PhotoSystem.Models
{
    public partial class TempPass
    {
        public TempPass()
        {
            ImageTempPass = new HashSet<ImageTempPass>();
        }

        public int Id { get; set; }
        public string TempPassword { get; set; }
        public string UserCompanyName { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }

        public virtual ICollection<ImageTempPass> ImageTempPass { get; set; }
    }
}
