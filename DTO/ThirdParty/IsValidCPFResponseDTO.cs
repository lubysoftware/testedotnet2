using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Response
{
    public class IsValidCPFResponseDTO : IResponseDTO
    {
        public string Message { get; set; }
    }
}
