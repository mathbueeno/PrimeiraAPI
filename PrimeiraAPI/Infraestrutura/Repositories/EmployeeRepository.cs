using Infraestrutura;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAPI.Infraestrutura.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserDbContext _context = new UserDbContext();
        public void Add(Employee employee)
        {
            _context.Employes.Add(employee);
            _context.SaveChanges();
        }

        public List<Employee> Get()
        {
            return _context.Employes.ToList();
        }

       
        public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.Employes.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(b => 
                new EmployeeDTO()
                {
                    Id = b.Id,
                    NameEmployee = b.name,
                    Photo = b.photo 
                }).ToList();
        }

		public Employee? Get(int id)
		{
			return _context.Employes.Find(id);
		}
	}
}
