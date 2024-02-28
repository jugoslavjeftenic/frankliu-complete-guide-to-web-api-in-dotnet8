using System.ComponentModel.DataAnnotations;
using T008_WebApiControllers.Models.Validations;

namespace T008_WebApiControllers.Models
{
	public class ShirtModel
	{
		public int ShirtId { get; set; }

		[Required]
		public string Brand { get; set; } = "";

		[Required]
		public string Color { get; set; } = "";

		[Shirt_EnsureCorrectSizing]
		public int? Size { get; set; }

		[Required]
		public string Gender { get; set; } = "";

		public double? Price { get; set; }
	}
}
