using EncoderConsoleApp.Enums;

namespace Cipher.Handlers.Files;

public interface IFileHandler
{
    byte[] Read(string path);

    void  Write(byte[] bytes, OperationType type);
}