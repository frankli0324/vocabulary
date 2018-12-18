using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
namespace vocabulary
{
    class mp3player
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string command,      //MCI命令字符串
            string returnString, //存放反馈信息的缓冲区
            int returnSize,      //缓冲区的长度
            IntPtr hwndCallback  //回调窗口的句柄，一般为NULL
            );                   //若成功则返回0，否则返回错误码。

        private void PlayWait(string file)
        {
            mciSendString(string.Format("open \"{0}\" type mpegvideo alias media", file), null, 0, IntPtr.Zero);
            mciSendString("play media wait", null, 0, IntPtr.Zero);
            mciSendString("close media", null, 0, IntPtr.Zero);
        }

        private void PlayRepeat(string file)
        {
            mciSendString(string.Format("open \"{0}\" type mpegvideo alias media", file), null, 0, IntPtr.Zero);
            mciSendString("play media repeat", null, 0, IntPtr.Zero);
        }

        private Thread thread;
        public void Play(string file, int times)
        {
            thread = new Thread(() =>
            {
                if (times == 0)
                {
                    PlayRepeat(file);
                }
                else if (times > 0)
                {
                    for (int i = 0; i < times; i++)
                    {
                        PlayWait(file);
                    }
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
        public void Exit()
        {
            if (thread != null)
            {
                try
                {
                    thread.Abort();
                }
                catch { }
                thread = null;
            }
        }
    }
}
