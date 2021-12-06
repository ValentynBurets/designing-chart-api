using System.Linq;
using System.Collections.Generic;


namespace Business.Contract.Model
{
    public class StatisticReportModel
    {
        public IEnumerable<GetAttemptViewModel> Attempts { get; set; }
        public double MaxMark { get => Attempts.Max(a => a.Mark); }
        public double AverageMark { get => Attempts.Average(a => a.Mark); }
        public double ActualMark { get => MaxMark * Attempts.First().PerCent; }
        public double CoursePercentage { get; set; }
    }
}