using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Contracts;
using ChattingApp.Error_Model;
using ChattingApp.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChattingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepo _messageRepo;
        private readonly IConversationRepo _conversationRepo;
        private readonly IUserRepo _userRepo;

        private readonly IRepoBase<Message> _repo;

        public MessageController(
            IMessageRepo messageRepo,
            IRepoBase<Message> repo,
            IConversationRepo conversationRepo,
            IUserRepo userRepo)
        {
            _messageRepo = messageRepo;
            _repo = repo;
            _conversationRepo=conversationRepo;
            _userRepo=userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMassageAsync(int convId)
        {
            var conversation = await _conversationRepo.GetConvByIdDbAsync(convId);
            if (conversation is null) return NotFound(new ApiReposnse(404));

            var msgs = await _messageRepo.GetAllMessageAsync(convId);
            if (msgs is null) return BadRequest(400);
            return Ok(msgs);
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessageAsync(int messageId)
        {
            var msg = await _messageRepo.GetMessagedDbById(messageId);
            // handle error
            if (msg is null) return NotFound(new ApiReposnse(404));

            return Ok(msg);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message msg)
        {
            if (msg is null) return BadRequest(new ApiReposnse(400));
            _messageRepo.CreateMessage(msg);
            await _repo.Save();
            return CreatedAtAction("GetMessageAsync", new { Id = msg.Id }, msg );
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(int messageId)
        {
            var message = await _messageRepo.GetMessagedDbById(messageId);
            if (message == null) return NotFound(new ApiReposnse(404));
            _messageRepo.DeleteMessage(messageId);
            await _repo.Save();
            return NoContent();
        }
    }
}