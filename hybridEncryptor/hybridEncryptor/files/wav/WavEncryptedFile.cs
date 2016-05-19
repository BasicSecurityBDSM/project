using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class WavEncryptedFile : EncryptedFile
    {
        WavFile wavFile;
        WavFile wavDesKey;
        WavFile wavDesIV;
        WavFile wavHash;
        public WavEncryptedFile()
        {
            wavFile = new WavFile();
            wavDesKey = new WavFile();
            wavDesIV = new WavFile();
            wavHash = new WavFile();
        }
        public WavEncryptedFile(byte[] file, byte[] desKey, byte[] desIV, byte[] hash) : this()
        {
            Thread fileThread = new Thread(() => wavFile.Generate(file));
            Thread desKeyThread = new Thread(() => wavDesKey.Generate(desKey));
            Thread desIVThread = new Thread(() => wavDesIV.Generate(desIV));
            Thread hashThread = new Thread(() => wavHash.Generate(hash));
            fileThread.Start();
            desKeyThread.Start();
            desIVThread.Start();
            hashThread.Start();
            fileThread.Join();
            desKeyThread.Join();
            desIVThread.Join();
            hashThread.Join();
        }
        public WavEncryptedFile(string folderPath) : this()
        {
            Thread wavFileThread = new Thread(() => wavFile.Load(folderPath + "\\wavFile.wav"));
            Thread wavDesKeyThread = new Thread(() => wavDesKey.Load(folderPath + "\\wavDesKey.wav"));
            Thread wavDesIVThread = new Thread(() => wavDesIV.Load(folderPath + "\\wavDesIV.wav"));
            Thread wavHashThread = new Thread(() => wavHash.Load(folderPath + "\\wavHash.wav"));
            wavFileThread.Start();
            wavDesKeyThread.Start();
            wavDesIVThread.Start();
            wavHashThread.Start();
            wavFileThread.Join();
            wavDesKeyThread.Join();
            wavDesIVThread.Join();
            wavHashThread.Join();
        }
        public WavEncryptedFile(string filePath, string desKeyPath, string desIVPath, string hashPath) : this()
        {
            Thread wavFileThread = new Thread(() => wavFile.Load(filePath));
            Thread wavDesKeyThread = new Thread(() => wavDesKey.Load(desKeyPath));
            Thread wavDesIVThread = new Thread(() => wavDesIV.Load(desIVPath));
            Thread wavHashThread = new Thread(() => wavHash.Load(hashPath));
            wavFileThread.Start();
            wavDesKeyThread.Start();
            wavDesIVThread.Start();
            wavHashThread.Start();
            wavFileThread.Join();
            wavDesKeyThread.Join();
            wavDesIVThread.Join();
            wavHashThread.Join();
        }
        public static WavEncryptedFile FromEncryptedFile(EncryptedFile encryptedFile)
        {
            return new WavEncryptedFile(encryptedFile.GetFile(), encryptedFile.GetDesKey(), encryptedFile.GetDesIV(), encryptedFile.GetHash());
        }
        public override byte[] GetFile()
        {
            return wavFile.GetData();
        }
        public override byte[] GetDesKey()
        {
            return wavDesKey.GetData();
        }
        public override byte[] GetDesIV()
        {
            return wavDesIV.GetData();
        }
        public override byte[] GetHash()
        {
            return wavHash.GetData();
        }
        public void save(string path)
        {
            Thread wavFileThread = new Thread(() => wavFile.Save(path + "\\wavFile.wav"));
            Thread wavDesKeyThread = new Thread(() => wavDesKey.Save(path + "\\wavDesKey.wav"));
            Thread wavDesIVThread = new Thread(() => wavDesIV.Save(path + "\\wavDesIV.wav"));
            Thread wavHashThread = new Thread(() => wavHash.Save(path + "\\wavHash.wav"));
            wavFileThread.Start();
            wavDesKeyThread.Start();
            wavDesIVThread.Start();
            wavHashThread.Start();
            wavFileThread.Join();
            wavDesKeyThread.Join();
            wavDesIVThread.Join();
            wavHashThread.Join();
        }
    }
}
