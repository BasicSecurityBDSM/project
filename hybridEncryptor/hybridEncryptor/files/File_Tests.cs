using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    [TestFixture]
    class File_Tests
    {
        [Test]
        public void WavFileData()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            WavFile wav = new WavFile();
            wav.Generate(testFile);
            byte[] outFile = wav.GetData();
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void TxtFileData()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            TxtFile txt = new TxtFile();
            txt.Generate(testFile);
            byte[] outFile = txt.GetData();
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void WavFileData_SaveAndLoad()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            WavFile wavS = new WavFile();
            wavS.Generate(testFile);
            wavS.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testFile.wav");
            WavFile wavL = new WavFile();
            wavL.Load(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testFile.wav");
            byte[] outFile = wavL.GetData();
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void TxtFileData_SaveAndLoad()
        {
            string testString = "dit is een teststring";
            byte[] testFile = Encoding.ASCII.GetBytes(testString);
            TxtFile txtS = new TxtFile();
            txtS.Generate(testFile);
            txtS.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/testFile.txt");
            TxtFile txtL = new TxtFile();
            txtL.Load(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/testFile.txt");
            byte[] outFile = txtL.GetData();
            Assert.AreEqual(outFile, testFile);
        }
    }
}
