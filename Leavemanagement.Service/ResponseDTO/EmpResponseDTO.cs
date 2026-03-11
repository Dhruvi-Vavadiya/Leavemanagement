using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Leavemanagement.Service.ResponseDTO
{
    public class EmpResponseDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        List<string?> ProofPhotos { get; set; }
    }
}
