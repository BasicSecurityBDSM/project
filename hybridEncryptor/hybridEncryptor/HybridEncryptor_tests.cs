using NUnit.Framework;
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

            HybridEncryptor.EncryptedFile encrypted;
            encrypted = hybrid.Encrypt(testFile,recieverPublic,senderPrivate);

            HybridEncryptor.DecryptedFile decrypted;
            decrypted = hybrid.Decrypt(encrypted,senderPrivate, recieverPrivate);

            Assert.IsTrue(decrypted.CompareHash(testFile));
            Assert.AreEqual(testFile, decrypted.GetFile());
        }
    }
}
