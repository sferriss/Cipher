using EncoderConsoleApp.Enums;

namespace Cipher.Handlers.Files;

public class FileHandler : IFileHandler
{
    public byte[] Read(string path)
    {
        return File.ReadAllBytes(path);
    }

    public void Write(byte[] bytes, OperationType type)
    {
        File.WriteAllBytes(
            OperationType.Decode == type
                ? @$"..\..\..\ReturnedFiles\Decoded\decode.txt"
                : @"..\..\..\ReturnedFiles\Encoded\decode.txt", bytes);
    }
}