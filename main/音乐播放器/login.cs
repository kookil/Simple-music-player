using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    public partial class login : Form
    {
        public login()
        {
            welcome fw = new welcome();
            fw.Show();//show出欢迎窗口
            System.Threading.Thread.Sleep(2000);//欢迎窗口停留时间2s
            fw.Close();
            InitializeComponent();

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入账号", "提示");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入密码", "提示");

            }
            else
            {

                string[] str = File.ReadAllLines(@"C:\Users\kookil\Desktop\message.txt");
                string account = textBox1.Text;
                string password = textBox2.Text;
                for (int i = 1; i < str.Length; i += 2)
                {
                    if (account == str[i - 1])
                    {
                        if (password == str[i])
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("登陆失败，请确保已注册且密码正确", "提示");
                        break;
                    }
                }
                //this.DialogResult = DialogResult.OK;
                //this.Close();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] str = File.ReadAllLines(@"C:\Users\kookil\Desktop\message.txt");

            string account = textBox1.Text;
            string password = textBox2.Text;
            if (str.Length == 0)
            {
                File.AppendAllText(@"C:\Users\kookil\Desktop\message.txt", account+"\t"+password);
                MessageBox.Show("注册成功");
            }
            else
            {
                for (int i = 0; i < str.Length; i += 1)
                {
                    if (account == str[i])
                    {
                        MessageBox.Show("该账号已经被注册，请重新输入");
                    }
                    else
                    {
                        File.AppendAllText(@"C:\Users\kookil\Desktop\message.txt", "\n" + account+"\t" + password);
                    }
                }

            }
        }
    }
}
