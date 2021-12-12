using Newtonsoft.Json;
using System;

namespace Business.Contract.Model
{
    public class AttemptResultReport
    {
        public Guid Id { get; set; }
        public double Mark { get; set; }
        [JsonIgnore]
        public DateTime StartDate { get; set; }
        [JsonIgnore]
        public DateTime EndDate { get; set; }
        public string TimeSpend { get; set; }
    }
}
