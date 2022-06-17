using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication2.Models
{
    public partial class CTopic
    {
        public CTopic()
        {
            VLetters = new HashSet<VLetter>();
        }

        public int Id { get; set; }
        public string TopicName { get; set; }

        public virtual ICollection<VLetter> VLetters { get; set; }
    }
}
