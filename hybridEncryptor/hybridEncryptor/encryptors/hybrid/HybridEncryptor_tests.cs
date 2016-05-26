using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace hybridEncryptor
{
    [TestFixture]
    class HybridEncryptor_tests
    {
        private HybridEncryptor hybrid;
        Random random;
        byte[] testFile;
        string path;
        string senderPrivate; string senderPublic;
        string recieverPrivate; string recieverPublic;
        [SetUp]
        public void setup()
        {
            //setup encryptor and random
            hybrid = new HybridEncryptor();
            random = new Random();
            //fill the testfile with random bytes
            testFile = new byte[(int)Math.Pow(1000, 1)];
            random.NextBytes(testFile);
            //set the keys
            senderPrivate = hybrid.GetRsaKey(true);
            senderPublic = hybrid.GetRsaKey();
            hybrid.GenerateRsaKey();
            recieverPrivate = hybrid.GetRsaKey(true);
            recieverPublic = hybrid.GetRsaKey();
            //set the path and create the folder
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testfiles";
            Directory.CreateDirectory(path);
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash()
        {
            //encrypt the file
            EncryptedFile encrypted;
            encrypted = hybrid.Encrypt(testFile,recieverPublic,senderPrivate);
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile()
        {
            //encrypt the file
            WavEncryptedFile encrypted;
            encrypted = WavEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_txtFile()
        {
            //encrypt the file
            TxtEncryptedFile encrypted;
            encrypted = TxtEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_imgFile()
        {
            //encrypt the file
            ImgEncryptedFile encrypted;
            encrypted = ImgEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_wavFile_SaveAndLoad()
        {
            //encrypt the file
            WavEncryptedFile encryptedS;
            encryptedS = WavEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //save the file
            encryptedS.save(path);
            //load the file
            WavEncryptedFile encryptedL;
            encryptedL = new WavEncryptedFile(path);
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_txtFile_SaveAndLoad()
        {
            //encrypt the file
            TxtEncryptedFile encryptedS;
            encryptedS = TxtEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //save the file
            encryptedS.save(path);
            //load the file
            TxtEncryptedFile encryptedL;
            encryptedL = new TxtEncryptedFile(path);
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
        [Test]
        public void HybridEncryptAndDecrypt_hash_imgFile_SaveAndLoad()
        {
            //encrypt the file
            ImgEncryptedFile encryptedS;
            encryptedS = ImgEncryptedFile.FromEncryptedFile(hybrid.Encrypt(testFile, recieverPublic, senderPrivate));
            //save the file
            encryptedS.save(path);
            //load the file
            ImgEncryptedFile encryptedL;
            encryptedL = new ImgEncryptedFile(path);
            //decrypt the file
            DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encryptedL, senderPublic, recieverPrivate);
            //check the hash and the file
            Assert.IsTrue(decrypted.GetHash());
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
    }
}
