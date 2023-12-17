namespace T02_WebAPIControllers.Models.Repositories
{
	public static class ShirtRepository
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

		public static ShirtModel? GetShirtById(int id)
		{
			return _shirts.FirstOrDefault(x => x.ShirtId == id);
		}
	}
}
