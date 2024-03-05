namespace T016_ImplementingEndpoints.Models.Repositories
{
	public class ShirtRepository
	{
		private static readonly List<ShirtModel> _shirts =
		[
			new ShirtModel{ ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men",
				Price = 30, Size = 10 },
			new ShirtModel{ ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men",
				Price = 35, Size = 12 },
			new ShirtModel{ ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women",
				Price = 28, Size = 8 },
			new ShirtModel{ ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender = "Women",
				Price = 30, Size = 9 }
		];

		// Create
		public static void AddShirt(ShirtModel shirt)
		{
			int maxId = _shirts.Max(x => x.ShirtId);
			shirt.ShirtId = ++maxId;

			_shirts.Add(shirt);
		}

		// Read
		public static List<ShirtModel> GetShirts()
		{
			return _shirts;
		}

		// Update
		public static void EditShirt(ShirtModel shirt)
		{
			var shirtToUpdate = _shirts.First(x => x.ShirtId.Equals(shirt.ShirtId));
			shirtToUpdate.Brand = shirt.Brand;
			shirtToUpdate.Price = shirt.Price;
			shirtToUpdate.Size = shirt.Size;
			shirtToUpdate.Color = shirt.Color;
			shirtToUpdate.Gender = shirt.Gender;
		}

		// Delete
		public static void DeleteShirt(int id)
		{
			var shirt = GetShirtById(id);
			if (shirt is not null)
			{
				_shirts.Remove(shirt);
			}
		}

		// ReadById
		public static ShirtModel? GetShirtById(int id)
		{
			return _shirts.FirstOrDefault(x => x.ShirtId.Equals(id));
		}

		// ReadByProperties
		public static ShirtModel? GetShirtByProperties(string brand, string gender, string color, int size)
		{
			return _shirts.FirstOrDefault(
				x =>
					string.IsNullOrWhiteSpace(brand) is not true
					&& string.IsNullOrWhiteSpace(x.Brand) is not true
					&& x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)
					&& string.IsNullOrWhiteSpace(gender) is not true
					&& string.IsNullOrWhiteSpace(x.Gender) is not true
					&& x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)
					&& string.IsNullOrWhiteSpace(color) is not true
					&& string.IsNullOrWhiteSpace(x.Color) is not true
					&& x.Color.Equals(color, StringComparison.OrdinalIgnoreCase)
					&& x.Size.Equals(size)
				);
		}

		// ShirtExists
		public static bool ShirtExists(int id)
		{
			return _shirts.Any(x => x.ShirtId.Equals(id));
		}
	}
}
