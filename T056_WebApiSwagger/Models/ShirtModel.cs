using System.ComponentModel.DataAnnotations;
using T056_WebApiSwagger.Models.Validations;

namespace T056_WebApiSwagger.Models
{
	public class ShirtModel
	{
		[Key]
		public int ShirtId { get; set; }

		[Required]
		public string Brand { get; set; } = String.Empty;

		public string Description { get; set; } = String.Empty;

		[Required]
		public string Color { get; set; } = String.Empty;

		[Shirt_EnsureCorrectSizing]
		public int Size { get; set; }

		[Required]
		public string Gender { get; set; } = String.Empty;

		public double? Price { get; set; }

		public bool ValidateDescription()
		{
			return string.IsNullOrEmpty(Description) is false;
		}
	}
}
