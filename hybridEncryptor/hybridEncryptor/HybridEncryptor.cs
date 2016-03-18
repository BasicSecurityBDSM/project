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
        private SHA1 sha;

        private byte[] byteFile;
        private string stringFile;

        private byte[] encryptedFile;
        private byte[] encryptedDesKey;
        private byte[] encryptedDesIV;

        private byte[] fileHash;
        private byte[] encryptedHash;
        public HybridEncryptor()
        {
            des = new DesEncryptor();
            rsa = new RsaEncryptor();
        }
        public void SetFileName(string filename)
        {
            byteFile = File.ReadAllBytes(filename);
            stringFile = byteFile.ToString();
        }
        public void Encrypt(string publicOntvanger,string privateZender)
        {
            //encrypt file
            encryptedFile = des.Encrpyt(stringFile);
            //encrypt keys
            rsa.SetKey(publicOntvanger);
            encryptedDesKey = rsa.Encrypt(des.GetKey());
            encryptedDesIV = rsa.Encrypt(des.GetIV());
            //encrypt hash
            fileHash = sha.ComputeHash(byteFile);
            rsa.SetKey(privateZender);
            encryptedHash = rsa.Encrypt(fileHash);
        }
        public string GetEncryptedFile()
        {
            return encryptedFile.ToString();
        }
        public string GetEncryptedHash()
        {
            return encryptedHash.ToString();
        }
    }
}
