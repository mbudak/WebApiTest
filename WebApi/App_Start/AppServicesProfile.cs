using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
            
            // EmployeeInfo and Dto have different member names
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(dest => dest.OccupationName, call => call.MapFrom(prop => prop.Occupation))
                .ForMember(dest => dest.PhoneNumber, call => call.MapFrom(prop => prop.Phone))
                .ForMember(dest => dest.LastModifiedDateTime, call => call.MapFrom(prop => prop.LastModified));
           
            
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(dest => dest.Occupation, call => call.MapFrom(prop => prop.OccupationName))
                .ForMember(dest => dest.Phone, call => call.MapFrom(prop => prop.PhoneNumber))
                .ForMember(dest => dest.LastModified, call => call.MapFrom(prop => prop.LastModifiedDateTime));

            CreateMap<Employee, EmployeeInfo>();
        }
    }
}