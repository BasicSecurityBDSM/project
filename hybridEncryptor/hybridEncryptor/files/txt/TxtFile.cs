using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    class TxtFile : File
    {
        private string text;

        public override void Generate(byte[] dataToInsert)
        {
            text = Convert.ToBase64String(dataToInsert);
        }

        public override byte[] GetData()
        {
            return Convert.FromBase64String(text);
        }

        public override void Load(string filePath)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                text = file.ReadLine();
                file.Close();
            }
        }

        public override void Save(string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                file.WriteLine(text);
                file.Close();
            }
        }
    }
}
