using Microsoft.AspNetCore.Mvc;
using T02_WebAPIControllers.Models;

namespace T02_WebAPIControllers.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		[HttpPost]
		public string CreateShirt([FromBody] ShirtModel shirt)
		{
			return $"Create a shirt.";
		}

		[HttpGet]
		public string ReadAllShirts()
		{
			return $"Read all shirts.";
		}

		[HttpGet("{id}")]
		public string ReadShirtById(int id,
			[FromQuery] string primaryColor, [FromHeader] string secondaryColor)
		{
			return $"Read a shirt with Id: {id}, primary color: {primaryColor}, secondary color: {secondaryColor}.";
		}

		[HttpPut("{id}")]
		public string UpdateShirt(int id)
		{
			return $"Update a shirt with Id: {id}.";
		}

		[HttpDelete("{id}")]
		public string DeleteShirt(int id)
		{
			return $"Delete a shirt with Id: {id}.";
		}
	}
}
