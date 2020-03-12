using MultithreadingKeyManager.UserData;

namespace MultithreadingKeyManager.KeyData
{
    public interface IKey
    {
        KeyTypes GetKeyType();
        User GeUser();
        string GetPublicKey();
        string GetPrivateKey();
    }
    public enum KeyTypes
    {
        RSA,
        DSA
    }
}
