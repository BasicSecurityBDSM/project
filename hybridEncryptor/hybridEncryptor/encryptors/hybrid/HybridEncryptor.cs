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
        public EncryptedFile Encrypt(byte[] fileToEncrypt,string OtherPublicKey,string OwnPrivateKey)
        {
            //encrypt file
            string stringFile = Convert.ToBase64String(fileToEncrypt);
            byte[] file = des.Encrpyt(stringFile);
            //encrypt keys
            rsa.SetKey(OtherPublicKey);
            byte[] desKey = rsa.Encrypt(des.GetKey());
            byte[] desIV = rsa.Encrypt(des.GetIV());
            //encrypt hash
            rsa.SetKey(OwnPrivateKey);
            byte[] hash = SHA1.Create().ComputeHash(fileToEncrypt);
            hash = rsa.Encrypt(hash);
            //return file
            return new EncryptedFile(file,desKey,desIV,hash);
        }
        public DecryptedFile Decrypt(EncryptedFile fileToDecrypt,string OtherPublicKey,string OwnPrivateKey)
        {
            //decrypt keys
            rsa.SetKey(OwnPrivateKey);
            byte[] desKey = rsa.Decrypt(fileToDecrypt.GetDesKey());
            byte[] desIV = rsa.Decrypt(fileToDecrypt.GetDesIV());
            //decrypt file
            des.SetKey(desKey);
            des.SetIV(desIV);
            string stringFile = des.Decrpyt(fileToDecrypt.GetFile());
            byte[] file = Convert.FromBase64String(stringFile);
            //decrypt hash
            rsa.SetKey(OtherPublicKey);
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
    }
}
