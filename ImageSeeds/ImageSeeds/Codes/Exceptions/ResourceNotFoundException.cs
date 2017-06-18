using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageSeeds.Codes.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}