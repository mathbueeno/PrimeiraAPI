using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeiraAPI.Models
{
	[Table("employee")]
	public class Employee
	{
		

		[Key]
		public int Id { get; private set; }
		public string name { get; private set; }
		public int age { get; private set; }
		public string photo { get; private set; }


		public Employee(int id, string name, int age, string photo)
		{
			
			this.name = name ?? throw new ArgumentNullException(nameof(name));
			this.age = age;
			this.photo = photo;
		}
	}
}
