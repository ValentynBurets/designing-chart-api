using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class CategoryType: EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
