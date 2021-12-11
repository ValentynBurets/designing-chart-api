using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Model
{
    public class CreateAttemptViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Guid ExerciseId { get; set; }
        //public Guid StudentId { get; set; }
        public string Chart { get; set; }
    }
}
