using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Parking
{
    public interface InterfaceChangeUser
    {
        bool ChangeLogin(string s);
        bool ChangePassword(string s3, string s4);
        void AddCar(string s);
        void DeleteCar(string s);
        void AddMoto(string s);
        void DeleteMoto(string s);
        bool ChangeMoney(string s);
        bool ChangeName(string s);
    }
}
