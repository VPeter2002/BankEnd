using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository; // Vagy ahol az FWCDbContext van
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint.Services
{
    public class SignalRHub : Hub
    {
        // Adatbázis elérés injektálása
        private readonly FWCDbContext _context;

        public SignalRHub(FWCDbContext context)
        {
            _context = context;
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessageToRoom(string roomName, string user, string message)
        {
            // 1. MENTÉS AZ ADATBÁZISBA
            var chatMsg = new ChatMessage
            {
                Sender = user,
                Message = message,
                RoomName = roomName,
                Timestamp = DateTime.Now
            };

            _context.ChatMessages.Add(chatMsg);
            await _context.SaveChangesAsync();

            // 2. KÜLDÉS (Csak mentés után)
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }
    }
}