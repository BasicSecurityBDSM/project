using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            txtFile.Generate(file);
            txtDesKey.Generate(desKey);
            txtDesIV.Generate(desIV);
            txtHash.Generate(hash);
        }
        public TxtEncryptedFile(string folderPath) : this()
        {
            txtFile.Load(folderPath + "\\txtFile.txt");
            txtDesKey.Load(folderPath + "\\txtDesKey.txt");
            txtDesIV.Load(folderPath + "\\txtDesIV.txt");
            txtHash.Load(folderPath + "\\txtHash.txt");
        }
        public TxtEncryptedFile(string filePath, string desKeyPath, string desIVPath, string hashPath) : this()
        {
            txtFile.Load(filePath);
            txtDesKey.Load(desKeyPath);
            txtDesIV.Load(desIVPath);
            txtHash.Load(hashPath);
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
            txtFile.Save(path + "\\txtFile.txt");
            txtDesKey.Save(path + "\\txtDesKey.txt");
            txtDesIV.Save(path + "\\txtDesIV.txt");
            txtHash.Save(path + "\\txtHash.txt");
        }
    }
}
