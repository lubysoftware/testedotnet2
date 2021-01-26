using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    public class AuthUserResponseDTO : IResponseDTO
    {        
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public DeveloperResponseDTO Developer { get; set; }
    }
}
