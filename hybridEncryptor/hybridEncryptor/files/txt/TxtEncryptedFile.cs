using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class TxtEncryptedFile : EncryptedFile
    {
        TxtFile txtFile;
        TxtFile txtDesKey;
        TxtFile txtDesIV;
        TxtFile txtHash;
        public TxtEncryptedFile()
        {
            txtFile = new TxtFile();
            txtDesKey = new TxtFile();
            txtDesIV = new TxtFile();
            txtHash = new TxtFile();
        }
        public TxtEncryptedFile(byte[] file, byte[] desKey, byte[] desIV, byte[] hash) : this()
        {
            Thread fileThread = new Thread(() => txtFile.Generate(file));
            Thread desKeyThread = new Thread(() => txtDesKey.Generate(desKey));
            Thread desIVThread = new Thread(() => txtDesIV.Generate(desIV));
            Thread hashThread = new Thread(() => txtHash.Generate(hash));
            fileThread.Start();
            desKeyThread.Start();
            desIVThread.Start();
            hashThread.Start();
            fileThread.Join();
            desKeyThread.Join();
            desIVThread.Join();
            hashThread.Join();
        }
        public TxtEncryptedFile(string folderPath) : this()
        {
            Thread txtFileThread = new Thread(() => txtFile.Load(folderPath + "\\txtFile.txt"));
            Thread txtDesKeyThread = new Thread(() => txtDesKey.Load(folderPath + "\\txtDesKey.txt"));
            Thread txtDesIVThread = new Thread(() => txtDesIV.Load(folderPath + "\\txtDesIV.txt"));
            Thread txtHashThread = new Thread(() => txtHash.Load(folderPath + "\\txtHash.txt"));
            txtFileThread.Start();
            txtDesKeyThread.Start();
            txtDesIVThread.Start();
            txtHashThread.Start();
            txtFileThread.Join();
            txtDesKeyThread.Join();
            txtDesIVThread.Join();
            txtHashThread.Join();
        }
        public TxtEncryptedFile(string filePath, string desKeyPath, string desIVPath, string hashPath) : this()
        {
            Thread txtFileThread = new Thread(() => txtFile.Load(filePath));
            Thread txtDesKeyThread = new Thread(() => txtDesKey.Load(desKeyPath));
            Thread txtDesIVThread = new Thread(() => txtDesIV.Load(desIVPath));
            Thread txtHashThread = new Thread(() => txtHash.Load(hashPath));
            txtFileThread.Start();
            txtDesKeyThread.Start();
            txtDesIVThread.Start();
            txtHashThread.Start();
            txtFileThread.Join();
            txtDesKeyThread.Join();
            txtDesIVThread.Join();
            txtHashThread.Join();
        }
        public static TxtEncryptedFile FromEncryptedFile(EncryptedFile encryptedFile)
        {
            return new TxtEncryptedFile(encryptedFile.GetFile(), encryptedFile.GetDesKey(), encryptedFile.GetDesIV(), encryptedFile.GetHash());
        }
        public override byte[] GetFile()
        {
            return txtFile.GetData();
        }
        public override byte[] GetDesKey()
        {
            return txtDesKey.GetData();
        }
        public override byte[] GetDesIV()
        {
            return txtDesIV.GetData();
        }
        public override byte[] GetHash()
        {
            return txtHash.GetData();
        }
        public void save(string path)
        {
            Thread txtFileThread = new Thread(() => txtFile.Save(path + "\\txtFile.txt"));
            Thread txtDesKeyThread = new Thread(() => txtDesKey.Save(path + "\\txtDesKey.txt"));
            Thread txtDesIVThread = new Thread(() => txtDesIV.Save(path + "\\txtDesIV.txt"));
            Thread txtHashThread = new Thread(() => txtHash.Save(path + "\\txtHash.txt"));
            txtFileThread.Start();
            txtDesKeyThread.Start();
            txtDesIVThread.Start();
            txtHashThread.Start();
            txtFileThread.Join();
            txtDesKeyThread.Join();
            txtDesIVThread.Join();
            txtHashThread.Join();
        }
    }
}
