namespace Cipher.Handlers.Ciphers.Interfaces;

public interface ICipherHandler
{
    void Encode(byte[] bytes, string key);
    
    void Decode(byte[] bytes, string key);
}