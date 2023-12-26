using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogFilter.entities
{
    public class Auth
    {
        public int Time {get;set;}
        public string SourceUserDomain {get;set;}
        public string DestinationUserDomain {get;set;}
    }
}