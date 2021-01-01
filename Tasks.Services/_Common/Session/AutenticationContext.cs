using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Tasks.Domain._Common.Session;

namespace Tasks.Services._Common.Session
{
    public class AutenticationContext : IAutenticationContext
    {
        public Guid Id => GetClaimValue<Guid>(nameof(Id));
        public string Login => GetClaimValue<string>(nameof(Login));

        private readonly HttpContext _context;

        public AutenticationContext(HttpContext context)
        {
            _context = context;
        }

        private T GetClaimValue<T>(string key)
        {
            if (!_context.User.Identity.IsAuthenticated) return default;
            var claim = _context.User.Claims.FirstOrDefault(c => c.Type == key);
            if (claim == null) return default;
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }
    }
}
