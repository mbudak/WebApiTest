using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly ILogDbWrapper<LogEntity> _logDbWrapper;

        public LoggerRepository(ILogDbWrapper<LogEntity> logDbWrapper)
        {
            _logDbWrapper = logDbWrapper;
        }
        public async Task<IEnumerable<LogEntity>> GetAllAsync()
        {
            return await _logDbWrapper.FindAllAsync();
        }

        public async Task<bool> ClearAllLogAsync()
        {
            return await _logDbWrapper.DeleteAllAsync();
        }

       
        public async Task<bool> SaveLogAsync(LogEntity log)
        {
            log.CreatedAt = DateTime.Now;
            return await _logDbWrapper.InsertAsync(log);
        }
    }
}
