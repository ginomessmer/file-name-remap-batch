using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileNameRemapBatch.Data
{
    public class Remap
    {
        public string FromChar { get; set; }
        public string ToChar { get; set; }

        public Remap(string from, string to)
        {
            this.FromChar = from;
            this.ToChar = to;
        }
    }
}
