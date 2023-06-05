using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class MotorCycle : Vehicle
    {
        string name_moto;
        
        public MotorCycle(string name)
        {
            name_moto = name;
        }
        public override string Name
        {
            get { return name_moto; }
            set { name_moto = value; }
        }
    }
}
