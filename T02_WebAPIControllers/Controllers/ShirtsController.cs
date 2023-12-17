using Microsoft.AspNetCore.Mvc;
using T02_WebAPIControllers.Models;
using T02_WebAPIControllers.Models.Repositories;

namespace T02_WebAPIControllers.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		[HttpPost]
		public IActionResult CreateShirt([FromBody] ShirtModel shirt)
		{
			return Ok($"Create a shirt.");
		}

		[HttpGet]
		public IActionResult ReadAllShirts()
		{
			return Ok($"Read all shirts.");
		}

		[HttpGet("{id}")]
		public IActionResult ReadShirtById(int id)
		{
			if (id < 1)
			{
				return BadRequest();
			}

			var shirt = ShirtRepository.GetShirtById(id);

			if (shirt == null)
			{
				return NotFound();
			}

			return Ok(shirt);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateShirt(int id)
		{
			return Ok($"Update a shirt with Id: {id}.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteShirt(int id)
		{
			return Ok($"Delete a shirt with Id: {id}.");
		}
	}
}
