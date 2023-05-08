using Ostral.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ostral.Core.Interfaces;

namespace Ostral.Core.Results
{
    public class GetRandomCourseResult : IResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public Course Course { get; set; } = new();
    }
}