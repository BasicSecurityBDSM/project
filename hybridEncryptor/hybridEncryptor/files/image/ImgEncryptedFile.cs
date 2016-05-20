using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class ImgEncryptedFile : EncryptedFile
    {
        ImgFile imgFile;
        ImgFile imgDesKey;
        ImgFile imgDesIV;
        ImgFile imgHash;
        public ImgEncryptedFile()
        {
            imgFile = new ImgFile();
            imgDesKey = new ImgFile();
            imgDesIV = new ImgFile();
            imgHash = new ImgFile();
        }
        public ImgEncryptedFile(byte[] file, byte[] desKey, byte[] desIV, byte[] hash) : this()
        {
            Thread fileThread = new Thread(() => imgFile.Generate(file));
            Thread desKeyThread = new Thread(() => imgDesKey.Generate(desKey));
            Thread desIVThread = new Thread(() => imgDesIV.Generate(desIV));
            Thread hashThread = new Thread(() => imgHash.Generate(hash));
            fileThread.Start();
            desKeyThread.Start();
            desIVThread.Start();
            hashThread.Start();
            fileThread.Join();
            desKeyThread.Join();
            desIVThread.Join();
            hashThread.Join();
        }
        public ImgEncryptedFile(string folderPath) : this()
        {
            Thread imgFilethread = new Thread(() => imgFile.Load(folderPath + "\\imgFile.bmp"));
            Thread imgDesKeythread = new Thread(() => imgDesKey.Load(folderPath + "\\imgDesKey.bmp"));
            Thread imgDesIVthread = new Thread(() => imgDesIV.Load(folderPath + "\\imgDesIV.bmp"));
            Thread imgHashthread = new Thread(() => imgHash.Load(folderPath + "\\imgHash.bmp"));
            imgFilethread.Start();
            imgDesKeythread.Start();
            imgDesIVthread.Start();
            imgHashthread.Start();
            imgFilethread.Join();
            imgDesKeythread.Join();
            imgDesIVthread.Join();
            imgHashthread.Join();
        }
        public ImgEncryptedFile(string filePath, string desKeyPath, string desIVPath, string hashPath) : this()
        {
            Thread imgFileThread = new Thread(() => imgFile.Load(filePath));
            Thread imgDesKeyThread = new Thread(() => imgDesKey.Load(desKeyPath));
            Thread imgDesIVThread = new Thread(() => imgDesIV.Load(desIVPath));
            Thread imgHashThread = new Thread(() => imgHash.Load(hashPath));
            imgFileThread.Start();
            imgDesKeyThread.Start();
            imgDesIVThread.Start();
            imgHashThread.Start();
            imgFileThread.Join();
            imgDesKeyThread.Join();
            imgDesIVThread.Join();
            imgHashThread.Join();
        }
        public static ImgEncryptedFile FromEncryptedFile(EncryptedFile encryptedFile)
        {
            return new ImgEncryptedFile(encryptedFile.GetFile(), encryptedFile.GetDesKey(), encryptedFile.GetDesIV(), encryptedFile.GetHash());
        }
        public override byte[] GetFile()
        {
            return imgFile.GetData();
        }
        public override byte[] GetDesKey()
        {
            return imgDesKey.GetData();
        }
        public override byte[] GetDesIV()
        {
            return imgDesIV.GetData();
        }
        public override byte[] GetHash()
        {
            return imgHash.GetData();
        }
        public override void save(string path)
        {
            Thread imgFileThread = new Thread(() => imgFile.Save(path + "\\imgFile.bmp"));
            Thread imgDesKeyThread = new Thread(() => imgDesKey.Save(path + "\\imgDesKey.bmp"));
            Thread imgDesIVThread = new Thread(() => imgDesIV.Save(path + "\\imgDesIV.bmp"));
            Thread imgHashThread = new Thread(() => imgHash.Save(path + "\\imgHash.bmp"));
            imgFileThread.Start();
            imgDesKeyThread.Start();
            imgDesIVThread.Start();
            imgHashThread.Start();
            imgFileThread.Join();
            imgDesKeyThread.Join();
            imgDesIVThread.Join();
            imgHashThread.Join();
        }

    }
}
