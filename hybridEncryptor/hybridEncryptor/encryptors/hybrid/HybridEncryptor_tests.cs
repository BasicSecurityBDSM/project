using NUnit.Framework;
using System;
using System.Text;

namespace hybridEncryptor
{
    [TestFixture]
    class HybridEncryptor_tests
    {
        private HybridEncryptor hybrid;
        [SetUp]
        public void setup()
        {
            hybrid = new HybridEncryptor();
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            EncryptedFile encrypted;
            encrypted = hybrid.Encrypt(testFile,recieverPublic,senderPrivate);

            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted,senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            WavEncryptedFile encrypted;
            encrypted = WavEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile_SaveAndLoad()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            WavEncryptedFile encryptedS;
            encryptedS = WavEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            encryptedS.save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            WavEncryptedFile encryptedL;
            encryptedL = new WavEncryptedFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
    }
}
