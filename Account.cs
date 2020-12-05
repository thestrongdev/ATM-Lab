using System;
using System.Collections.Generic;
using System.Text;

namespace ATMLabGoodson
{
    class Account
    {
        private string _name;
        private string _password;
        public double Balance { get; set; }

        public Account(string name, string password)
        {
            _name = name;
            _password = password;
        }

        
    }
}
