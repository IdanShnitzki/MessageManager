using AutoMapper;
using MessageManagerService.Controllers.Models;
using MessageManagerService.Data;
using MessageManagerService.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageManagerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesManagerController : ControllerBase
    {
        private readonly IMessageRepo _repository;
        private readonly IMapper _mapper;

        public MessagesManagerController(IMessageRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MessageReadDto>> GetMessage()
        {
            Console.WriteLine("Gettting Messages");

            var messages = _repository.GetAllMessages();
            return Ok(_mapper.Map<IEnumerable<Message>>(messages));
        }

        [HttpGet("{id}", Name = "GetMessageById")]
        public ActionResult<MessageReadDto> GetMessageById(int id)
        {
            Console.WriteLine($"Gettting Message #{id}");

            var message = _repository.GetMessageById(id);

            if (message != null)
                return Ok(_mapper.Map<MessageReadDto>(message));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<MessageReadDto>> CreateMessageAsync(MessageCreateDto message)
        {
            Console.WriteLine($"Creating message with the following text: {message.MessageStr}");

            var messageModel = _mapper.Map<Message>(message);

            _repository.CreateMessage(messageModel);
            await _repository.SaveChangesAsync();

            var messageReadDto = _mapper.Map<MessageReadDto>(messageModel);

            Console.WriteLine($"Message created: Id:{messageReadDto.Id}; Text:{message.MessageStr}; IsPalindrome:{messageReadDto.IsPalindrome};");

            return CreatedAtRoute(nameof(GetMessageById), new { id = messageReadDto.Id }, messageReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<MessageReadDto> UpdateMessage(int id, MessageUpdateDto message)
        {
            Console.WriteLine($"Updating message #{id} with the following input text:{message.MessageStr}");

            var retrivedMessage = _repository.GetMessageById(id);
            if (retrivedMessage == null)
            {
                return NotFound();
            }

            var messageModel = _mapper.Map<Message>(message);
            messageModel.Id = id;

            _repository.UpdateChanges(messageModel);
            _repository.SaveChanges();

            _repository.UpdateMessage(id, messageModel);

            var messageReadDto = _mapper.Map<MessageReadDto>(messageModel);

            Console.WriteLine($"Message updated: Id:{messageReadDto.Id}; Text:{message.MessageStr}; IsPalindrome:{messageReadDto.IsPalindrome};");

            return CreatedAtRoute(nameof(GetMessageById), new { id = messageReadDto.Id }, messageReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessageAsync(int id)
        {
            Console.WriteLine($"Deleting message #{id}");

            var existingItem = _repository.GetMessageById(id);
            if (existingItem == null)
            {
                Console.WriteLine($"Deleting message #{id} failed. Message #{id} was not found in Database");
                return NotFound();
            }


            _repository.RemoveMessage(id);
            await _repository.SaveChangesAsync();

            Console.WriteLine($"Deleting message #{id} finished with success");

            return NoContent();
        }
    }
}
