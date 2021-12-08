﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contract.Model;

namespace Business.Contract.Services
{
    public interface IStatisticService
    {
        Task<IEnumerable<StatisticReportModel>> GetStatistics(Guid? studentId = null, DateTime? startDate = null, DateTime? endDate = null, string category = null);
        Task<IEnumerable<StatisticReportModel>> GetStatistics(string studentName = null, DateTime? startDate = null, DateTime? endDate = null, string category = null);
    }
}
