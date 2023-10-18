using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose
{
    public class OverQualityException: Exception
    {
        public OverQualityException()
        {

        }

        public OverQualityException(string message) 
            : base(message)
        {

        }
    }
}
