using Microsoft.AspNetCore.Authorization;
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
		private readonly ILogger<EmployeeController> _logger;


		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
		}

		[Authorize]
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
		public IActionResult Get(int pageNumber, int pageQuantity)
		{
			//string mensagem = "Teve um erro";
			_logger?.Log(LogLevel.Error, "Teve um erro");

			var employee = _employeeRepository.Get(pageNumber, pageQuantity);

			_logger?.LogInformation("Teve um erro");

			return Ok(employee);
		}

		[Authorize]
		[HttpPost]
		[Route("{id}/download")]
		public IActionResult DownloadPhoto(int id)
		{
			var employee = _employeeRepository.Get(id);
			var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

			return File(dataBytes, "image/png");
		}
	}
}
