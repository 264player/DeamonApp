using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon1
{
    /// <summary>
    /// 守护进程守护其他进程的类
    /// </summary>
    internal class DeamonProcess
    {
        /// <summary>
        /// 第二个守护程序的路径
        /// </summary>
        private string Deamon2Path;

        /// <summary>
        /// 目标程序的路径
        /// </summary>
        private string TargetPath;

        /// <summary>
        /// 是否是主守护程序
        /// </summary>
        private bool IsMaster {  get; set; }

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DeamonProcess()
        {
            bool result = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsMaster"], out result);
            IsMaster = result;
            string? tarGetPath = ConfigurationManager.AppSettings["Target"],
                deamon2Path = ConfigurationManager.AppSettings["Deamon2"];
            if (!string.IsNullOrEmpty(tarGetPath))
                TargetPath = tarGetPath;
            else
                TargetPath = string.Empty;
            if (!string.IsNullOrEmpty(deamon2Path))
                Deamon2Path = deamon2Path;
            else
                Deamon2Path = string.Empty;
        }

        /// <summary>
        /// 开始守护工作
        /// </summary>
        public void Working()
        {
            ProcessStartInfo? pinfo1 = CreateProcessInfo(TargetPath),
                pinfo2 = CreateProcessInfo(Deamon2Path);
            if (pinfo1 != null)
            {
                Thread target = new Thread(() => MonitorProcess(pinfo1, 10));
                target.Start();
            }
            if (pinfo2 != null)
            {
                Thread deamon = new Thread(() => MonitorProcess(pinfo2, 100));
                deamon.Start();
            }
        }

        /// <summary>
        /// 根据程序名，创建进程启动信息
        /// </summary>
        /// <param name="appName">程序名</param>
        /// <returns>如果程序名不为空或空白，就返回processStartInfo对象,否则返回空</returns>
        private ProcessStartInfo? CreateProcessInfo(string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                return null;
            }
            return new ProcessStartInfo(appName);
        }

        /// <summary>
        /// 监视进程，使用单独的线程进行监视，是长任务
        /// </summary>
        /// <param name="processInfo">进程信息</param>
        /// <param name="timeGap">监视进程的时间间隔</param>
        private  void MonitorProcess(ProcessStartInfo processInfo,int timeGap)
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
                    Thread.Sleep(timeGap);
                }
                Console.WriteLine("进程已重新启动");
            }
        }
    }
}
