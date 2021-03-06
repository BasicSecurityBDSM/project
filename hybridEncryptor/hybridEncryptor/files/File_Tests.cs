﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    [TestFixture]
    class File_Tests
    {
        string testString;
        byte[] testFile;
        string path;
        [SetUp]
        public void setup()
        {
            testString = "dit is een teststring";
            testFile = Encoding.ASCII.GetBytes(testString);
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testfiles";
            Directory.CreateDirectory(path);
        }
        [Test]
        public void WavFileData()
        {
            //put the data in the file
            WavFile wav = new WavFile();
            wav.Generate(testFile);
            //pull data out
            byte[] outFile = wav.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void TxtFileData()
        {
            //put the data in the file
            TxtFile txt = new TxtFile();
            txt.Generate(testFile);
            //pull data out
            byte[] outFile = txt.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void ImgFileData()
        {
            //put the data in the file
            ImgFile img = new ImgFile();
            img.Generate(testFile);
            //pull data out
            byte[] outFile = img.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void WavFileData_SaveAndLoad()
        {
            //put the data in the file
            WavFile wavS = new WavFile();
            wavS.Generate(testFile);
            //save the file
            wavS.Save(path + "\\testFile.wav");
            //load the file
            WavFile wavL = new WavFile();
            wavL.Load(path + "\\testFile.wav");
            //pull data out
            byte[] outFile = wavL.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void TxtFileData_SaveAndLoad()
        {
            //put the data in the file
            TxtFile txtS = new TxtFile();
            txtS.Generate(testFile);
            //save the file
            txtS.Save(path + "/testFile.txt");
            //load the file
            TxtFile txtL = new TxtFile();
            txtL.Load(path + "/testFile.txt");
            //pull data out
            byte[] outFile = txtL.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
        [Test]
        public void ImgFileData_SaveAndLoad()
        {
            //put the data in the file
            ImgFile imgS = new ImgFile();
            imgS.Generate(testFile);
            //save the file
            imgS.Save(path + "/testFile.bmp");
            //load the file
            ImgFile imgL = new ImgFile();
            imgL.Load(path + "/testFile.bmp");
            //pull data out
            byte[] outFile = imgL.GetData();
            //check if it's still the same
            Assert.AreEqual(outFile, testFile);
        }
    }
}
