using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Student: User
    {
        public Student()
        {
            Attempts = new HashSet<Attempt>();
        }
        
        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
