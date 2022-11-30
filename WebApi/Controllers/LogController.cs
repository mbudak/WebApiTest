using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILoggerRepository _logger;
        public LogController(ILoggerRepository logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<LogEntity>> GetAll()
        {
            return await _logger.GetAllAsync();
        }

    }
}
