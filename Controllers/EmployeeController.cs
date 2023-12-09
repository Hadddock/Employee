
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
using System.Net;
using Employees;

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

	[Route("/")]
    public async Task<IActionResult> Index()
	{
        return RedirectToAction("Create");
    }

	[Route("employee/details/{id}")]
	public async Task<IActionResult> Details(string id)
	{
		if (id == null)
		{
			return NotFound();
		}

        try
        {
            var employees = await _collection.FindAsync(e => e.Id == id);
            return View(employees.FirstOrDefault());
        }

        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel { Ex = ex });
        }
	}

	[Route("employee/create")]
	public IActionResult Create()
	{
		return View();
	}

    [HttpPost, Route("employee/create"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] Employees.Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Create");
        }

        try
        {
            await _collection.InsertOneAsync(employee);
            return RedirectToAction("Details", new { id = employee.Id });
        }

        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel { Ex = ex });
        }
        
    }

    [Route("employee/edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var employee = await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
            return View(employee);
        }

        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel { Ex = ex });
        }
    }

    [HttpPost, Route("employee/edit/{id}"), ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [FromForm] Employees.Employee employee)
    {
        if(id == null || employee.Id == null)
        {
            return NotFound();
        }
      
        if (!ModelState.IsValid || id != employee.Id)
        {
            return RedirectToAction("Edit");
        }

        try
        {
            await _collection.ReplaceOneAsync((e => e.Id == employee.Id), employee);
            return RedirectToAction("Details", new { id = employee.Id });
        }

        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel { Ex = ex });
        }
  
    }

    [Route("employee/delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var employee = await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
            return View("Delete", employee);
        }

        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel { Ex = ex });
        }
    }


	[HttpPost, Route("employee/delete/{id}"), ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return View("Error", new Models.ErrorViewModel {Ex=ex});
        }
	}

}