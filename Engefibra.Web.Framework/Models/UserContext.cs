using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Web.Framework.Models
{
    public class UserContext
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Guid CookieID { get; set; }
        public string IpAddress { get; set; }
        public bool Logged { get; set; }
        public List<string> Perfis { get; set; }
    }
}
