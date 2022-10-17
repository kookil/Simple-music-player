using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using WMPLib;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace 音乐播放器
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            this.SizeChanged += new Resize(this).Form1_Resize;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void 注销账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("要注销吗？", "提示", MessageBoxButtons.YesNoCancel,
               MessageBoxIcon.Question) == DialogResult.Yes)
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        List<string> Music = new List<string>();
        private void 导入歌曲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = ("请选择音乐文件");
            ofd.InitialDirectory = @"C:\Users\kookil\Music\";
            ofd.Multiselect = true;
            ofd.Filter = "(音乐文件)|*.wav;*.flac;*.mp3;*.ape|所有文件|*.*";
            ofd.ShowDialog();

            string[] path = ofd.FileNames;
            for (int i = 0; i < path.Length; i++)
            {
                listBox1.Items.Add(Path.GetFileName(path[i]));
                Music.Add(path[i]);
            }
            axWindowsMediaPlayer1.currentPlaylist = axWindowsMediaPlayer1.newPlaylist("aa", "");

            foreach (string fn in ofd.FileNames)
            {
                axWindowsMediaPlayer1.currentPlaylist.appendItem(axWindowsMediaPlayer1.newMedia(fn));
            }

            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            index--;
            if (index < 0)
            {
                index = listBox1.Items.Count - 1;
            }

            listBox1.SelectedIndex = index;
            axWindowsMediaPlayer1.URL = Music[index];
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "播放")
            {
                axWindowsMediaPlayer1.URL = Music[listBox1.SelectedIndex];
                axWindowsMediaPlayer1.Ctlcontrols.play();
                button2.Text = "暂停";
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                button2.Text = "播放";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            index++;
            if (index == listBox1.Items.Count)
            {
                index = 0;
            }
            listBox1.SelectedIndex = index;
            axWindowsMediaPlayer1.URL = Music[index];
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void 清空歌曲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.Items.IndexOf(textBox1.Text);
            listBox1.TopIndex = index;
            listBox1.SelectedIndex = index;
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 循环播放_Click(object sender, EventArgs e)
        {
            if (循环播放.Text == "循环播放")
            {
                axWindowsMediaPlayer1.settings.setMode("shuffle", false);
                循环播放.Text = "随机播放";
            }
            else if (循环播放.Text == "随机播放")
            {
                axWindowsMediaPlayer1.settings.setMode("shuffle", true);
                循环播放.Text = "单曲循环";
            }
            else
            {
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                循环播放.Text = "循环播放";
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
        class Resize
        {
            private Form _form;

            public Resize(Form form)
            {
                int count = form.Controls.Count * 2 + 2;
                float[] factor = new float[count];
                int i = 0;
                factor[i++] = form.Size.Width;
                factor[i++] = form.Size.Height;
                foreach (Control ctrl in form.Controls)
                {
                    factor[i++] = ctrl.Location.X / (float)form.Size.Width;
                    factor[i++] = ctrl.Location.Y / (float)form.Size.Height;
                    ctrl.Tag = ctrl.Size;
                }
                form.Tag = factor;
                this._form = form;
            }

            public void Form1_Resize(object sender, EventArgs e)
            {
                float[] scale = (float[])this._form.Tag;
                int i = 2;
                foreach (Control ctrl in this._form.Controls)
                {
                    ctrl.Left = (int)(this._form.Size.Width * scale[i++]);
                    ctrl.Top = (int)(this._form.Size.Height * scale[i++]);
                    ctrl.Width = (int)(this._form.Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);
                    ctrl.Height = (int)(this._form.Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);
                }
            }
        }

        private void 更换皮肤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件（所有类型）|*.png;*.gif;*.gpeg;*.bmp;*.ico;*.jpg;";

            string filePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                System.Drawing.Image bg = System.Drawing.Image.FromFile(filePath);
                this.BackgroundImage = bg;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string s = listBox1.SelectedItem.ToString();
            if (radioButton1.Checked)
            {
                s += ",";
                s += radioButton1.Text;
            }
            if (radioButton2.Checked)
            {
                s += ",";
                s += radioButton2.Text;
            }
            if (radioButton3.Checked)
            {
                s += ",";
                s += radioButton3.Text;
            }
            if (radioButton4.Checked)
            {
                s += ",";
                s += radioButton4.Text;
            }
            if (radioButton5.Checked)
            {
                s += ",";
                s += radioButton5.Text;
            }
            if (radioButton6.Checked)
            {
                s += ",";
                s += radioButton6.Text;
            }
            if (radioButton7.Checked)
            {
                s += ",";
                s += radioButton7.Text;
            }
            if (radioButton8.Checked)
            {
                s += ",";
                s += radioButton8.Text;
            }
            if (radioButton9.Checked)
            {
                s += ",";
                s += radioButton9.Text;
            }
            if (radioButton10.Checked)
            {
                s += ",";
                s += radioButton10.Text;
            }
            if (radioButton11.Checked)
            {
                s += ",";
                s += radioButton11.Text;
            }
            if (radioButton12.Checked)
            {
                s += ",";
                s += radioButton12.Text;
            }
            if (radioButton13.Checked)
            {
                s += ",";
                s += radioButton13.Text;
            }
            if (radioButton13.Checked)
            {
                s += ",";
                s += radioButton13.Text;
            }
            if (radioButton14.Checked)
            {
                s += ",";
                s += radioButton14.Text;
            }
            if (radioButton15.Checked)
            {
                s += ",";
                s += radioButton15.Text;
            }
            if (radioButton16.Checked)
            {
                s += ",";
                s += radioButton16.Text;
            }
            if (radioButton17.Checked)
            {
                s += ",";
                s += radioButton17.Text;
            }
            //StreamWriter sw = new StreamWriter(@"C:\Users\kookil\Desktop\Music.txt");
            using (StreamWriter sw = File.AppendText(@"C:\Users\kookil\Desktop\Music.txt"))
            {
                sw.WriteLine(s);
                sw.Close();
                MessageBox.Show("写入完成");
            }
            
        }


        private void button6_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\kookil\Desktop\Music.txt");
            List<Dictionary<string, Dictionary<string, string>>> MuList = new List<Dictionary<string, Dictionary<string, string>>>();
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string name = parts[0];
                string yz = parts[1];
                string fg = parts[2];
                string ch = parts[3];
                Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
                dic[name] = new Dictionary<string, string>();
                Dictionary<string, string> dicc = new Dictionary<string, string>();
                dicc[yz]= parts[1];
                dicc[fg] = parts[2];
                dicc[ch] = parts[3];
                dic[name] = dicc;
                MuList.Add(dic);
            }
            string gqlx = textBox2.Text;
            for (int i = 0; i < MuList.Count; i++)
            {
                foreach (var item in MuList[i].Values)
                {
                    foreach (var q in item)
                    {
                        if (gqlx==q.Value)
                        {
                            this.listBox1.Items.Add((MuList[i].Keys).First());
                        }
                    }
                }
            }
        }
    }
}
