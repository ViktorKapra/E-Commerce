
using AutoFixture;
using AutoMapper;
using ECom.API.DTOs.ProfileDTOs;
using ECom.API.Mapper;
using ECom.Data.Account;

namespace ECom.Test.APITests.Mapper
{
    public class MappingTests
    {
        private readonly IMapper _mapper;
        public MappingTests()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
        }

        private UserDTO createUserDTO()
        {
            var fixture = new Fixture();
            var result = new UserDTO();
            result.Email = fixture.Create<string>();
            result.FirstName = fixture.Create<string>();
            result.LastName = fixture.Create<string>();
            result.PhoneNumber = fixture.Create<string>();
            result.AddressDelivery = fixture.Create<string>();
            return result;
        }

        [Fact]
        public void Mapping_UserDTO_To_User_saves_Id()
        {
            //Arrange
            var fixture = new Fixture();
            UserDTO userDTO = createUserDTO();
            var user = new EComUser();
            var idBeforeMapping = user.Id;
            //Act
            _mapper.Map(userDTO, user);
            //Assert
            var idAfterMapping = user.Id;
            Assert.Equal(idBeforeMapping, idAfterMapping);
        }
        [Fact]
        public void Mapping_UserDTO_To_User_Maps_correctly()
        {
            //Arrange
            var fixture = new Fixture();
            UserDTO userDTO = createUserDTO();
            var user = new EComUser();
            //Act
            _mapper.Map(userDTO, user);
            //Assert
            Assert.Equal(user.UserName, userDTO.Email);
            Assert.Equal(user.Email, userDTO.Email);
            Assert.Equal(user.FirstName, userDTO.FirstName);
            Assert.Equal(user.LastName, userDTO.LastName);
            Assert.Equal(user.PhoneNumber, userDTO.PhoneNumber);
            Assert.Equal(user.AddressDelivery, userDTO.AddressDelivery);
        }
        [Fact]
        public void Mapping_User_To_User_Maps_correctly()
        {
            //Arrange
            var fixture = new Fixture();
            var user = new EComUser();
            UserDTO userDTO = createUserDTO();
            //Act
            _mapper.Map(user, userDTO);
            //Assert
            Assert.Equal(user.Email, userDTO.Email);
            Assert.Equal(user.FirstName, userDTO.FirstName);
            Assert.Equal(user.LastName, userDTO.LastName);
            Assert.Equal(user.PhoneNumber, userDTO.PhoneNumber);
            Assert.Equal(user.AddressDelivery, userDTO.AddressDelivery);
        }
    }
}
