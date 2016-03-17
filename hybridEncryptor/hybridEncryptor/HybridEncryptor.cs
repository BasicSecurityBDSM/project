using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hybridEncryptor
{
    public class HybridEncryptor
    {
        private DesEncryptor des;
        private RsaEncryptor rsa;
        public HybridEncryptor()
        {
            des = new DesEncryptor();
            rsa = new RsaEncryptor();
        }
    }
}
