using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrimeiraAPI.Domain.Models.CompanyAggregate;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;

namespace Infraestrutura
{
    public class UserDbContext : DbContext
	{
		
		public DbSet<Employee> Employes { get; set; }
		public DbSet<Company> Companies { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer(
				 "Server=localhost,1433;Database=Employee;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate = True"
				);
		
	}
}