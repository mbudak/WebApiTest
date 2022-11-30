using AutoMapper;
using BusinessLayer.Model.Interfaces;
using DataAccessLayer.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using System.Security.Cryptography.X509Certificates;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var companies = await _companyRepository.GetAllAsync();
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            foreach(var mappedEmployee in mappedEmployees)
            {
                mappedEmployee.CompanyName = companies.FirstOrDefault(c => 
                    c.SiteId.Equals(mappedEmployee.SiteId)
                 && c.CompanyCode.Equals(mappedEmployee.CompanyCode)
                )?.CompanyName;
            }
            return mappedEmployees;
        }
        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string siteId, string companyCode, string employeeCode)
        {
            var employee = await _employeeRepository.GetByCodeAsync(siteId, companyCode, employeeCode);
            var company = await _companyRepository.GetByCodeAsync(siteId, companyCode);

            return employee != null ? _mapper.Map<Employee, EmployeeInfo>(employee, o =>
            {
                o.AfterMap((src, dest) => dest.CompanyName = company.CompanyName);
            }) : null;
        }
        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            return await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(siteId, companyCode, employeeCode);
        }

    }
}
