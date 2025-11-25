using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Endpoint.Services
{
    public class SignalRHub : Hub
    {
        // 1. Belépés egy szobába (Páciens vagy Orvos hívja)
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            // Opcionális: Értesítés, hogy valaki belépett
            // await Clients.Group(roomName).SendAsync("ReceiveMessage", "Rendszer", $"{Context.ConnectionId} csatlakozott.");
        }

        // 2. Kilépés egy szobából (Amikor az orvos átvált másik betegre)
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        // 3. Üzenet küldése a szobába
        public async Task SendMessageToRoom(string roomName, string user, string message)
        {
            // Csak azok kapják meg, akik ebben a szobában vannak
            await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
        }
    }
}