using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication2.Models
{
    public partial class VLetter
    {
        public int Id { get; set; }
        public DateTime ReceiptTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Answer { get; set; }
        public int? CTopicId { get; set; }
        public int? CAddressee1Id { get; set; }
        public int? CAddressee2Id { get; set; }

        public virtual CAddressee CAddressee1 { get; set; }
        public virtual CAddressee CAddressee2 { get; set; }
        public virtual CTopic CTopic { get; set; }
    }
}
