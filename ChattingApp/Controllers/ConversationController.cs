using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ChattingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepo _convRepo;
        private readonly IRepoBase<Conversation> _repo;

        public ConversationController(IConversationRepo conversationRepo, IRepoBase<Conversation> repo)
        {
            _convRepo = conversationRepo;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetConversation(int userId, int roomId)
        {
            /// <summary>
            /// check first userId and roomId
            /// </summary>

            var conv = await _convRepo.GetConvAsync(userId, roomId);
            /// handle error
            if (conv is null) return BadRequest();
            return Ok(conv);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversationAsync([FromBody] Conversation conv)
        {
            _convRepo.CreateConvAsync(conv);
            await _repo.Save();
            return Ok(conv);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConvAsync(int convId)
        {
            _convRepo.DeleteConv(convId);
            await _repo.Save();
            return Ok(201);
        }
    }
}