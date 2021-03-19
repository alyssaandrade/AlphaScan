using System;
using System.Collections.Generic;
using System.Text;

namespace BabyTech
{
    public class AWSUser
    {
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenIssued { get; set; }
        public DateTime Expires { get; set; }
    }
}
