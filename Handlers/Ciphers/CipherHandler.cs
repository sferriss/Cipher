using Cipher.Handlers.Ciphers.Interfaces;
using Cipher.Handlers.Files;
using EncoderConsoleApp.Enums;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text;

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

        //padding para deixar o tamanho do arquivo multiplo de 6
        //enviar no cabeçalho o countPadding
        int countPadding = 0;
        byte[] cipherFile = new byte[bytes.Length + 1];

        if (bytes.Length % 6 != 0)
        {
            countPadding = 6 - bytes.Length % 6;
            Array.Resize(ref bytes, bytes.Length + (6 - bytes.Length % 6));
            Array.Resize(ref cipherFile, bytes.Length + 1);
        }
       
        //tamanho final da saida = tamanho do arquivo de entrada + padding necessario + 1 byte de cabeçalho

        //corpo do cifrador em bloco
       
        int countBlock = 0;
        int qtdBlocks = bytes.Length/6;
        /*-------------------------round-----------------------------------*/

        //pega um bloco de 64 bits do arquivo de entrada
        var fileBlock = new byte[6];

        for (int ciclos = 0; ciclos < qtdBlocks; ciclos++)
        {

        
            for (int i =0, y = countBlock; i < 6; i++,y++)
            {
                fileBlock[i] = bytes[y];
            }
            
                    //permutação inicial do bloco

                    //separa em dois blocos de 32 bits

            var lBlock = new byte[3];
            var rBlock = new byte[3];

            for (int i = 0; i < 3; i++)
            {
                lBlock[i] = fileBlock[i];
            }
            int aux = 0;
            for (int i = 3; i < 6; i++)
            {
                rBlock[aux] = fileBlock[i];
                aux++;
            }

        
            #region Round
            /*


                //fluxo direita
                //passa pela função f junto com a subKey
                var returnFuntionF = FunctionF(rBlock, subkeys[0]);

                //fluxo esquerda
                //xor com a saida função f
                var xorBlock = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    xorBlock[i] = lBlock[i] ^ returnFuntionF[i];
                }
                for (int i = 0; i < 3; i++)
                {
                    lBlock[i] = Convert.ToByte(xorBlock[i]);
                }

                //swap dos lados

                var auxSwap = new byte[3];
                for (int i = 0; i < 3; i++)
                {
                    auxSwap[i] = lBlock[i];
                    lBlock[i] = rBlock[i];
                    rBlock[i] = auxSwap[i];
                }

            //inicia novo round

            */
            #endregion

            (byte[], byte[]) retornoRound = Round(lBlock, rBlock, subkeys,0,OperationType.Encode);

            byte[] cipherBlock = new byte[6];
            for (int i = 0; i < 3; i++)
            {
                cipherBlock[i] = lBlock[i];
            }
            for (int i = 3, y = 0; i < 6; i++,y++)
            {
                cipherBlock[i] = rBlock[y];
            }

            for (int i = 0, y = countBlock+1; i < 6; i++,y++)
            {
                cipherFile[y] = cipherBlock[i];
            }

            countBlock += 6;
        }
        cipherFile[0] =  Convert.ToByte(countPadding);


        _fileHandler.Write(cipherFile, OperationType.Encode);
    }

    public void Decode(byte[] bytes, string key)
    {
        var subkeys = _keyCipherHandler.GetSubKeys(key);

        byte cabecalho = bytes[0];

        byte[] returnCypher = new byte[bytes.Length];
        byte[] decypherFile = new byte[bytes.Length-1];
        
        //copia o array sem o cabeçalho
        Array.Copy(bytes, 1, decypherFile, 0,bytes.Length-1);

        //corpo do cifrador em bloco

        int countBlock = 0;
        int qtdBlocks = bytes.Length / 6;
        /*-------------------------round-----------------------------------*/

        //pega um bloco de 64 bits do arquivo de entrada
        var fileBlock = new byte[6];

        for (int ciclos = 0; ciclos < qtdBlocks; ciclos++)
        {


            for (int i = 0, y = countBlock; i < 6; i++, y++)
            {
                fileBlock[i] = decypherFile[y];
            }

            //separa em dois blocos de 32 bits

            var lBlock = new byte[3];
            var rBlock = new byte[3];

            for (int i = 0; i < 3; i++)
            {
                lBlock[i] = fileBlock[i];
            }
            int aux = 0;
            for (int i = 3; i < 6; i++)
            {
                rBlock[aux] = fileBlock[i];
                aux++;
            }

            (byte[], byte[]) retornoRound = Round(lBlock, rBlock, subkeys, 0, OperationType.Decode);

            byte[] cipherBlock = new byte[6];
            for (int i = 0; i < 3; i++)
            {
                cipherBlock[i] = lBlock[i];
            }
            for (int i = 3, y = 0; i < 6; i++, y++)
            {
                cipherBlock[i] = rBlock[y];
            }

            for (int i = 0, y = countBlock; i < 6; i++, y++)
            {
                returnCypher[y] = cipherBlock[i];
            }

            countBlock += 6;
        }

        //remove padding
        var qtdPadding = Convert.ToInt32(cabecalho);
        //verificar tamnho dos arrays, algo estranho
        Array.Resize(ref returnCypher, (returnCypher.Length-1) -qtdPadding);

        _fileHandler.Write(returnCypher, OperationType.Decode);
    }
    
    private byte[] FunctionF(byte[] rBlock, string subKey) {


        var s = subKey.Select(Convert.ToByte).ToArray();

        Array.Resize(ref s, rBlock.Length);

        byte[] retF = new byte[rBlock.Length];

        for (int i = 0; i < rBlock.Length; i++)
        {
            retF[i] = (byte)(s[i] ^ rBlock[i]);

        }

        return retF;
    }

    private (byte[], byte[]) Round(byte[] lBlock, byte[] rBlock,string[] subKeys,int round,OperationType operacao) {
        
        if (round >= 8) {
            return (lBlock,rBlock);
        }
        string subKeyRound;
        if (operacao == OperationType.Encode) {
            subKeyRound = subKeys[round];
        }
        else
        {
            subKeyRound = subKeys[(subKeys.Length-1) - round];
        }

        //fluxo direita
        //passa pela função f junto com a subKey
        byte[] returnFuntionF =new byte[3];
        if (operacao == OperationType.Encode)
        {
            returnFuntionF = FunctionF(rBlock, subKeyRound);
        }
        else
        {
            returnFuntionF = FunctionF(lBlock, subKeyRound);
        }

        //fluxo esquerda
        //xor com a saida função f
        var xorBlock = new byte[3];
        
        if (operacao == OperationType.Encode)
        {

            for (int i = 0; i < 3; i++)
            {
                xorBlock[i] = (byte)(lBlock[i] ^ returnFuntionF[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                lBlock[i] = xorBlock[i];
            }

        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                xorBlock[i] = (byte)(rBlock[i] ^ returnFuntionF[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                rBlock[i] = xorBlock[i];
            }
        }

        //swap dos lados
        var auxSwap = new byte[3];
        if (operacao == OperationType.Encode)
        {
            for (int i = 0; i < 3; i++)
            {
                auxSwap[i] = lBlock[i];
                lBlock[i] = rBlock[i];
                rBlock[i] = auxSwap[i];
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                auxSwap[i] = rBlock[i];
                rBlock[i] = lBlock[i];
                lBlock[i] = auxSwap[i];
            }
        }

        //inicia novo round


        return Round(lBlock,rBlock,subKeys,round +1, operacao);
    }
    
}