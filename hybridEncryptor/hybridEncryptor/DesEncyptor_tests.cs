using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    [TestFixture]
    class DesEncyptor_tests
    {
        [Test]
        public void DesEncrpytAndDecryptTest()
        {
            DesEncryptor des = new DesEncryptor();
            byte[] key = des.GetKey();
            byte[] IV = des.GetIV();
            string testString = "dit is een test string";
            byte[] output = des.Encrpyt(testString);
            des.SetKey(key);
            des.SetIV(IV);
            Assert.AreEqual(testString,des.Decrpyt(output));
        }
    }
}
