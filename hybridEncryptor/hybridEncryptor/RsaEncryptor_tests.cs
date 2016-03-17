using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    [TestFixture]
    class RsaEncyptor_tests
    {
        [Test]
        public void RsaEncrpytAndDecryptTest()
        {
            RsaEncryptor rsa = new RsaEncryptor();
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            string privateKey = rsa.GetKey(true);
            string publicKey = rsa.GetKey(false);
            string testString = "dit is een test string";
            byte[] dataToEncrypt = ByteConverter.GetBytes(testString);
            rsa.SetKey(publicKey);
            byte[] encrypted = rsa.Encrypt(dataToEncrypt);
            rsa.SetKey(privateKey);
            byte[] decrypted = rsa.Decrypt(encrypted);
            Assert.AreEqual(ByteConverter.GetString(decrypted), testString);
        }
    }
}
