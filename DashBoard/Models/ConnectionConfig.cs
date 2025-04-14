using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Models
{
    public class ConnectionConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string IsEncrypted { get; set; }
        public string UrlScheme { get; set; }
        public string BaseUr1 { get; set; }
    }
}
