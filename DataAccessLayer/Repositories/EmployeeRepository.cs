using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string siteId, string companyCode, string employeeCode)
        {
            if (siteId.Trim() == "" )
            {
                throw new BusinessException(501, "siteId is mandatory");
            }
            if (companyCode.Trim() == "")
            {
                throw new BusinessException(501, "companyCode is mandatory");
            }
            if (employeeCode .Trim() == "")
            {
                throw new BusinessException(501, "employeeCode is mandatory");
            }


            var employee = await _employeeDbWrapper.FindAsync(o => 
                o.SiteId == siteId 
             && o.CompanyCode == companyCode 
             && o.EmployeeCode == employeeCode
            );
            return employee.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            // If record found Update it
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return _employeeDbWrapper.Update(itemRepo);
            }
            // If record not found Insert it.
            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string siteId, string companyCode, string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(o =>
                o.SiteId.Equals(siteId)
             && o.CompanyCode.Equals(companyCode)
             && o.EmployeeCode.Equals(employeeCode)
            );
        }
    }
}
