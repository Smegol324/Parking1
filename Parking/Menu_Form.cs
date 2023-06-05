using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking
{
    public partial class Menu_Form : Form
    {
        User user;
        City city;
        System.Windows.Forms.Timer timer;
        int AllParkingFreeSpotPrevious;
        public Menu_Form(User user, City city)
        {
            InitializeComponent();
            this.user = user;
            this.city = city;
            AllParkingFreeSpotPrevious = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка повернення до входу 
        {
            Login_Form login = new Login_Form(city);
            login.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) // кнопка зміна особистих даних
        {
            UserInformation_Form info = new UserInformation_Form(user, city);
            info.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // кнопка паркування
        {
            Parkovka_Form parkovka = new Parkovka_Form(user, city);
            parkovka.Show();
            this.Hide();
        }
        // Функція яка виконується при запуску форми та вибирає який інтерфейс надати користувачу
        private void Menu_Form_Load(object sender, EventArgs e)
        {
            if (user.Login == "guest")
            {
                InterfaceGuestTrue();
                InterfaceAdminFalse();
                InterfaceUserFalse();
            }
            else if (user.Login == "admin")
            {
                InterfaceGuestFalse();
                InterfaceAdminTrue();
                InterfaceUserFalse();
            }
            else
            {
                InterfaceGuestFalse();
                InterfaceAdminFalse();
                InterfaceUserTrue();
            }
            CorrectStartPosition();   
        }
        public void InterfaceUserTrue()
        {
            button2.Visible = true;
            button3.Visible = true;
        }
        public void InterfaceAdminTrue()
        {
            button4.Visible = true;
            button6.Visible = true;
        }
        public void InterfaceGuestTrue()
        {
            button5.Visible = true;
        }
        public void InterfaceUserFalse()
        {
            button2.Visible = false;
            button3.Visible = false;
        }
        public void InterfaceAdminFalse()
        {
            button4.Visible = false;
            button6.Visible = false;
        }
        public void InterfaceGuestFalse()
        {
            button5.Visible = false;
        }


        // Функція для правильного динамічного розміщення об'єктів на формі
        public void CorrectStartPosition()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            button1.Width = formWidth / 10;
            button1.Height = formHeight / 20;
            button1.Left = formWidth - formWidth / 70 - button1.Width;
            button1.Top = formHeight / 40;
            button2.Width = button3.Width = button4.Width = button5.Width = button6.Width =  formWidth / 5;
            button2.Height = button3.Height = button4.Height = button5.Height = button6.Height = formHeight / 10;
            button2.Left = (formWidth - button2.Width) / 2 - formWidth / 5;
            button3.Left = (formWidth - button2.Width) / 2 + formWidth / 5;
            button2.Top = button3.Top = (formHeight - button2.Height) / 2;
            button5.Left = (formWidth - button5.Width) / 2;
            button5.Top = (formHeight - button5.Height) / 2;
            button4.Left = (formWidth - button2.Width) / 2 - formWidth / 5;
            button6.Left = (formWidth - button2.Width) / 2 + formWidth / 5;
            button4.Top = button6.Top = (formHeight - button2.Height) / 2;
        }
        // Функція для таймеру
        private void Timer_Tick(object sender, EventArgs e)
        {
            int AllParkingFreeSpot = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            if (AllParkingFreeSpot != AllParkingFreeSpotPrevious)
            {
                user = new User(user.Login);
                AllParkingFreeSpotPrevious = AllParkingFreeSpot;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Parkovka_Form parkovka = new Parkovka_Form(user, city);
            parkovka.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Parkovka_Form parkovka = new Parkovka_Form(user, city);
            parkovka.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditSizeParking_Form edit_size = new EditSizeParking_Form(user, city);
            edit_size.Show();
            this.Hide();
        }
    }
}
