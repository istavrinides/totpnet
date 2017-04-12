using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOTPModel
{
    public class TOTPUser
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public Boolean OTPEnabled { get; set; }
        public System.Data.Linq.Binary OTPKey { get; set; }
    }
}
