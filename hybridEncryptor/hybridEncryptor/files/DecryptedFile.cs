using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class DecryptedFile
    {
        private byte[] file;
        private byte[] hash;
        public DecryptedFile(byte[] file, byte[] hash)
        {
            this.file = file;
            this.hash = hash;
        }
        public byte[] GetFile()
        {
            return file;
        }
        public bool CompareHash(byte[] fileToCompare)
        {
            byte[] compare = SHA1.Create().ComputeHash(fileToCompare);
            return (compare.SequenceEqual(hash));
        }
    }
}
