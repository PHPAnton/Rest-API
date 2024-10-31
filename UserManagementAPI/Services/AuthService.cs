using System.Collections.Concurrent;

namespace UserManagementAPI.Services
{
    public static class AuthService
    {
        private static readonly ConcurrentDictionary<int, DateTime> ActiveSessions = new();

        public static bool IsAuthenticated(int userId)
        {
            return ActiveSessions.TryGetValue(userId, out var loginTime) &&
                   DateTime.UtcNow < loginTime.AddMinutes(5);
        }

        public static void Login(int userId)
        {
            ActiveSessions[userId] = DateTime.UtcNow;
        }

        public static void Logout(int userId)
        {
            ActiveSessions.TryRemove(userId, out _);
        }
    }
}
