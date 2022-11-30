using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        public async Task<EmployeeDto> Get(string siteId, string companyCode, string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(siteId, companyCode, employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        public async Task<bool> Post([FromBody]EmployeeDto employeeDto)
        {
            var item = _mapper.Map<EmployeeInfo>(employeeDto);
            return await _employeeService.SaveEmployeeAsync(item);
        }



    }
}
