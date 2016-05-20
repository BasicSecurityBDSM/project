using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class EncryptedFile
    {
        protected byte[] file;
        protected byte[] desKey;
        protected byte[] desIV;
        protected byte[] hash;
        public EncryptedFile(){}
        public EncryptedFile(byte[] file, byte[] desKey, byte[] desIV, byte[] hash)
        {
            this.file = file;
            this.desKey = desKey;
            this.desIV = desIV;
            this.hash = hash;
        }
        public virtual byte[] GetFile() {
            return file;
        }
        public virtual byte[] GetDesKey()
        {
            return desKey;
        }
        public virtual byte[] GetDesIV()
        {
            return desIV;
        }
        public virtual byte[] GetHash()
        {
            return hash;
        }
        public virtual void save(string path)
        {
        }
    }
}
