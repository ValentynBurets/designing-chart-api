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
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxMark { get; set; }
        public DateTime ExpirationDate { get; set; }
        public StatusType StatusType { get; set; }
        public virtual CategoryType CategoryType { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Chart EtalonChart { get; set; }
        public Guid EtalonChartId {get; set;}
        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
