using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ATMLabGoodson
{
    class RegisteredAccounts
    {

   

        public static List<Account> ReadFrom(string textFileName) //return list
        {
            List<string> lines = File.ReadAllLines(textFileName).ToList();
            List<Account> accounts = new List<Account>();

            foreach(string line in lines)
            {
                string[] oneAccount = line.Split(",");
                Account registered = new Account(oneAccount[0], oneAccount[1]);
                accounts.Add(registered);
            }


            return accounts;
        }

        public static List<Account> WriteTo(string userName, string passWord)
        {
            var textFileName = @"registeredaccounts.txt";
            List<string> accounts = File.ReadAllLines(textFileName).ToList();
            string newItem = $"{userName},{passWord}";
            File.WriteAllLines(textFileName, accounts);

            return ReadFrom(textFileName);
        }
    }
}
