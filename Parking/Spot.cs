using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking
{
    public class Spot
    {
        static string parkingFilePath = "parking.txt";
        int id;
        int cost;
        string state;
        int time;
        Vehicle vehicleInSpot;
        string userInSpot;
        bool isCar;

        public Spot(int i, int cos, string state, int time, bool isCar)
        {
            id = i;
            cost = cos;
            this.state = state;
            this.time = time;
            vehicleInSpot = null;
            userInSpot = null;
            this.isCar = isCar;
        }
        // гетери та сетери
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public int Time
        {
            get { return time; }
            set { time = value; }
        }
        public Vehicle VehicleInSpot
        {
            get { return vehicleInSpot; }
            set { vehicleInSpot = value; }
        }
        public string UserInSpot
        {
            get { return userInSpot; }
            set { userInSpot = value; }
        }
        public bool IsCar
        {
            get { return isCar; }
            set { isCar = value; }
        }
    }
}
