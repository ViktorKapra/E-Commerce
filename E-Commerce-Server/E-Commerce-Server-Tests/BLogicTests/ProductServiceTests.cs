using AutoMapper;
using ECom.API.Mapper;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Product;
using ECom.Constants;
using ECom.Data;
using ECom.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECom.Test.BLogicTests
{
    public class ProductServiceTests
    {
        private ApplicationDbContext _context;
        private IImageService _imageService = A.Fake<IImageService>();
        private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        private IProductService _productService;

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            if (_context.Products.Count() == 0)
            { LoadTestData(); }
            _productService = new ProductService(_context, _mapper, _imageService);
        }

        private void LoadTestData()
        {
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 1, 25), Price = 25.5m, TotalRating = 3.2m,
                        Genre = "PRG"},

                    new Product { Id = 2, Name = "Product 2", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 4, 26), Price = 30.0m, TotalRating = 4.5m,
                        Genre = "Action"},

                    new Product { Id = 3, Name = "Product 3", Platform = DataEnums.Platform.PC,
                        DateCreated = new DateOnly(2023, 1, 27), Price = 50.0m, TotalRating = 4.0m,
                        Genre = "Racing"},

                    new Product { Id = 4, Name = "Product 4", Platform = DataEnums.Platform.Console,
                        DateCreated = new DateOnly(2023, 1, 28), Price = 20.0m, TotalRating = 3.8m,
                        Genre = "Logic"},

                    new Product { Id = 5, Name = "Product 5", Platform = DataEnums.Platform.Console,
                        DateCreated = new DateOnly(2023, 1, 29), Price = 35.0m, TotalRating = 4.2m,
                        Genre = "Puzzle"},

                    new Product { Id = 6, Name = "Product 6", Platform = DataEnums.Platform.Mobile,
                        DateCreated = new DateOnly(2020, 1, 27), Price = 10.0m, TotalRating = 1.0m,
                        Genre = "Logic"},
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
        [Theory]
        [InlineData(DataEnums.Platform.PC, 3, new string[] { "Product 1", "Product 2", "Product 3" })]
        public async Task SearchAsync_SeachByPlatforms_True(DataEnums.Platform platform, int expectedCount,
            string[] expectedNames)
        {
            // Arrange
            var searchDTO = new ProductSearchDTO
            {
                Platform = Enum.GetName(platform),
                Limit = 10,
                Offset = 0
            };
            // Act
            var result = await _productService.SearchAsync(searchDTO);
            var resultsNames = result.Select(p => p.Name).ToList();
            // Assert
            Assert.Equal(expectedCount, result.Count);
            for (int i = 0; i < expectedCount; i++)
            {
                Assert.Contains(expectedNames[i], resultsNames);
            }
        }

        [Theory]
        [InlineData("Product 1", 1, new string[] { "Product 1" })]
        public async Task SearchAsync_SeachBySubstring_True(string searcedName, int expectedCount,
            string[] expectedNames)
        {
            // Arrange
            var searchDTO = new ProductSearchDTO
            {
                Name = searcedName,
                Limit = 10,
                Offset = 0
            };
            // Act
            var result = await _productService.SearchAsync(searchDTO);
            var resultsNames = result.Select(p => p.Name).ToList();
            // Assert
            Assert.Equal(expectedCount, result.Count);
            for (int i = 0; i < expectedCount; i++)
            {
                Assert.Contains(expectedNames[i], resultsNames);
            }
        }

        [Theory]
        [InlineData(3)]
        public async Task GetTaskById_ReturnsTaskWithCorrectId_IsTrue(int id)
        {
            // Act
            var result = await _productService.GetProductAsync(id);
            // Assert
            Assert.Equal(id, result.Id);
        }

        [Theory]
        [InlineData(0)]
        public async Task GetTaskById_WrongId_ReturnsNull_IsTtrue(int id)
        {
            // Act
            var result = await _productService.GetProductAsync(id);
            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeletedProduct_IsNotShownAgain_IsTrue(int id)
        {
            // Arrange
            var productCountBefore = _context.Products.Count();
            var newProduct = A.Fake<Product>();
            _context.Products.Add(newProduct);
            // Act
            var product = await _productService.DeleteProductAsync(newProduct.Id);
            var productCountAfter = _context.Products.Count();
            // Assert
            Assert.Equal(productCountBefore, productCountAfter);


        }
        [Fact]
        public async Task UpdateProductAsync_UpdatesProduct_IsTrue()
        {
            // Arrange
            var newProduct = A.Fake<Product>();
            newProduct.Name = "Name";
            newProduct.Genre = "Genre";
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            var productDTO = _mapper.Map<ProductDTO>(newProduct);
            productDTO.Name = "Updated Name";
            productDTO.Genre = "Updated Genre";
            // Act
            var result = await _productService.UpdateProductAsync(productDTO, null);
            var updatedProduct = await _productService.GetProductAsync(newProduct.Id);
            // Assert
            Assert.True(result);
            Assert.Equal(productDTO.Name, updatedProduct.Name);
            Assert.Equal(productDTO.Genre, updatedProduct.Genre);
        }

        [Fact]
        public async Task CreateProductAsync_CreatesProduct_IsTrue()
        {

            // Arrange
            var specialName = "Name";
            var specialGenre = "Genre";
            Expression<Func<Product, bool>> condition = p => p.Name == specialName
                                                            && p.Genre == specialGenre;
            var existing = await _context.Products.Where(condition).ToListAsync();
            if (existing.Count > 0)
            {
                _context.Products.RemoveRange(existing);
            }
            var productDTO = A.Fake<ProductDTO>();
            productDTO.Name = specialName;
            productDTO.Genre = specialGenre;
            var productImagesDTO = A.Fake<ProductImagesDTO>();
            // Act
            var result = await _productService.CreateProductAsync(productDTO, productImagesDTO);
            var newProduct = await _context.Products.FirstOrDefaultAsync(condition);
            // Assert
            Assert.True(result);
            Assert.Equal(productDTO.Name, newProduct.Name);
            Assert.Equal(productDTO.Genre, newProduct.Genre);
        }

        [Fact]
        public async Task UpdateProductAsync_UpdatesImages_IsTrue()
        {
            // Arrange
            string logo = "logo.png";
            string background = "background.png";
            var newProduct = new Product
            {
                Name = "Name",
                Genre = "Genre",
                Logo = logo,
                Background = background
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            var productDTO = _mapper.Map<ProductDTO>(newProduct);
            productDTO.Name = "Updated Name";
            productDTO.Genre = "Updated Genre";
            var imageDTO = new ProductImagesDTO
            {
                Logo = A.Fake<FormFile>(),
                Background = A.Fake<FormFile>()
            };
            // Act
            var result = await _productService.UpdateProductAsync(productDTO, imageDTO);
            var updatedProduct = await _productService.GetProductAsync(newProduct.Id);
            // Assert
            Assert.True(result);
            A.CallTo(() => _imageService.DeleteImageAsync(logo)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.DeleteImageAsync(background)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.UploadImageAsync(imageDTO.Logo, A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.UploadImageAsync(imageDTO.Background, A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}

