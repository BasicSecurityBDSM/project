using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    abstract class File
    {
        public abstract void Generate(byte[] dataToInsert);
        public abstract byte[] GetData();
        public abstract void Load(string filePath);
        public abstract void Save(string filePath);
    }
}
