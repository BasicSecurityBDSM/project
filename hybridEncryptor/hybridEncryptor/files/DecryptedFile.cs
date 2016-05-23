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
        RsaEncryptor rsa;
        private byte[] file;
        private bool hash;
        public DecryptedFile(byte[] file, bool hash)
        {
            this.file = file;
            this.hash = hash;
        }
        public byte[] GetFile()
        {
            return file;
        }
        public bool GetHash()
        {
            return hash;
        }
    }
}
