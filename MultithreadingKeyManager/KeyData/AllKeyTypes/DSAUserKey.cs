using MultithreadingKeyManager.UserData;
using System.Security.Cryptography;

namespace MultithreadingKeyManager.KeyData.AllKeyTypes
{
    //  key pair container
    public class DSAPairKeys
    {
        public DSAPairKeys(string Public, string Private)
        {
            PublicKey = Public;
            PrivateKey = Private;
        }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    // generate key pair on init class
    public class DSAUserKey : IKey
    {
        public DSAUserKey(User user)
        {
            // _RSAPairKeys = new RSAPairKeys("","");
            var dsa = new DSACryptoServiceProvider();
            var privateKey = dsa.ExportParameters(true); // private key
            var publicKey = dsa.ExportParameters(false); // public key
            _DSAPairKeys = new RSAPairKeys(dsa.ToXmlString(false), dsa.ToXmlString(true));
            _User = user;
        }
        // current user
        private User _User { get; set; }
        // pair public and private keys
        private RSAPairKeys _DSAPairKeys { get; set; }
        
        // get current key type
        KeyTypes IKey.GetKeyType()
        {
            return KeyTypes.DSA;
        }

        // get private PGP key
        public string GetPrivateKey()
        {
            return _DSAPairKeys.PrivateKey;
        }

        // get public PGP key
        public string GetPublicKey()
        {
            return _DSAPairKeys.PublicKey;
        }

        User IKey.GeUser()
        {
            return _User;
        }
    }
}
