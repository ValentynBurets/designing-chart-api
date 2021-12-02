using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Student: User
    {
        public Student(Guid idLink) : base(idLink)
        {
            Attempts = new HashSet<Attempt>();
        }
        
        public virtual ICollection<Attempt> Attempts { get; set; }
    }
}
