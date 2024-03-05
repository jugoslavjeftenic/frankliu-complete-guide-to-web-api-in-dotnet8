using System.ComponentModel.DataAnnotations;
using T022_EntityFrameworkCore.Models.Validations;

namespace T022_EntityFrameworkCore.Models
{
	public class ShirtModel
	{
		[Key] public int ShirtId { get; set; }
		[Required] public string Brand { get; set; } = String.Empty;
		[Required] public string Color { get; set; } = String.Empty;
		[Shirt_EnsureCorrectSizing] public int Size { get; set; }
		[Required] public string Gender { get; set; } = String.Empty;
		public double? Price { get; set; }
	}
}
