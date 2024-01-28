using PrimeiraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura
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

		public Employee? Get(int id)
		{
			return _context.Employes.Find(id);
		}
	}
}
