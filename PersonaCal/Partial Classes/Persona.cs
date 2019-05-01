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
            if (Arcana.Id == 1002)
                return string.Format("Fusion not possible");
            else
                return string.Format("{0,-13}\t{1,-15}\t{2,-15}", Arcana, Name, Level);
        }
    }
}
