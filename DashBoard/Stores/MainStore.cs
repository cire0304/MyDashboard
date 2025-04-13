using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashBoard.Stores
{
    public class MainStore
    {
        private string id;
        private string password;

        public string Password { get => password; set => password = value; }
        public string Id { get => id; set => id = value; }
    }
}
