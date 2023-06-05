using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using System.Diagnostics.SymbolStore;

namespace Parking
{
    public partial class Login_Form : Form
    {
        City city;
        public Login_Form(City city)
        {
            InitializeComponent();
            this.city = city;
        }
        
        private void button1_Click(object sender, EventArgs e) // кнопка виходу
        {
            Close();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e) // кнопка входу
        {
            if (User.LoginUser(textBox1.Text, textBox2.Text))
            {
                MessageBox.Show("Вхід виконано");
                User user = new User(textBox1.Text);
                Menu_Form menu = new Menu_Form(user, city);
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Не вірно введені дані");
            }
        }

        private void button3_Click(object sender, EventArgs e) // кнопка регістрація
        {
            if (User.RegisterUser(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text))
            {
                MessageBox.Show("Регістрація завершена");
                label4.Visible = false;
                textBox3.Visible = false;
                button3.Visible = false;
                label6.Visible = false;
                label5.Visible = true;
                button2.Visible = true;
                label7.Visible = false;
                textBox4.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Не вірно введені дані");
            }
        }

        private void label5_Click(object sender, EventArgs e) // кнопка інтерфейсу регістрації
        {
            label4.Visible = true;
            textBox3.Visible = true;
            button3.Visible = true;
            label6.Visible = true;
            label5.Visible = false;
            button2.Visible = false;
            label7.Visible = true;
            textBox4.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void label6_Click(object sender, EventArgs e) // кнопка інтерфейсу входу
        {
            label4.Visible = false;
            textBox3.Visible = false;
            button3.Visible = false;
            label6.Visible = false;
            label5.Visible = true;
            button2.Visible = true;
            label7.Visible = false;
            textBox4.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        // Функція яка викликається при запуску форми
        private void Login_Form_Load(object sender, EventArgs e)
        {
            CorrectStartPosition();
        }
        // Функція для правильного динамічного розміщення об'єктів на формі
        public void CorrectStartPosition()
        {
            int formWidth = this.Width;
            int formHeight = this.Height;
            label1.Left = (formWidth - label1.Width) / 2;
            label1.Top = (formHeight - label1.Height) / 10;
            button1.Width = formWidth / 10;
            button1.Height = formHeight / 20;
            button1.Left = formWidth - formWidth / 70 - button1.Width;
            button1.Top = formHeight / 40;
            label2.Left = (formWidth - label2.Width - textBox1.Width - 50) / 2;
            label2.Top = (formHeight - label2.Height) / 2;
            textBox1.Left = label2.Left + formWidth / 20;
            textBox1.Top = label2.Top + label2.Height - textBox1.Height + 3;
            label3.Left = label2.Left;
            textBox2.Left = textBox1.Left;
            label3.Top = label2.Top + label2.Height + formHeight / 40;
            textBox2.Top = label3.Top + label3.Height - textBox2.Height + 3;
            label4.Left = label3.Left;
            textBox3.Left = textBox2.Left;
            label4.Top = label3.Top + label3.Height + formHeight / 40;
            textBox3.Top = label4.Top + label4.Height - textBox3.Height + 3;
            label7.Left = label4.Left;
            textBox4.Left = textBox3.Left;
            label7.Top = label4.Top + label4.Height + formHeight / 40;
            textBox4.Top = label7.Top + label7.Height - textBox4.Height + 3;
            button2.Width = button3.Width = (formWidth / 15);
            button2.Height = button3.Height = (formHeight / 30);
            button2.Left = (formWidth - button2.Width) / 2;
            button2.Top = (formHeight - button2.Height) / 2 + (formHeight / 3);
            button3.Left = button2.Left;
            button3.Top = button2.Top;
            label5.Left = (formWidth - label5.Width) / 2;
            label6.Left = (formWidth - label6.Width) / 2;
            label5.Top = button2.Top + button2.Height + 20;
            label6.Top = button3.Top + button3.Height + 20;
            label8.Left = (formWidth - label8.Width) / 2;
            label8.Top = label6.Top + label6.Height + 20;
        }
        // Функція для використання програми як гість
        private void label8_Click(object sender, EventArgs e)
        {
            User user = new User("guest");
            Menu_Form menu = new Menu_Form(user, city);
            menu.Show();
            this.Hide();
        }
    }
}
