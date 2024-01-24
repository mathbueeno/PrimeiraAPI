using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers
{
	[ApiController]
	[Route("api/v1/employee")]
	public class EmployeeController : ControllerBase
	{

		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
		}

		
		[HttpPost]
		public IActionResult Add([FromForm] EmployeeViewModel employeeView)
		{
			var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

			using Stream fileStream = new FileStream(filePath, FileMode.Create);
			employeeView.Photo.CopyTo(fileStream);
			var employee = new Employee(employeeView.Name, employeeView.Age, filePath);
			
			_employeeRepository.Add(employee);	
			return Ok();
		}


		[HttpGet]
		public IActionResult Get()
		{
			var employee = _employeeRepository.Get();
			return Ok(employee);

		}
	}
}
