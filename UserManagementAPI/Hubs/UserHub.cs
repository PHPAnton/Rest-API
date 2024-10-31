using Microsoft.AspNetCore.SignalR;

namespace UserManagementAPI.Hubs
{
    public class UserHub : Hub
    {
        public async Task UpdateUserStatus(int userId, bool isOnline)
        {
            await Clients.All.SendAsync("ReceiveUserStatus", userId, isOnline);
        }
    }
}
