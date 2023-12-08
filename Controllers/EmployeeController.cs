
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.Encodings.Web;
using Account.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeModel = Employees.Employee; 

namespace Employee.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [Route("/index")]
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
	[Route("/details/{id}")]
	public IActionResult Get(string id)
	{
		ViewData["Title"] = "Get";
		ViewData["id"] = id;
		return View("Get");
	}


	[HttpPost]
	[Route("/create")]
	public IActionResult Post()
	{
		ViewData["Title"] = "Post";

		return View("Post");
	}

	[HttpPut("{id}")]
    [Route("/update/{id}")]
    public IActionResult Put(string id)
	{
		ViewData["Title"] = "Put";
		ViewData["id"] = id;
		return View("Put");
	}

	[HttpDelete("{id}")]
    [Route("/delete/{id}")]
    public IActionResult Delete(string id)
	{
		ViewData["Title"] = "Delete";
		ViewData["id"] = id;
		return View("Delete");
	}
}