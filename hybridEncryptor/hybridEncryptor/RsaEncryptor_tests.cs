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
        private RsaEncryptor rsa;
        [SetUp]
        public void setup()
        {
            rsa = new RsaEncryptor();
        }
        [Test]
        public void RsaEncrpytAndDecryptTest()
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            string privateKey = rsa.GetKey(true);
            string publicKey = rsa.GetKey(false);
            string testString = "dit is een test string";
            byte[] dataToEncrypt = ByteConverter.GetBytes(testString);
            rsa.generateNewKey();
            rsa.SetKey(publicKey);
            byte[] encrypted = rsa.Encrypt(dataToEncrypt);
            rsa.generateNewKey();
            rsa.SetKey(privateKey);
            byte[] decrypted = rsa.Decrypt(encrypted);
            Assert.AreEqual(ByteConverter.GetString(decrypted), testString);
        }
        [Test]
        public void RsaKeyGenTest()
        {
            string key1 = rsa.GetKey(true);
            rsa.generateNewKey();
            string key2 = rsa.GetKey(true);
            rsa.generateNewKey();
            string key3 = rsa.GetKey(true);
            Assert.AreNotEqual(key1, key2);
            Assert.AreNotEqual(key1, key3);
            Assert.AreNotEqual(key2, key3);
        }
    }
}
