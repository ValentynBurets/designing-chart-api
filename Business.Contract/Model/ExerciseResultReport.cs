using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.Contract.Model
{
    public class ExerciseResultReport
    {
        public ExerciseResultReport()
        {
            Attempts = new HashSet<AttemptResultReport>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public double CoursePercentage { get; set; }
        public double MaxMark { get => Attempts.Max(a => a.Mark); }
        public double AverageMark { get => Attempts.Average(a => a.Mark); }
        public IEnumerable<AttemptResultReport> Attempts { get; set; }
    }
}
