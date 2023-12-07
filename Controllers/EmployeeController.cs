
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Employee.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : Controller
{

	[HttpGet]
	public IActionResult Index()
	{
		ViewData["Title"] = "Search";
		return View();
	}	

	[HttpGet("{id}")]
	public IActionResult Get(string id)
	{
		ViewData["Title"] = "Get";
		ViewData["id"] = id;
		return View("Get");
	}

	[HttpPost]
	public IActionResult Post()
	{
		ViewData["Title"] = "Post";
	
		return View("Post");
	}

	[HttpPut("{id}")]
	public IActionResult Put(string id)
	{
		ViewData["Title"] = "Put";
		ViewData["id"] = id;
		return View("Put");
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(string id)
	{
		ViewData["Title"] = "Delete";
		ViewData["id"] = id;
		return View("Delete");
	}
}