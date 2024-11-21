using AutoMapper;
using ECom.API.Mapper;
using ECom.BLogic.Services.Interfaces;
using ECom.BLogic.Services.Profile;
using ECom.Constants;
using ECom.Data;
using ECom.Data.Account;
using ECom.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECom.Test.BLogicTests
{
    public class ServiceWithContextTests
    {
        protected ApplicationDbContext _context;
        protected IUserService _userService;
        protected readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        protected EComUser testUser = new EComUser();
        public ServiceWithContextTests(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            _context = new ApplicationDbContext(options);
            var userManager = A.Fake<UserManager<EComUser>>();
            _userService = new UserService(A.Fake<SignInManager<EComUser>>(), userManager, _mapper);
            A.CallTo(() => userManager.GetUserAsync(A<ClaimsPrincipal>._)).Returns(testUser);

            if (_context.Products.Count() == 0)
            { LoadTestData(); }
        }

        protected virtual void LoadTestData()
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
    }
}
