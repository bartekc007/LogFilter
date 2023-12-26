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
        public string SourceComputer {get;set;}
        public string DestinationComputer {get;set;}
        public string AuthType {get;set;}
        public string LogonType {get;set;}
        public string AuthenticationOrientation {get;set;}
        public string AuthStatus {get;set;}
    }
}