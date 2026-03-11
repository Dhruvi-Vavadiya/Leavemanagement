using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Leavemanagement.Service.ReqestDTO
{
    public class EmpRequestDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string>? RemoveImages { get; set; }
        public List<IFormFile?> ProofPhotos { get; set; }
    }
}
