using BoardGameNight.Domain.Exceptions;
using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using BoardGameNight.Infrastructure.Repositories;
using NSubstitute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardGameNight.UI.Tests
{
    public class BoardgameNightTests
    {
        private readonly IBoardgameNightRepository boardgameNightRepository = Substitute.For<IBoardgameNightRepository>();
        private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();
        private readonly IBoardgameNightService sut;

        public BoardgameNightTests()
        {
            sut = new BoardgameNightService(boardgameNightRepository, userRepository);
        }

        [Fact]
        public void Add_ValidData_AddsBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            ICollection<Boardgame> boardGames = new List<Boardgame>();
            var newBoardgameNight = new BoardgameNight { Id = boardgameNightId, Name = "Game Night 1", Boardgames = boardGames, Organizer = new User { BirthDate = new DateTime(2000, 1, 1) } };
            boardgameNightRepository.getById(boardgameNightId).Returns(newBoardgameNight);

            // Act
            sut.add(newBoardgameNight);

            // Assert
            boardgameNightRepository.Received().add(newBoardgameNight);
        }

        [Fact]
        public void Update_ValidData_UpdatesBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            ICollection<Boardgame> boardGames = new List<Boardgame>();
            var existingBoardgameNight = new BoardgameNight { Id = boardgameNightId, Name = "Updated Game Night", Boardgames = boardGames};

            // Act
            sut.update(existingBoardgameNight);

            // Assert
            boardgameNightRepository.Received().update(existingBoardgameNight);
        }

        [Fact]
        public void Delete_ValidData_DeletesBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            var boardgameNight = new BoardgameNight()
            {
                Id = boardgameNightId,
            };
            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);

            // Act
            sut.delete(boardgameNightId);

            // Assert
            boardgameNightRepository.Received().delete(boardgameNightId);
        }

        [Fact]
        public void GetById_ValidData_ReturnsBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            var boardgameNight = new BoardgameNight { Id = boardgameNightId, Name = "Game Night 1" };
            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);

            // Act
            var result = sut.getById(boardgameNightId);

            // Assert
            Assert.Equal(boardgameNight, result);
        }

        [Fact]
        public void GetAll_ValidData_ReturnsAllBoardgameNights()
        {
            // Arrange
            var boardgameNights = new List<BoardgameNight>
            {
                new BoardgameNight { Id = 1, Name = "Game Night 1" },
                new BoardgameNight { Id = 2, Name = "Game Night 2" }
            };
            boardgameNightRepository.getAll().Returns(boardgameNights);

            // Act
            var result = sut.getAll();

            // Assert
            Assert.Equal(boardgameNights, result);
        }

        [Fact]
        public void Delete_BoardgameNightDoesNotExist_ThrowsInvalidDataException()
        {
            // Arrange
            int boardgameNightId = 99;
            boardgameNightRepository.getById(boardgameNightId).Returns((BoardgameNight?)null);

            // Act & Assert
            Assert.Throws<InvalidDataException>(() => sut.delete(boardgameNightId));
        }

        [Fact]
        public void GetById_BoardgameNightDoesNotExist_ReturnsNull()
        {
            // Arrange
            int boardgameNightId = 99;
            boardgameNightRepository.getById(boardgameNightId).Returns((BoardgameNight?)null);

            // Act & Assert
            Assert.Null(sut.getById(boardgameNightId));
        }

        [Fact]
        public void addParticipant_BoardgameNightDoesNotExist_ThrowsInvalidDataException()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            boardgameNightRepository.getById(boardgameNightId).Returns((BoardgameNight?)null);

            // Act & Assert
            Assert.Throws<InvalidDataException>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_ParticipantDoesNotExist_ThrowsInvalidDataException()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var boardgameNight = new BoardgameNight();
            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns((User?)null);

            // Act & Assert
            Assert.Throws<InvalidDataException>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_UserAlreadyEnrolled_ThrowsAlreadyEnrolledInBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var user = new User { Id = participantId };
            var boardgameNight = new BoardgameNight
            {
                Id = boardgameNightId,
                Participants = new List<User> { user }
            };

            ICollection<BoardgameNight> boardgameNights = new List<BoardgameNight> { boardgameNight };

            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns(user);
            boardgameNightRepository.getAll().Returns(boardgameNights);

            // Act & Assert
            Assert.Throws<AlreadyEnrolledInBoardgameNight>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_GameNightIsFull_ThrowsGameNightIsFullException()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var user = new User { Id = participantId };
            var boardgameNight = new BoardgameNight
            {
                Id = boardgameNightId,
                MaxParticipants = 1,
                Participants = new List<User> { new User() }
            };

            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns(user);

            // Act & Assert
            var exception = Assert.Throws<GameNightIsFullException>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_OrganizerTriesToEnroll_ThrowsOrganizerEnrolledInOwnBoardgameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var user = new User { Id = participantId };
            var boardgameNight = new BoardgameNight
            {
                Id = boardgameNightId,
                MaxParticipants = 2,
                Participants = new List<User> { new User() },
                Organizer = user
            };

            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns(user);

            // Act & Assert
            var exception = Assert.Throws<OrganizerEnrolledInOwnBoardgameNight>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_UserNotOldEnough_ThrowsNotOldEnoughException()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var user = new User 
            {
                Id = participantId,
                BirthDate = DateTime.Today,
            };
            var boardgameNight = new BoardgameNight
            {
                Id = boardgameNightId,
                MaxParticipants = 2,
                Participants = new List<User> { new User() },
                Organizer = new User(),
                IsAdult = true
            };

            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns(user);

            // Act & Assert
            var exception = Assert.Throws<NotOldEnoughException>(() =>
                sut.addParticipant(boardgameNightId, participantId));
        }

        [Fact]
        public void addParticipant_UserAlreadyEnrolledThatDay_ThrowsAlreadyEnrolledInBoardgameNightDate()
        {
            // Arrange
            int boardgameNight1Id = 1;
            int boardgameNight2Id = 2;
            int participantId = 2;
            var user = new User
            {
                Id = participantId,
            };
            var boardgameNight1 = new BoardgameNight
            {
                Id = boardgameNight1Id,
                MaxParticipants = 2,
                Participants = new List<User> { user },
                DateTime = DateTime.Today
            };
            var boardgameNight2 = new BoardgameNight
            {
                Id = boardgameNight2Id,
                MaxParticipants = 2,
                Participants = new List<User> { new User() },
                Organizer = new User(),
                DateTime = DateTime.Today
            };

            ICollection<BoardgameNight> boardgameNights = new List<BoardgameNight> { boardgameNight1, boardgameNight2 };

            boardgameNightRepository.getById(boardgameNight2Id).Returns(boardgameNight2);
            boardgameNightRepository.getAll().Returns(boardgameNights);
            userRepository.getById(participantId).Returns(user);

            // Act & Assert
            Assert.Throws<AlreadyEnrolledInBoardgameNightDate>(() =>
                sut.addParticipant(boardgameNight2Id, participantId));
        }

        [Fact]
        public void addParticipant_ValidData_AddsParticipantToGameNight()
        {
            // Arrange
            int boardgameNightId = 1;
            int participantId = 2;
            var user = new User
            {
                Id = participantId,
            };
            var boardgameNight = new BoardgameNight
            {
                Id = boardgameNightId,
                MaxParticipants = 2,
                Participants = new List<User> { new User() },
                Organizer = new User()
            };

            boardgameNightRepository.getById(boardgameNightId).Returns(boardgameNight);
            userRepository.getById(participantId).Returns(user);

            // Act
            sut.addParticipant(boardgameNightId, participantId);

            // Assert
            Assert.Contains(user, boardgameNight.Participants);
            boardgameNightRepository.Received().update(boardgameNight);
        }
    }
}