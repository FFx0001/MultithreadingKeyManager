using MultithreadingKeyManager.KeyData;

namespace MultithreadingKeyManager.UserData
{
    public class User
    {
        public User(string UserName, KeyTypes keyType)
        {
            _UserName = UserName;
            _KeyType = keyType;
        }
        private KeyTypes _KeyType { get; set; }
        private string _UserName { get; set; }
        public KeyTypes KeyType { get { return _KeyType; } }
        public string UserName { get { return _UserName; } }
        public string UserToken { get { return CryptoAlgoriphms.ComputeSha256Hash(UserName); } }
    }
}
