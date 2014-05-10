using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogetherIsBetter
{
    public class User
    {

        private String username, role;
        private bool authenticated;

        public bool Authenticated
        {
            get { return authenticated; }
            set { authenticated = value; }
        }

        public String Role
        {
            get { return role; }
            set { role = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }



    }
}
