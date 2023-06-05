using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class City
    {
        Parking_ parking1;
        Parking_ parking2;
        Parking_ parking3;
        public City(int id1, int i1, int j1, int id2, int i2, int j2, int id3, int i3, int j3) {
            parking1 = new Parking_(id1, i1, j1);
            parking2 = new Parking_(id2, i2, j2);
            parking3 = new Parking_(id3, i3, j3);
        }
        // гетери для паркувань
        public Parking_ getParking1
        {
            get { return parking1; }
        }
        public Parking_ getParking2
        {
            get { return parking2; }
        }
        public Parking_ getParking3
        {
            get { return parking3; }
        }
        public string ParkingSizeWrite()
        {
            string result_text = "";
            result_text += "Parking 1, Size: " + getParking1.NumberOfRows + "x" + getParking1.NumberOfColumns + "\r\n";
            result_text += "Parking 2, Size: " + getParking2.NumberOfRows + "x" + getParking2.NumberOfColumns + "\r\n";
            result_text += "Parking 3, Size: " + getParking3.NumberOfRows + "x" + getParking3.NumberOfColumns + "\r\n";
            return result_text;
        }
    }
}
