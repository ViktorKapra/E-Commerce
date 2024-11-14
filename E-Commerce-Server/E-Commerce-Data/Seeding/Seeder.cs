using ECom.Data.Models;
using Microsoft.EntityFrameworkCore;
using static ECom.Constants.DataEnums;

namespace ECom.Data.Seeding
{
    internal class Seeder
    {
        private readonly ModelBuilder _modelBuilder;
        public Seeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void SeedProducts()
        {

            var gameNames = new string[] {
                "PC game 1", "PC game 2", "PC game 3",
                "Mobile game 1", "Mobile game 2","Mobile game 3",
                "Console game 1", "Console game 2", "VR game 1", "Web game 1"};

            var gamePrices = new decimal[] { 50, 60, 70, 30, 40, 50, 60, 70, 80, 90 };

            var gamePlatforms = new Platform[] { Platform.PC, Platform.PC, Platform.PC,
                Platform.Mobile, Platform.Mobile, Platform.Mobile,
                Platform.Console, Platform.Console, Platform.VR, Platform.Web };

            var gameDataCreated = new DateOnly[] { new DateOnly(2021, 1, 1), new DateOnly(2021, 1, 2), new DateOnly(2021, 1, 3),
                new DateOnly(2018, 1, 4), new DateOnly(2021, 1, 5), new DateOnly(2020, 1, 6),
                new DateOnly(2021, 1, 7), new DateOnly(2021, 1, 8), new DateOnly(2021, 1, 9), new DateOnly(2023, 1, 10) };

            var gameTotalRatings = new decimal[] { 4.5m, 4.6m, 4.7m, 4.8m, 4.9m, 5.0m, 4.1m, 4.2m, 4.3m, 4.4m };
            var gameGenres = new string[] { "Action", "Adventure", "RPG", "Puzzle", "Strategy", "Simulation", "Sports", "Racing", "Horror", "MMO" };
            var gameRatings = new Rating[] { Rating.PEGI_3, Rating.PEGI_7, Rating.PEGI_12, Rating.PEGI_12, Rating.PEGI_16,
                                             Rating.PEGI_16, Rating.PEGI_16, Rating.PEGI_18, Rating.PEGI_18,Rating.PEGI_18};
            var gameCounts = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

            for (int i = 0; i < gameNames.Length; i++)
            {
                _modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = i + 1,
                        Name = gameNames[i],
                        Price = gamePrices[i],
                        Platform = gamePlatforms[i],
                        DateCreated = gameDataCreated[i],
                        TotalRating = gameTotalRatings[i],
                        Rating = gameRatings[i],
                        Genre = gameGenres[i],
                        Count = gameCounts[i]
                    }
                );
            }
        }

    }
}
