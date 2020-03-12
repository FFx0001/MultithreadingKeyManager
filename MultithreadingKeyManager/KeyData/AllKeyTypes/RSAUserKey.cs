using MultithreadingKeyManager.UserData;
using System.Security.Cryptography;
using System.Threading;

namespace MultithreadingKeyManager.KeyData.AllKeyTypes
{
    // key container
    public class RSAPairKeys
    {
        public RSAPairKeys(string Public, string Private)
        {
            PublicKey = Public;
            PrivateKey = Private;
        }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
    public class RSAUserKey : IKey
    {
        // generate key pair on init class
        public RSAUserKey(User user)
        {
            _User = user;
            Thread.CurrentThread.IsBackground = true;
            RSACryptoServiceProvider RsaKey = new RSACryptoServiceProvider(config.RSAKeyLength);
            _RSAPairKeys = new RSAPairKeys(RsaKey.ToXmlString(false), RsaKey.ToXmlString(true));
        }
        // current user
        private User _User { get; set; }
        // pair public and private keys
        private RSAPairKeys _RSAPairKeys { get; set; }
        // get private rsa key
        public string GetPrivateKey()
        {
            return _RSAPairKeys.PrivateKey;
        }

        // get public rsa key
        public string GetPublicKey()
        {
            return _RSAPairKeys.PublicKey;
        }

        // get user
        public User GeUser()
        {
            return _User;
        }
        // get current key type
        KeyTypes IKey.GetKeyType()
        {
            return KeyTypes.RSA;
        }
    }
}
