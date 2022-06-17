using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication2.Models
{
    public partial class CAddressee
    {
        public CAddressee()
        {
            VLetterCAddressee1s = new HashSet<VLetter>();
            VLetterCAddressee2s = new HashSet<VLetter>();
        }

        public int Id { get; set; }
        public string Passport { get; set; }
        public string AddresseeName { get; set; }

        public virtual ICollection<VLetter> VLetterCAddressee1s { get; set; }
        public virtual ICollection<VLetter> VLetterCAddressee2s { get; set; }
    }
}
