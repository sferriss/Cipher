using Cipher.Handlers.Ciphers.Interfaces;
using Cipher.Handlers.Files;

namespace Cipher.Handlers.Ciphers;

public class CipherHandler : ICipherHandler
{
    private readonly IFileHandler _fileHandler;
    private readonly IKeyCipherHandler _keyCipherHandler;

    public CipherHandler(IFileHandler fileHandler, IKeyCipherHandler keyCipherHandler)
    {
        _fileHandler = fileHandler;
        _keyCipherHandler = keyCipherHandler;
    }

    public void Encode(byte[] bytes, string key)
    {
        var subkeys = _keyCipherHandler.GetSubKeys(key);

        //corpo do cifrador em bloco

        throw new NotImplementedException();
    }

    public void Decode(byte[] bytes, string key)
    {
        var subkeys = _keyCipherHandler.GetSubKeys(key);
        throw new NotImplementedException();
    }
}