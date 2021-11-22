using AutoMapper;
using FluentAssertions;
using MessageManagerService.Controllers;
using MessageManagerService.Controllers.Models;
using MessageManagerService.Data;
using MessageManagerService.Dtos;
using MessageManagerService.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MessageManagerUnitTesting.cs
{
    public class MessageManagerUnitTest
    {
        private readonly Mock<IMessageRepo> _messageRepoMockStub = new();
        private readonly Mock<IMapper> _mapperMockStub = new();
        private readonly Random rand = new();
        private IMapper _mapper;

        [Fact]
        public void GetMessageById_WithUnexstingMessage_ReturnsNotFound()
        {
            _messageRepoMockStub.Setup(r => r.GetMessageById(It.IsAny<int>())).Returns((Message)null);

            var controller = new MessagesManagerController(_messageRepoMockStub.Object, _mapperMockStub.Object);

            var message = controller.GetMessageById(rand.Next(10000000, 100000000));

            Assert.IsType<NotFoundResult>(message.Result);
        }


        [Fact]
        public void GetMessageById_WithExstingMessage_ReturnsExpectedMessage()
        {
            var mapper = GetMapper();
            // Arrange
            var ExpectedMessage = CreateRandomMesasge();
            _messageRepoMockStub.Setup(r => r.GetMessageById(It.IsAny<int>())).Returns(ExpectedMessage);

            //Act
            var controller = new MessagesManagerController(_messageRepoMockStub.Object, mapper);
            ActionResult<MessageReadDto> messageResult = controller.GetMessageById(rand.Next(1, 10));

            //Assert
            var messageResultValue = ((OkObjectResult)messageResult.Result).Value;
            ExpectedMessage.Should().BeEquivalentTo(messageResultValue, opt => opt.ComparingByMembers<MessageReadDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task CreateMessageAsync_WithMessageToCreate_ReturnsCreatedMessge()
        {
            // Arrange  
            var message = CreateRandomMesasge();
            var mapper = GetMapper();
            var messageToCreate = mapper.Map<MessageCreateDto>(message);
            var controller = new MessagesManagerController(_messageRepoMockStub.Object, mapper);

            //Act
            var createdMessageDto = await controller.CreateMessageAsync(messageToCreate);

            //Assert
            var createdMessage = (createdMessageDto.Result as CreatedAtRouteResult).Value as MessageReadDto;
            messageToCreate.Should().BeEquivalentTo(createdMessage, opt => opt.ComparingByMembers<MessageReadDto>().ExcludingMissingMembers());
        }

        [Fact]
        public void UpdateMessage_WithMessgeToUpdate_ReturnsUpdatedMessge()
        {
            // Arrange  
            var existingMessage = CreateRandomMesasge();
            _messageRepoMockStub.Setup(repo => repo.GetMessageById(It.IsAny<int>())).Returns(existingMessage);

            var mapper = GetMapper();
            var message = mapper.Map<Message>(existingMessage);
            var messageToUpdate = mapper.Map<MessageUpdateDto>(message);
            var controller = new MessagesManagerController(_messageRepoMockStub.Object, mapper);

            //Act
            var createdMessageDto = controller.UpdateMessage(existingMessage.Id, messageToUpdate);

            //Assert
            var createdMessage = (createdMessageDto.Result as CreatedAtRouteResult).Value as MessageReadDto;
            existingMessage.Should().BeEquivalentTo(createdMessage, opt => opt.ComparingByMembers<MessageReadDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task DeleteMessage_WithExistingMessage_ReturnsNoContent()
        {
            //// Arrange  
            var existingMessage = CreateRandomMesasge();
            _messageRepoMockStub.Setup(repo => repo.GetMessageById(It.IsAny<int>())).Returns(existingMessage);

            var mapper = GetMapper();
            var controller = new MessagesManagerController(_messageRepoMockStub.Object, _mapperMockStub.Object);

            //Act
            var createdMessageDto = await controller.DeleteMessageAsync(existingMessage.Id);

            //Assert
            createdMessageDto.Should().BeOfType<NoContentResult>();
        }

        #region Helpers
        private Message CreateRandomMesasge()
        {
            return new Message
            {
                Id = rand.Next(1, 10),
                MessageStr = "This is a Message Created from UnitTest",
                IsPalindrome = false
            };
        }

        private IMapper GetMapper()
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MessageProfile());
                });
                _mapper = config.CreateMapper();
            }

            return _mapper;
        }
        #endregion
    }
}
