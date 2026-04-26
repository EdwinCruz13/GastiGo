using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Public.Interfaces
{
    public interface IBalanceServicecs
    {
        public Task RecalculateBalanceAsync(Guid userId);
    }
}
