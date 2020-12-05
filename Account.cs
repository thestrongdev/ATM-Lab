using System;
using System.Collections.Generic;
using System.Text;

namespace ATMLabGoodson
{
    class Account
    {
        private string _name;
        private string _password;
        public string userAccount { get; set; }

        public string userName { get; set; }
        public string password { get; set; }
        public double Balance { get; set; }

        public Account(string name, string password)
        {
            _name = name;
            _password = password;
        }

        public Account GetAccount(string name, string password, List<Account> allAccounts)
        {
            foreach (Account item in allAccounts)
            {
                if (name == _name && password == _password)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
