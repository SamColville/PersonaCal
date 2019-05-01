using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaCal
{
    public partial class Persona
    {
        public override string ToString()
        {
            return string.Format("{0,-15}\t{1,-15}\t{2,-15}", Arcana, Name, Level);
        }
    }
}
