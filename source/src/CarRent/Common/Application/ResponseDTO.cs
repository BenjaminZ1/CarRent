using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.Common.Application
{
    public class ResponseDto
    {
        public int Id { get; set; }
        public int NumberOfRows { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }

    }
}
