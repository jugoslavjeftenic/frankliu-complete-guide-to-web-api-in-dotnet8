namespace T04_EntityFrameworkCore.Models.Repositories
{
	public class ShirtRepository
	{
		private static readonly List<ShirtModel> _shirts = [
			new ShirtModel
			{
				ShirtId = 1,
				Brand = "My Brand",
				Color = "Blue",
				Gender = "Men",
				Price = 30,
				Size = 10
			},
			new ShirtModel
			{
				ShirtId = 2,
				Brand = "My Brand",
				Color = "Black",
				Gender = "Men",
				Price = 35,
				Size = 12
			},
			new ShirtModel
			{
				ShirtId = 3,
				Brand = "Your Brand",
				Color = "Pink",
				Gender = "Women",
				Price = 28,
				Size = 8
			},
			new ShirtModel
			{
				ShirtId = 4,
				Brand = "Your Brand",
				Color = "Yello",
				Gender = "Women",
				Price = 30,
				Size = 9
			}
		];

		public static bool ShirtExists(int id)
		{
			return _shirts.Any(x => x.ShirtId == id);
		}

		public static void AddShirt(ShirtModel shirt)
		{
			int maxId = _shirts.Max(x => x.ShirtId);
			shirt.ShirtId = maxId + 1;

			_shirts.Add(shirt);
		}

		public static List<ShirtModel> GetShirts()
		{
			return _shirts;
		}

		public static ShirtModel? GetShirtById(int id)
		{
			return _shirts.FirstOrDefault(x => x.ShirtId == id);
		}

		public static ShirtModel? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
		{
			return _shirts.FirstOrDefault(x =>
				!string.IsNullOrWhiteSpace(brand) &&
				!string.IsNullOrWhiteSpace(x.Brand) &&
				x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(gender) &&
				!string.IsNullOrWhiteSpace(x.Gender) &&
				x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(color) &&
				!string.IsNullOrWhiteSpace(x.Color) &&
				x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
				size.HasValue &&
				x.Size.HasValue &&
				size.Value == x.Size.Value);
		}

		public static void UpdateShirt(ShirtModel shirt)
		{
			var shirtToUpdate = _shirts.First(x => x.ShirtId == shirt.ShirtId);
			shirtToUpdate.Brand = shirt.Brand;
			shirtToUpdate.Price = shirt.Price;
			shirtToUpdate.Size = shirt.Size;
			shirtToUpdate.Color = shirt.Color;
			shirtToUpdate.Gender = shirt.Gender;
		}

		public static void DeleteShirt(int id)
		{
			var shirt = GetShirtById(id);
			if (shirt != null)
			{
				_shirts.Remove(shirt);
			}
		}
	}
}
