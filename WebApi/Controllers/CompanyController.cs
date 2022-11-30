using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string siteId, string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(siteId, companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody]CompanyDto companyDto)
        {
            var item = _mapper.Map<CompanyInfo>(companyDto);
            return await _companyService.SaveCompanyAsync(item);
        }

        // We don't need id for put since we already have it in companyDto
        // PUT api/<controller>
        public async Task<bool> Put([FromBody]CompanyDto companyDto)
        {
            var item = _mapper.Map<CompanyInfo>(companyDto);
            return await _companyService.SaveCompanyAsync(item);
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string siteId, string companyCode)
        {
            return await _companyService.DeleteCompanyAsync(siteId, companyCode);
        }
    }
}