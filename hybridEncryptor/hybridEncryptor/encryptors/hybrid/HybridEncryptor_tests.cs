using NUnit.Framework;
using System;
using System.Text;

namespace hybridEncryptor
{
    [TestFixture]
    class HybridEncryptor_tests
    {
        private HybridEncryptor hybrid;
        Random random;
        byte[] testFile;
        [SetUp]
        public void setup()
        {
            hybrid = new HybridEncryptor();
            random = new Random();
            testFile = new byte[(int)Math.Pow(1, 1)];
            random.NextBytes(testFile);
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash()
        {
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            EncryptedFile encrypted;
            encrypted = hybrid.Encrypt(testFile,recieverPublic,senderPrivate);

            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPublic, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile()
        {
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
        public void HybridEncryptAndDecrypt_hash_txtFile()
        {
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            TxtEncryptedFile encrypted;
            encrypted = TxtEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));

            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_imgFile()
        {
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            ImgEncryptedFile encrypted;
            encrypted = ImgEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));

            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile_SaveAndLoad()
        {
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
        [Test]
        public void HybridEncryptAndDecrypt_hash_txtFile_SaveAndLoad()
        {
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            TxtEncryptedFile encryptedS;
            encryptedS = TxtEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            encryptedS.save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            TxtEncryptedFile encryptedL;
            encryptedL = new TxtEncryptedFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_imgFile_SaveAndLoad()
        {
            string senderPrivate = hybrid.GetRsaKey(true);
            string senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            string recieverPrivate = hybrid.GetRsaKey(true);
            string recieverPublic = hybrid.GetRsaKey();

            ImgEncryptedFile encryptedS;
            encryptedS = ImgEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            encryptedS.save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            ImgEncryptedFile encryptedL;
            encryptedL = new ImgEncryptedFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
    }
}
