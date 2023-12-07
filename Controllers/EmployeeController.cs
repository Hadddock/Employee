using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Employee.Controllers;

public class EmployeeController : Controller
{
	// 
	// GET: /Employee/
	public string Index()
	{
		return "This is my default action...";
	}	
	// 
	// GET: /Employee/Detail/ 
	public string Detail(string ID)
	{
		return HtmlEncoder.Default.Encode($"Employee ID is: {ID}");

	}
}