using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Application.ViewModel;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;

namespace PrimeiraAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/employee")]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mapper = mapper;
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

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, "image/png");
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


        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {

            var employee = _employeeRepository.Get(id);

            var employeesDtos = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeesDtos);
        }



    }
}
