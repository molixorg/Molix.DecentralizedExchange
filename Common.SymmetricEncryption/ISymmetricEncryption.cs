using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SymmetricEncryption
{
    public interface ISymmetricEncryption
    {
        byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes);
        byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes);
        string Encrypt(string input, string password);
        string Decrypt(string input, string password);
    }
}
