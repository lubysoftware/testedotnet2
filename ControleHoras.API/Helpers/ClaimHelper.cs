using System;
using System.Linq;
using System.Security.Claims;

namespace ControleHoras.API.Helpers
{
    public static class ClaimHelper
    {
        public static string GetClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.Where(c => c.Type == claimType).FirstOrDefault();

            if (claim == null)
            {
                throw new Exception("Claim not found");
            }

            return claim.Value;
        }
    }
}
