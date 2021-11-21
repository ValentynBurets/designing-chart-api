using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Chart: EntityBase
    {
        public string Data { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
