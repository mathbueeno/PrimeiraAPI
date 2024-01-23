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
			
		}

		public List<Employee> Get()
		{
			throw new NotImplementedException();
		}
	}
}
