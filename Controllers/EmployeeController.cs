
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeModel = Employees.Employee; 

namespace Employee.Controllers;

[ApiController]
public class EmployeeController : Controller

{
	private readonly IMongoDatabase _database;
	private readonly IMongoCollection<EmployeeModel> _collection;

	public EmployeeController(IMongoClient client)
	{
		_database = client.GetDatabase("employee");
		_collection = _database.GetCollection<EmployeeModel>("employees");
	}

	[HttpGet]
    [Route("/")]
    public IActionResult Index()
	{
		var newEmployee = new EmployeeModel
		{
			FirstName = "Jeff",
			LastName = "Reff",
			Salary = 45.50m,
			DateOfBirth = new DateTime(1990, 12, 1),
			HireDate = new DateTime(2019, 6, 1),
			Phone = "4655552348",
			Email = "Renchaw@fmail.com",
			IsAdministrator = false,
	};
		//_collection.InsertOne(newEmployee);
		Console.WriteLine(_collection);
		ViewData["Title"] = "Search";
		return View();
	}

	[HttpGet]
	[Route("employee/details/{id}")]
	public async Task<IActionResult> Details(string id)
	{
		if (id == null)
		{
			return NotFound();
            
		}
		var employees = await _collection.FindAsync(e => e.Id == id);
		var employee = employees.FirstOrDefault();

		if (employee == null)
		{
			return NotFound();
		}

		Console.WriteLine(employee);
		ViewData["Title"] = "Details";
		return View(employee);
	}


	[HttpGet]
	[Route("employee/create")]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[Route("employee/create")]
	public IActionResult Post()
	{
		ViewData["Title"] = "Post";

		return View("Post");
	}

	[HttpPut("{id}")]
    [Route("employee/update/{id}")]
    public IActionResult Put(string id)
	{
		ViewData["Title"] = "Put";
		ViewData["id"] = id;
		return View("Put");
	}

	[HttpDelete("{id}")]
    [Route("employee/delete/{id}")]
    public IActionResult Delete(string id)
	{
		ViewData["Title"] = "Delete";
		ViewData["id"] = id;
		return View("Delete");
	}
}