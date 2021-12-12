using Domain.Entity.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Model
{
    public class GetFullExerciseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string StatusType { get; set; }
        public string EtalonChart { get; set; }
        public double MaxMark { get; set; }
        public string CategoryName { get; set; }
    }
}
