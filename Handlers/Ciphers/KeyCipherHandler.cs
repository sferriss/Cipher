using Cipher.Handlers.Ciphers.Interfaces;

namespace Cipher.Handlers.Ciphers;

public class KeyCipherHandler : IKeyCipherHandler
{
    public string[] GetSubKeys(string key)
    {
        var bytesKey = key.Select(Convert.ToByte).ToArray();
        var reverseKey = key.Reverse().Select(Convert.ToByte).ToArray();
        
        var keyWithLeftShift = bytesKey.Select(x => (byte)(x << 2)).ToArray();

        var reverseAndLeftShift = reverseKey.Zip(keyWithLeftShift, (x, y) => new Tuple<byte, byte>(x, y)).ToArray();
        
        var withXor = reverseAndLeftShift.Select(x => (byte)(x.Item1 ^ x.Item2)).ToArray();
        
        var reverseAndXor = reverseKey.Zip(withXor, (x, y) => new Tuple<byte, byte>(x, y)).ToArray();
        var leftShiftAndXorReverse = withXor.Zip(keyWithLeftShift.Reverse(), (x, y) => new Tuple<byte, byte>(x, y)).ToArray();

        var combination1 = JoinValues(reverseAndXor);
        var combination2 = JoinValues(leftShiftAndXorReverse);

        combination1.AddRange(combination2);
        
        return combination1.ToArray();
    }

    private static List<string> JoinValues(IEnumerable<Tuple<byte, byte>> values)
    {
        var result = new List<string>();
        var isTrue = true;
        
        foreach (var x in values)
        {
            string str;
            var item1 = (char)x.Item1;
            var item2 = (char)x.Item2;
            if(isTrue)
            {
                str = item1 + item2.ToString();
                result.Add(str);
                isTrue = false;
                continue;
            }
            
            str = item2.ToString() + item1;
            result.Add(str);
            isTrue = true;
        }

        return result;
    }
}