namespace Cipher.Handlers.Ciphers.Interfaces;

public interface IKeyCipherHandler
{
    string[] GetSubKeys(string key);
}