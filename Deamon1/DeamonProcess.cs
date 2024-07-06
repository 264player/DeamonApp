using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon1
{
    internal class DeamonProcess
    {
        private string? Deamon2Path;
        private string? TargetPath;
        private bool IsMaster {  get; set; }
        public DeamonProcess()
        {
            bool result = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsMaster"], out result);
            IsMaster = result;
            Deamon2Path = ConfigurationManager.AppSettings["Deamon2"];
            TargetPath = ConfigurationManager.AppSettings["Target"];
        }

        public void Working()
        {
            Thread masterDeamon = new Thread(() => MonitorProcess(new ProcessStartInfo(Deamon2Path))),
                target = new Thread(() => MonitorProcess(new ProcessStartInfo(TargetPath)));
            masterDeamon.Start();
            target.Start();

        }

        /// <summary>
        /// 监视进程，使用单独的线程进行监视，是长任务
        /// </summary>
        /// <param name="process">需要被监视的进程</param>
        public void MonitorProcess(ProcessStartInfo processInfo)
        {
            while(true)
            {
                Process? process = Process.Start(processInfo);
                if (process == null)
                {
                    Console.WriteLine("进程启动失败");
                    return;
                }
                // 输出进程ID
                Console.WriteLine($"启动进程 ID: {process.Id}");
                // 监控进程
                while (!process.HasExited)
                {
                    Console.WriteLine("进程正在运行...");
                }
                Console.WriteLine("进程已重新启动");
            }
        }
    }
}
