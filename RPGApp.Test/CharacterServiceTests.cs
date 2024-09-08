using Moq;
using RPGApp.Domain.Abstractions.Repositories;
using RPGApp.Domain.DTOs;
using RPGApp.Domain.DTOs.Character;
using RPGApp.Service;
using System.Runtime.CompilerServices;

namespace RPGApp.Test
{
    public class CharacterServiceTests
    {
        private readonly CharacterService _service;
        private readonly Mock<ICharacterRepository> _mockRepo;
        public CharacterServiceTests()
        {
            _mockRepo = new Mock<ICharacterRepository>();
            _service = new CharacterService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCharacterById_Returns_Character()
        {
            //Arrange 
            var character = new GetCharacterResponseDto
            {
                Id = 1,
                Name = "Harry",
                HitPoints = 25,
                Strength = 75,
                Defense = 85,
                Intelligence = 90
            };

            int userId = 1;

            _mockRepo.Setup(repo => repo.GetCharacterById(1, userId)).ReturnsAsync(character);

            // Act
            var result = await _service.GetCharacterById(1, userId);

            //Assert
            Assert.NotNull(result); 
            Assert.Equal("Harry", result!.Data!.Name);
            Assert.Equal(1, result!.Data!.Id);
        }

        [Fact]
        public async Task GetAllCharacters_Returns_Characters()
        {
            //Arrnge
            var characters = new List<GetCharacterResponseDto>
            {
                new GetCharacterResponseDto {Id = 1,Name= "Harry"},
                new GetCharacterResponseDto {Id = 2, Name= "Peter"}
            };

            int userId = 1; 

            _mockRepo.Setup(repo => repo.GetAllCharacters(userId)).ReturnsAsync(characters);

            //Act

            var result = await _service.GetAllCharacters(userId);

            //Assert 

            Assert.NotNull(result);
            Assert.Equal(2, result.Data!.Count); 
            Assert.Equal(characters, result.Data); 

        }

        [Fact]
        public async Task AddCharacter_Returns_Characters_With_Newly_Added_character()
        {
            //Arrange 
            var characters = new List<GetCharacterResponseDto>
            {
                new GetCharacterResponseDto {Id = 1, Name = "Harry"},
                new GetCharacterResponseDto {Id = 2, Name = "Peter"}, 
                new GetCharacterResponseDto {Id = 3, Name = "Jesmine"}
            };

            var newCharacter = new AddCharacterRequestDto
            {
                Name = characters[2].Name
            }; 

            int userId = 1;

            _mockRepo.Setup(repo => repo.AddCharacter(newCharacter, userId)).ReturnsAsync(characters);

            //Act 
            var result = await _service.AddCharacter(newCharacter, userId);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(3, result.Data!.Count); 
            Assert.Equal(characters, result.Data);
        }


        [Fact]
        public async Task DeleteCharacter_Returns_Remaining_Characters()
        {
            // Arrange 
            var characters = new List<GetCharacterResponseDto>
            {
                new GetCharacterResponseDto {Id = 1, Name = "Harry"},
                new GetCharacterResponseDto {Id = 2, Name = "Peter"},
                new GetCharacterResponseDto {Id = 3, Name = "Jesmine"}
            };

            var updatedCharacters = new List<GetCharacterResponseDto>
            {
                new GetCharacterResponseDto {Id = 1, Name = "Harry"},
                new GetCharacterResponseDto {Id = 2, Name = "Peter"}
            };

            int userId = 1;

            _mockRepo.Setup(repo => repo.DeleteCharacter(3, userId)).ReturnsAsync(updatedCharacters);

            // Act
            var result = await _service.DeleteCharacter(3, userId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Data!.Count); 
            Assert.Equal(updatedCharacters, result.Data);
        }



    }
}