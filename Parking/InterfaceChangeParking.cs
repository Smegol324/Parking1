using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking
{
    public interface InterfaceChangeParking
    {
        void ChangeState(string new_state, int row, int column);
        void ChangeTime(int new_time, int row, int column);
        void ChangeIsCar(bool isCar, int row, int column);
        void AddCarInSpot(string new_car_name, int row, int column);
        string DeleteCarInSpot(int row, int column);
        void AddMotoInSpot(string new_car_name, int row, int column);
        string DeleteMotoInSpot(int row, int column);
        void AddUserInSpot(string new_user_login, int row, int column);
        string DeleteUserInSpot(int row, int column);
    }
}
