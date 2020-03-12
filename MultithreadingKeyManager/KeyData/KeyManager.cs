using MultithreadingKeyManager.KeyData.AllKeyTypes;
using MultithreadingKeyManager.UserData;
using System.Collections.Generic;
using System.Threading;

namespace MultithreadingKeyManager.KeyData
{
    public static class KeyManager
    {
       public static List<IKey> Keys = new List<IKey>();
        // Create async new key pair
        public static void CreateNewKeyPair(User user)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                if (user.KeyType == KeyTypes.RSA) { Keys.Add(new RSAUserKey(user)); }
                if (user.KeyType == KeyTypes.DSA) { Keys.Add(new DSAUserKey(user)); }
            }).Start();
        }
        // delete key from token
        public static bool DeleteKey(string UserToken)
        {
            if (Keys.Count > 0)
            {
                foreach (IKey key in Keys)
                {
                    if (key.GeUser().UserToken == UserToken) { Keys.Remove(key);return true; }
                }
            }
            return false;
        }
        // get public key from token
        public static string GetPublicKey(string UserToken)
        {
            if (Keys.Count > 0)
            {
                foreach (IKey key in Keys)
                {
                    if (key.GeUser().UserToken == UserToken) { return key.GetPublicKey(); }
                }
            }
            return "";
        }
        // get private key from token
        public static string GetPrivateKey(string UserToken)
        {
            if (Keys.Count > 0)
            {
                foreach (IKey key in Keys)
                {
                    if (key.GeUser().UserToken == UserToken) { return key.GetPrivateKey(); }
                }
            }
            return "";
        }
    }
   
}
