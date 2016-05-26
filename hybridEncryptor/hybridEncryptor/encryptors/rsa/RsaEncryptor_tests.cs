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
            //setup the keys
            string privateKey = rsa.GetKey(true);
            string publicKey = rsa.GetKey(false);
            string testString = "dit is een test string";
            //create byte[] from string (fake it being a file)
            byte[] dataToEncrypt = ByteConverter.GetBytes(testString);
            //generate new key to make sure the setkey works
            rsa.generateNewKey();
            //set key
            rsa.SetKey(publicKey);
            //encrypt
            byte[] encrypted = rsa.Encrypt(dataToEncrypt,false);
            //generate new key to make sure the setkey works
            rsa.generateNewKey();
            //set key
            rsa.SetKey(privateKey);
            //decrypt
            byte[] decrypted = rsa.Decrypt(encrypted);
            //check if it's the same
            Assert.AreEqual(ByteConverter.GetString(decrypted), testString);
        }
        [Test]
        public void RsaKeyGenTest()
        {
            //get the key
            string key1 = rsa.GetKey(true);
            //generate a new one
            rsa.generateNewKey();
            //set second key
            string key2 = rsa.GetKey(true);
            //generate a new one
            rsa.generateNewKey();
            //set third key
            string key3 = rsa.GetKey(true);
            //check if any of them are the same
            Assert.AreNotEqual(key1, key2);
            Assert.AreNotEqual(key1, key3);
            Assert.AreNotEqual(key2, key3);
        }
    }
}
