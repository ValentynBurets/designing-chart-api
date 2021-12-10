using System;

namespace Business.Contract.Model
{
    public class AttemptResultReport
    {
        public Guid Id { get; set; }
        public double Mark { get; set; }
        public string TimeSpend { get; set; }
    }
}
