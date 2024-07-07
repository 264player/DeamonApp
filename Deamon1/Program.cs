using Deamon1;
using System;
using System.Configuration;
using System.Diagnostics;

class Program
{
    static Mutex? mutex = null;
    static void Main(string[] args)
    {
        if (!CheckSelfSingleton())
        {
            return;
        }
        // 程序的主逻辑
        Console.WriteLine("程序启动中...");
        string? targetAppName = ConfigurationManager.AppSettings["Target"];
        if (string.IsNullOrEmpty(targetAppName) )
        {
            Console.WriteLine("没有需要守护的进程，请重新配置 ");
            return;
        }
        if (IsProcessRunning(targetAppName))
        {
            Console.WriteLine($"{targetAppName}正在运行");
        }else
        {
            Console.WriteLine($"{targetAppName}没有运行");
        }
        
        DeamonProcess deamonProcess = new DeamonProcess();
        deamonProcess.Working();
    }

    /// <summary>
    /// 检查是否只存在一个进程
    /// </summary>
    /// <returns>本守护程序是否只存在一个进程，是返回true，不是返回false</returns>
    static bool CheckSelfSingleton()
    {
        string? LockName = ConfigurationManager.AppSettings["LockName"];
        if (string.IsNullOrEmpty(LockName))
        {
            LockName = "Deamon1";
        }
        string mutexName = $"Global\\{LockName}"; // 确保名称唯一
        // 尝试创建一个互斥锁
        bool createdNew;
        mutex = new Mutex(true, mutexName, out createdNew);
        if (!createdNew)
        {
            Console.WriteLine("程序已经在运行中，不能启动多个实例。");
            return false;
        }
        // 确保在程序退出时释放互斥锁
        AppDomain.CurrentDomain.ProcessExit += (s, e) => mutex.ReleaseMutex();
        return true;
    }

    /// <summary>
    /// 指定名称的进程是否在运行
    /// </summary>
    /// <param name="processName">需要检查是否在运行的检查名称</param>
    /// <returns>有正在运行的指定进程就返回true，没有正在运行的指定进程就返回false</returns>
    static bool IsProcessRunning(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        return processes.Length > 0;
    }
}
