using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking
{
    public partial class UserInformation_Form : Form
    {
        User user;
        City city;
        System.Windows.Forms.Timer timer;
        int AllParkingFreeSpotPrevious;
        public UserInformation_Form(User user, City city)
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
        // Функція для повернення до головного меню
        private void button1_Click(object sender, EventArgs e)
        {
            Menu_Form menu = new Menu_Form(user, city);
            menu.Show();
            this.Hide();
        }
        // Функція яка виконується при запуску форми
        private void UserInformation_Form_Load(object sender, EventArgs e)
        {
            CorrectStartPosition();
            textBox1.Text = user.InformationWrite();
        }
        // Функція для переходу до форми редагування при натисненні на відповідну кнопку
        private void button2_Click(object sender, EventArgs e)
        {
            EditInformation_Form edit = new EditInformation_Form(user, city);
            edit.Show();
            this.Hide();
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
            textBox1.Left = (formWidth - textBox1.Width) / 2;
            textBox1.Top = (formHeight - textBox1.Height) / 2;
            label1.Left = (formWidth - label1.Width) / 2;
            label1.Top = textBox1.Top - label1.Height - 20;
            button2.Width = formWidth / 10;
            button2.Height = formHeight / 20;
            button2.Top = formHeight / 2 + formHeight / 3;
            button2.Left = (formWidth - button2.Width) / 2;
        }
        // Функція для таймеру
        private void Timer_Tick(object sender, EventArgs e)
        {
            int AllParkingFreeSpot = city.getParking1.FreeParkingSpaces + city.getParking2.FreeParkingSpaces + city.getParking3.FreeParkingSpaces;
            if (AllParkingFreeSpot != AllParkingFreeSpotPrevious)
            {
                user = new User(user.Login);
                AllParkingFreeSpotPrevious = AllParkingFreeSpot;
                textBox1.Text = user.InformationWrite();
            }
        }
    }
}
