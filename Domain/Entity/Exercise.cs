using Domain.Entity.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Exercise : EntityBase
    {
        public Exercise()
        {
            Attempts = new HashSet<Attempt>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxMark { get; set; }
        public DateTime ExpirationDate { get; set; }
        public StatusType StatusType { get; set; }
        public Guid CategoryId { get; set; }
        public string EtalonChart { get; set; }
        public virtual CategoryType CategoryType { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
