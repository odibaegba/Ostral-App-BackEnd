using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostral.Core.DTOs
{
    public class CommentDTO
    {
       // public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }



    }
}
