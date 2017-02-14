# Microsoft Azure Exam 70-534 Prep Course
## Application Insights Demonstration

### What's in this repository
This code can be used to deploy the resources used in the Application Insights demonstration that is part of the Microsoft Azure Exam 70-534 prep course on LinuxAcademy.com.

**Important note**: Azure services are not included in your LinuxAcademy.com subscription. If you would like to use this code in Azure, you will need your own Azure subscription and you may be billed for service directly by Microsoft. These charges will be separate from and in addition to your LinuxAcademy.com subscription.

The following folders are in this solution:
**la70534ai**: Contains an ASP.NET MVC application written in .NET 4.6. Note that this is _not_ a .NET Core application. To run this application you must use a Windows-based web host that supports the .NET 4.6 framework. To debug this site locally and run Application Insights telemetry on your development machine, you must use a Windows computer running Visual Studio 2015 Update 3 or later.

Also, you must change the ConnectionString constant in HomeController.cs to use your Azure SQL Database connection string.

```
public class HomeController : Controller
{
	private const string ConnString = "ENTER YOUR AZURE SQL DB CONNECTION STRING HERE";
	
	public ActionResult Index()
	{
		return View();
	}
```
**sql**: This folder contains Transact-SQL commands to replicate the table and stored procedures used in the demo.

**template**: This folder contains an ARM template and parameter file for deploying the Azure resources needed to replicate this demo.
