using AutoFixture;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Product;
using ECom.Constants;
using ECom.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace ECom.Test.BLogicTests
{
    public class ProductServiceTests : ServiceWithContextTests
    {
        private IImageService _imageService = A.Fake<IImageService>();
        private IProductService _productService;


        public ProductServiceTests() : base("DataBase_Poducts")
        {
            _productService = new ProductService(_context, _mapper, _imageService, _userService);
        }
        protected override void LoadTestData()
        {
            base.LoadTestData();
            _context.Users.Add(testUser);
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

        [Fact]
        public async Task DeletedProduct_IsNotShownAgain_IsTrue()
        {
            // Arrange
            var fixture = new Fixture();
            var productCountBefore = _context.Products.Count();
            var newProduct = A.Fake<Product>();
            newProduct.Name = fixture.Create<string>();
            newProduct.Genre = fixture.Create<string>();
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            // Act
            await _productService.DeleteProductAsync(newProduct.Id);
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
            var productImagesDTO = A.Fake<ProductImagesDTO>();
            // Act
            await _productService.UpdateProductAsync(productDTO, productImagesDTO);
            var updatedProduct = await _productService.GetProductAsync(newProduct.Id);
            // Assert
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
            await _productService.CreateProductAsync(productDTO, productImagesDTO);
            var newProduct = await _context.Products.FirstOrDefaultAsync(condition);
            // Assert
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
            await _productService.UpdateProductAsync(productDTO, imageDTO);
            var updatedProduct = await _productService.GetProductAsync(newProduct.Id);
            // Assert

            A.CallTo(() => _imageService.DeleteImageAsync(logo)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.DeleteImageAsync(background)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.UploadImageAsync(imageDTO.Logo, A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _imageService.UploadImageAsync(imageDTO.Background, A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FilterAsync_FilterByGenre_ReturnsFilteredProducts()
        {
            // Arrange
            var genre = "Action";
            var filterDTO = new ProductFilterDTO
            {
                Genre = genre
            };

            // Act
            var result = await _productService.FilterAsync(filterDTO);

            // Assert
            Assert.All(result, p => Assert.Equal(genre, p.Genre));
        }

        [Fact]
        public async Task FilterAsync_FilterByAgeRating_ReturnsFilteredProducts()
        {
            // Arrange
            var fixture = new Fixture();
            var agerRating = fixture.Create<DataEnums.Rating>();
            var filterDTO = new ProductFilterDTO
            {
                AgeRating = Enum.GetName(agerRating)
            };

            // Act
            var result = await _productService.FilterAsync(filterDTO);

            // Assert
            Assert.All(result, p => Assert.True(Enum.Parse<DataEnums.Rating>(p.Rating) == agerRating));
        }

        [Theory]
        [InlineData("Price")]
        [InlineData("TotalRating")]
        public async Task FilterAsync_Sort_ReturnsSortedProducts(string sortProperty)
        {
            // Arrange
            var filterDTO = new ProductFilterDTO
            {
                OrderPropertyName = sortProperty
            };

            // Act
            var result = await _productService.FilterAsync(filterDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.AsQueryable().OrderBy(x => x.GetType().GetProperty(sortProperty)), result);
        }

        [Fact]
        public async Task RateProduct_CreatesNewProductRating_True()
        {
            // Arrange
            int CountBefore = _context.ProductRatings.Count();
            var fixture = new Fixture();
            var rating = fixture.Create<ProductRatingDTO>();
            rating.ProductId = _context.Products.First().Id;

            // Act

            await _productService.RateProductAsync(rating);
            // Assert
            Assert.Equal(CountBefore + 1, _context.ProductRatings.Count());

        }

        [Fact]
        public async Task DeleteRatingAsync_DeletesUserRating_True()
        {
            // Arrange
            var fixture = new Fixture();
            ProductRatingDTO ratingDTO = fixture.Create<ProductRatingDTO>();
            if (_context.ProductRatings.Count() == 0)
            {
                ProductRating rating = new ProductRating();
                rating.ProductId = _context.Products.First().Id;
                rating.UserId = testUser.Id;
                _context.ProductRatings.Add(rating);
                await _context.SaveChangesAsync();
                ratingDTO = _mapper.Map<ProductRatingDTO>(rating);
            }
            else
            {
                ratingDTO = _mapper.Map<ProductRatingDTO>(_context.ProductRatings.First());
            }
            // Act

            await _productService.DeleteRatingAsync(ratingDTO);
            // Assert
            Assert.Null(_context.ProductRatings.FirstOrDefault(r => r.UserId == testUser.Id));

        }
    }
}




