using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 音乐播放器
{
    static class Program

    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            login frmLogin = new login();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new main());
            }
        }
    }
}
