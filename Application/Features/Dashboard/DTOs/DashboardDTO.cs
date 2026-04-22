using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.DTOs
{
    public class DashboardYearDTO
    {
        public int Year { get; set; }
        public List<DashboardGroupDTO> Groups { get; set; }
    }

    public class DashboardGroupDTO
    {
        public string Name { get; set; } // Income, Expenses, Investment
        public List<DashboardCategoryDTO> Categories { get; set; }
    }

    public class DashboardCategoryDTO
    {
        public string Name { get; set; } // salario, IR, taxi
        public List<MonthlyValueDTO> Values { get; set; }
    }

    public class MonthlyValueDTO
    {
        public int Month { get; set; } // 1-12
        public decimal Amount { get; set; }
    }
}
