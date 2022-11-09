using Cipher.Handlers.Ciphers.Interfaces;
using Cipher.Handlers.Files;

namespace Cipher;

public class RunProject
{
    private readonly IFileHandler _fileHandler;
    private readonly ICipherHandler _cipherHandler;

    public RunProject(IFileHandler fileHandler, ICipherHandler cipherHandler)
    {
        _fileHandler = fileHandler;
        _cipherHandler = cipherHandler;
    }

    public void Run()
    {
        var option = "";
        
        while (option != "2")
        {
            option = CallMainMenu();

            switch (option)
            {
                case "0":
                {
                    option = CallKeyMenu();
                    var key = option;

                    if (key?.Length != 4)
                    {
                        Console.WriteLine("\nTamanho de chave inválido \n");
                        break;
                    }
                
                    option = CallPathMenu();
                    
                    var file = _fileHandler.Read(option!);
                    
                    Console.WriteLine("\nCodificando... \n");
                    //decoder.Encode(file, divider);
                    _cipherHandler.Encode(file, key);
                    Console.WriteLine(@"Salvo em: EncoderConsoleApp\EncoderConsoleApp\ReturnedFiles\Encoded\encoded.txt");
                    break;
                }
                case "1":
                {
                    option = CallOptionsPathMenu();

                    switch (option)
                    {
                        case "0":
                        {
                            option = CallKeyMenu();
                            var key = option;
                            
                            if (key?.Length != 4)
                            {
                                Console.WriteLine("\nTamanho de chave inválido \n");
                                break;
                            }
                            
                            const string path = @"..\..\..\ReturnedFiles\Encoded\encoded.txt";
                            var file = _fileHandler.Read(path);

                            Console.WriteLine("\nDecodificando... \n");
                            _cipherHandler.Decode(file, key);
                            Console.WriteLine(@"Salvo em: EncoderConsoleApp\EncoderConsoleApp\ReturnedFiles\Decoded\decode.txt");
                            break;
                        }
                        case "1":
                        {
                            option = CallPathMenu();
                            var path = option;
                            
                            option = CallKeyMenu();
                            var key = option;
                            
                            if (key?.Length != 4)
                            {
                                Console.WriteLine("\nTamanho de chave inválido \n");
                                break;
                            }
                            var file = _fileHandler.Read(path);

                            Console.WriteLine("\nDecodificando... \n");
                            _cipherHandler.Decode(file, key);
                            Console.WriteLine(@"Salvo em: EncoderConsoleApp\EncoderConsoleApp\ReturnedFiles\Decoded\decode.txt");
                            break;
                        }
                        default:
                            Console.WriteLine("Opção inválida! \n");
                            break;
                    }

                    break;
                }
                case "2":
                    Console.Clear();
                    Console.WriteLine("Encerrando \n");
                    break;
                default:
                    Console.WriteLine("Opção inválida! \n");
                    break;
            }
        }
    }
    
    private static string? CallMainMenu()
    {
        Console.Write("\nSelecione uma opção: \n" +
                      "0 - Codificar \n" +
                      "1 - Decodificar \n" +
                      "2 - Sair \n");

        return Console.ReadLine();
    }
    
    private static string? CallKeyMenu()
    {
        Console.Write("\nInforme uma chave de 4 caracteres: \n");

        return Console.ReadLine();
    }
    
    private static string? CallPathMenu()
    {
        Console.Write("\nInforme o caminho do arquivo: \n");

        return Console.ReadLine();
    }
    
    private static string? CallOptionsPathMenu()
    {
        Console.Write("\nSelecione uma opção: \n" +
                      "0 - Caminho default (ultima codificação feita) \n" +
                      "1 - Digitar um caminho diferente \n");

        return Console.ReadLine();
    }
}