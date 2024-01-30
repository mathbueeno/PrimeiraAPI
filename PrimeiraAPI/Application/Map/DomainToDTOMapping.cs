using AutoMapper;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;

namespace PrimeiraAPI.Application.Map
{
    public class DomainToDTOMapping : Profile
	{
		public DomainToDTOMapping() 
		{
			CreateMap<Employee, EmployeeDTO>()
				.ForMember(dest => dest.NameEmployee, a => a.MapFrom(orig => orig.name));
		}
	}
}
