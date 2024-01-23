using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrimeiraAPI.Models;

namespace Infraestrutura
{
	public class UserDbContext : DbContext
	{
		private IConfiguration _configuration;

		public DbSet<Employee> Employes { get; set; }
		public UserDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); 
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var typeDatabase = _configuration["TypeDataBase"];
			var connectionString = _configuration.GetConnectionString(typeDatabase);

			if(typeDatabase == "SqlServer")
			{
				optionsBuilder.UseSqlServer(connectionString);
			}
		}
	}
}