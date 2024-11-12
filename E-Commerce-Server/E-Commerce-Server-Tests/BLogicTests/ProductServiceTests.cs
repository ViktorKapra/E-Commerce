﻿using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Product;
using ECom.Constants;
using ECom.Data;
using ECom.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ECom.Test.BLogicTests
{
    public class ProductServiceTests
    {
        private ApplicationDbContext _context;
        private IProductService _productService;

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            LoadTestData();
            _productService = new ProductService(_context);
        }

        private void LoadTestData()
        {
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 1, 25), Price = 25.5m, TotalRating = 3.2m},

                    new Product { Id = 2, Name = "Product 2", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 4, 26), Price = 30.0m, TotalRating = 4.5m},

                    new Product { Id = 3, Name = "Product 3", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 1, 27), Price = 50.0m, TotalRating = 4.0m},

                    new Product { Id = 4, Name = "Product 4", Platform = DataEnums.Platform.Console,
                        DateCreated = new DateOnly(2023, 1, 28), Price = 20.0m, TotalRating = 3.8m},

                    new Product { Id = 5, Name = "Product 5", Platform = DataEnums.Platform.Console,
                        DateCreated = new DateOnly(2023, 1, 29), Price = 35.0m, TotalRating = 4.2m},

                    new Product { Id = 3, Name = "Product 6", Platform = DataEnums.Platform.Mobile,
                        DateCreated = new DateOnly(2020, 1, 27), Price = 10.0m, TotalRating = 1.0m},
                };
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetTopPlatforms_ReturnsTopPlatforms()
        {
            // Arrange
            var plaformCount = 3;
            // Act
            var result = await _productService.GetTopPlatformsAsync(plaformCount);
            // Assert
            Assert.Equal(plaformCount, result.Count);
            Assert.Equal(DataEnums.Platform.PC, result[0]);
            Assert.Equal(DataEnums.Platform.Console, result[1]);
            Assert.Equal(DataEnums.Platform.Mobile, result[2]);
        }

    }
}

