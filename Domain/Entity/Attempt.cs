using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Attempt: EntityBase
    {
        public DateTime StartTime { get; set;}
        public DateTime FinishTime { get; set; }
        public double Mark { get; set; }
        public virtual Exercise Exercise { get; set; }
        public Guid ExerciseId { get; set; }
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        public string Chart { get; set; }
    }
}
