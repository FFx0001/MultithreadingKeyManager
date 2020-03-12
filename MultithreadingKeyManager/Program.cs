using MultithreadingKeyManager.KeyData;
using MultithreadingKeyManager.UserData;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultithreadingKeyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RSA KeyLength: "+config.RSAKeyLength.ToString() + "\nStart process async creating pool keys for users:");
            List<User> Users = new List<User>();
            List<string[]> Userdata = new List<string[]>();
            Userdata.Add(new string[] { "Вася",     "RSA" });
            Userdata.Add(new string[] { "Сергей",   "RSA" });
            Userdata.Add(new string[] { "Андрей",   "RSA" });
            Userdata.Add(new string[] { "Игорь",    "DSA" });
            Userdata.Add(new string[] { "Анатолий", "DSA" });
            Userdata.Add(new string[] { "Дмитрий",  "DSA" });

            foreach (string[] _Userdata in Userdata)
            {
                if (_Userdata[1] == "RSA") { Users.Add(new User(_Userdata[0], KeyTypes.RSA)); } // Create RSA keys pair
                if (_Userdata[1] == "DSA") { Users.Add(new User(_Userdata[0], KeyTypes.DSA)); } // Create DSA keys pair
                KeyManager.CreateNewKeyPair(Users[Users.Count - 1]); 
                Console.WriteLine("Waiting create new key pair for username:" + Users[Users.Count - 1].UserName + " KeyType:"+ _Userdata[1]);
            }
            
            // waiting all tasks
            Console.WriteLine();
            Console.WriteLine("[Waiting creating keys]:");
            Console.WriteLine();
            List<string> lastWritingKeyTokens = new List<string>();
            while (true)
            {
                try
                {
                    foreach (IKey key in KeyManager.Keys)
                    {
                        if (lastWritingKeyTokens.IndexOf(key.GeUser().UserToken) == -1)
                        {
                            Console.WriteLine(string.Format("[Key Type]:{2}\n[User Name]:{0}\n[User Token]:{1}\n\n[Public Key]:\n{3}\n\n[Private Key]:\n{4}", key.GeUser().UserName, key.GeUser().UserToken, key.GetType().ToString(), key.GetPublicKey(), key.GetPrivateKey()));
                            Console.WriteLine("-----------------------------------------------------------------\n");
                            lastWritingKeyTokens.Add(key.GeUser().UserToken);
                        }
                    }
                }
                catch { }
                if(KeyManager.Keys.Count == Users.Count) { Console.WriteLine("Press any key to close programm."); break; }
                Thread.Sleep(1);
            }
           
            Console.ReadLine();
        }
       
    }
}
