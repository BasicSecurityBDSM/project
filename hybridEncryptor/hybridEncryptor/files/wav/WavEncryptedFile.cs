using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            wavFile.Generate(file);
            wavDesKey.Generate(desKey);
            wavDesIV.Generate(desIV);
            wavHash.Generate(hash);
        }
        public WavEncryptedFile(string folderPath) : this()
        {
            wavFile.Load(folderPath+ "\\wavFile.wav");
            wavDesKey.Load(folderPath+ "\\wavDesKey.wav");
            wavDesIV.Load(folderPath+ "\\wavDesIV.wav");
            wavHash.Load(folderPath+ "\\wavHash.wav");
        }
        public WavEncryptedFile(string filePath, string desKeyPath, string desIVPath, string hashPath) : this()
        {
            wavFile.Load(filePath);
            wavDesKey.Load(desKeyPath);
            wavDesIV.Load(desIVPath);
            wavHash.Load(hashPath);
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
            wavFile.Save(path + "\\wavFile.wav");
            wavDesKey.Save(path + "\\wavDesKey.wav");
            wavDesIV.Save(path + "\\wavDesIV.wav");
            wavHash.Save(path + "\\wavHash.wav");
        }
    }
}
