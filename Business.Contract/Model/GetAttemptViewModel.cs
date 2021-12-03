using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Model
{
    public class GetAttemptViewModel
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public double Mark { get; set; }
        public double PerCent { get; set; }
        public virtual GetExerciseViewModel ExerciseInfo { get; set; }
        public string Chart { get; set; }
    }
}
