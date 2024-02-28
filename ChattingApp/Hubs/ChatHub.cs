using ChattingApp.Contracts;
using ChattingApp.Models;
using Microsoft.AspNetCore.SignalR;
using User = ChattingApp.Models.Message;
namespace ChattingApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IRoomRepo _roomRepo;
        private readonly IMessageRepo _messageRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRepoBase<Room> _repoBase;
        private readonly IRepoBase<Conversation> _repoBaseConv;
        private readonly IConversationRepo _conversationRepo;

        public ChatHub(
            IRoomRepo roomRepo,
            IMessageRepo messageRepo,
            IUserRepo userRepo, 
            IRepoBase<Room> repoBase,
            IRepoBase<Conversation> repoBase1,
            IConversationRepo conversationRepo
            ) 
        {
            _roomRepo = roomRepo;
            _messageRepo = messageRepo;
            _userRepo = userRepo;
            _repoBase = repoBase;
            _repoBaseConv=repoBase1;
            _conversationRepo = conversationRepo;
        }

        public async Task JoinRoom(int roomId, string userName)
        {
            var room = await _roomRepo.GetRoomAsync(roomId);

            if (room != null)
            {
                var user = await _userRepo.GetUserByNameAsync(userName);
                if (user != null)
                {
                    room.users.Add(user);
                    await _repoBase.Save();
                    await Clients.Group($"Room_{roomId}").SendAsync("UserJoined",
                        "Lets Program Bot", $"{userName} has Joined the Group", DateTime.Now);
                }
            }
        }

        public async Task SendMessage(int conversationId, string messageContent)
        {
            var conversation = await _conversationRepo.GetConvByIdDbAsync(conversationId);
            if(conversation != null)
            {
                var message = new Message { Content = messageContent, ConversationId  = conversationId};
                conversation.Messages.Add(message);
                await _repoBaseConv.Save();
                await Clients.Group($"Conversation_{conversationId}").SendAsync("ReceiveMessage", messageContent);
            }
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Implement logic to handle user disconnection
            // For example, remove the user from all rooms
            var userName = Context?.User?.Identity?.Name;
            var user = await _userRepo.GetUserByNameAsync(userName);
            if(user != null)
            {
                var rooms = _roomRepo.GetRoomByUserAsync(user);
                foreach (var room in await rooms)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Room_{room.Id}");
                }
                await base.OnDisconnectedAsync(exception);
            }
        }

        public async Task SendConnectedUsers()
        {
            // Implement logic to send connected users to the client
            var users = _userRepo.GetAllUserAsync();
            await Clients.All.SendAsync("ConnectedUsers", users);
        }


    }
}
