using Demo.Services;
using Demo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpGet]
    [Route("/list")]
    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _employeeService.GetEmployees();
    }


    [HttpGet]
    [Route("/get/{id}")]
    public async Task<Employee?> GetEmployeeById(int id)
    {
        return await _employeeService.GetById(id);
    }


    [HttpPost]
    [Route("/add")]
    public async Task<int> AddEmployee(Employee employee)
    {
        return await _employeeService.AddEmployee(employee);
    }
}