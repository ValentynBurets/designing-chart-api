using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.Contract.Model
{
    public class UserStatisticReport
    {
        public UserStatisticReport()
        {
            Exercises = new HashSet<ExerciseResultReport>();
        }

        public ProfileInfoModel User { get; set; }
        
        public double TotalProgress { get => Exercises.Sum(e => e.CoursePercentage); }
        
        public IEnumerable<ExerciseResultReport> Exercises { get; set; }
    }
}
