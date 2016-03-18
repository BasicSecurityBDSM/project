using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class HybridEncryptor
    {
        private DesEncryptor des;
        private RsaEncryptor rsa;

        public HybridEncryptor()
        {
            des = new DesEncryptor();
            rsa = new RsaEncryptor();
            
        }
        public EncryptedFile Encrypt(byte[] fileToEncrypt,string publicOther,string privateMe)
        {
            //encrypt file
            string stringFile = Convert.ToBase64String(fileToEncrypt);
            byte[] file = des.Encrpyt(stringFile);
            //encrypt keys
            rsa.SetKey(publicOther);
            byte[] desKey = rsa.Encrypt(des.GetKey());
            byte[] desIV = rsa.Encrypt(des.GetIV());
            //encrypt hash
            rsa.SetKey(privateMe);
            byte[] hash = SHA1.Create().ComputeHash(fileToEncrypt);
            hash = rsa.Encrypt(hash);
            //return file
            return new EncryptedFile(file,desKey,desIV,hash);
        }
        public DecryptedFile Decrypt(EncryptedFile fileToDecrypt,string publicOther,string privateMe)
        {
            //decrypt keys
            rsa.SetKey(privateMe);
            byte[] desKey = rsa.Decrypt(fileToDecrypt.GetDesKey());
            byte[] desIV = rsa.Decrypt(fileToDecrypt.GetDesIV());
            //decrypt file
            des.SetKey(desKey);
            des.SetIV(desIV);
            string stringFile = des.Decrpyt(fileToDecrypt.GetFile());
            byte[] file = Convert.FromBase64String(stringFile);
            //decrypt hash
            rsa.SetKey(publicOther);
            byte[] hash = rsa.Decrypt(fileToDecrypt.GetHash());
            return new DecryptedFile(file,hash);

        }
        public void GenerateRsaKey()
        {
            rsa.generateNewKey();
        }
        public string GetRsaKey(bool includePrivate = false)
        {
            return rsa.GetKey(includePrivate);
        }
        public class EncryptedFile
        {
            private byte[] file;
            private byte[] desKey;
            private byte[] desIV;
            private byte[] hash;
            public EncryptedFile(byte[] file, byte[] desKey,byte[] desIV, byte[] hash)
            {
                this.file = file;
                this.desKey = desKey;
                this.desIV = desIV;
                this.hash = hash;
            }
            public byte[] GetFile()
            {
                return file;
            }
            public byte[] GetDesKey()
            {
                return desKey;
            }
            public byte[] GetDesIV()
            {
                return desIV;
            }
            public byte[] GetHash()
            {
                return hash;
            }
        }
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
}
