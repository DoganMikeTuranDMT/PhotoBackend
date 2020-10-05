using System;
using System.Collections.Generic;

namespace PhotoSystem.Models
{
    public partial class FoClient
    {
        public FoClient()
        {
            EmpUser = new HashSet<EmpUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmpUser> EmpUser { get; set; }
    }
}
