using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ILoggerRepository
    {
        Task<IEnumerable<LogEntity>> GetAllAsync();
        Task<bool> SaveLogAsync(LogEntity log);
        Task<bool> ClearAllLogAsync();
    }
}
