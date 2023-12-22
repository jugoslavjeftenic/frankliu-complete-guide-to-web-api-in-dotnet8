using System.ComponentModel.DataAnnotations;
using T04_EntityFrameworkCore.Models.Validations;

namespace T04_EntityFrameworkCore.Models
{
	public class ShirtModel
	{
		[Key]
		public int ShirtId { get; set; }

		[Required]
		public string? Brand { get; set; }

		[Required]
		public string? Color { get; set; }

		[Shirt_EnsureCorrectSizing]
		public int? Size { get; set; }

		[Required]
		public string? Gender { get; set; }

		public double? Price { get; set; }
	}
}
