using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Car : Vehicle
    {
        string name_car;
        public Car(string name) 
        { 
            name_car = name;
        }
        public override string Name
        {
            get { return name_car; }
            set { name_car = value; }
        }
    }
}
