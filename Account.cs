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

        public Account()
        {
         
        }

        public int GetAccount(string name, string password, List<Account> allAccounts)
        {
            int counter = 0; 

            foreach (Account item in allAccounts)
            {
                if (name == _name && password == _password)
                {
                    return counter;
                }

                counter += 1;
                
            }

            return counter; //may cause an out of bounds exception at some point
        }
    }
}
