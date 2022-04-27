using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1._1.Tools.MyExceptions
{

    [Serializable]
    class FileNotFoundException : Exception
    {
        public FileNotFoundException(string message) : base(message) { }
    }
}
