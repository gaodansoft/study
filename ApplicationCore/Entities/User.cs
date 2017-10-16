using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{


    public class User: BaseEntity
    {
        public string AccountNum { get; set; }
        public string Pwd { get; set; }
        public int Status { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string Remark { get; set; }
    }
}
