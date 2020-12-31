using System;

namespace Tasks.Domain.Developers.Dtos
{
    public class TokenDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
