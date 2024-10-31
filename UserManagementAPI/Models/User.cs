namespace UserManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Пароль добавлен для примера, в реальной системе использовать хеширование
        public bool IsOnline { get; set; }
    }
}
