﻿
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
    public async Task<IActionResult> Create([FromForm] Employees.Employee employee)
    {
        if (ModelState.IsValid)
        {
		
			try
			{
                await _collection.InsertOneAsync(employee);
            }

			catch (Exception ex)
			{
				Console.WriteLine(ex);
                return RedirectToAction("Create");
            }
        }

		else
		{
            return RedirectToAction("Create");
        }

		return RedirectToAction("Details", new {id = employee.Id });
    }

    [Route("employee/edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost]
    [Route("employee/edit/{id}")]
    public async Task<IActionResult> Edit(string id, [FromForm] Employees.Employee employee)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _collection.ReplaceOneAsync((e => e.Id == employee.Id), employee);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Edit");
            }
        }

        else
        {
            return RedirectToAction("Edit");
        }

        return RedirectToAction("Details", new { id = employee.Id });
    }

    [Route("employee/delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        
        if (employee == null)
        {
            return NotFound();
        }

        return View("Delete",employee);
    }


	[HttpPost, Route("employee/delete/{id}")]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
        if (id == null)
        {
            return NotFound();
        }

        await _collection.DeleteOneAsync(e => e.Id == id);
		return RedirectToAction(nameof(Index));
	}

}