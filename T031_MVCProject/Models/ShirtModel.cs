using System.ComponentModel.DataAnnotations;
using T031_MVCProject.Models.Validations;

namespace T031_MVCProject.Models
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
