using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeamonApp
{
    /// <summary>
    /// 处理选项
    /// </summary>
    internal class HandleOptions
    {
        private Action Begin;
        private Action Peek;
        private Action End;
        public HandleOptions()
        {
            Begin += StartDeamon1;
            Begin += StartDeamon2;
            Begin += StartTarget;
            End += EndDeamon1;
            End += EndDeamon2;
            End += EndTarget;
            Peek += PeekDeamon1;
            Peek += PeekDeamon2;
            Peek += PeekTarget;
        }

        /// <summary>
        /// 处理输入的参数
        /// </summary>
        /// <param name="args">命令行参数</param>
        public void Handle(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                ExcuteOptions(options);
            })
            .WithNotParsed(errors =>
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error}");
                }
            });
        }

        /// <summary>
        /// 执行参数
        /// </summary>
        /// <param name="options"></param>
        private void ExcuteOptions(Options options)
        {
            if (options.End)
            {
                End.Invoke();
            }else
            {
                Begin.Invoke();
            }

            if (options.Peek)
            {
                Peek.Invoke();
            }
        }

        private void StartDeamon1()
        {
            Console.WriteLine("守护进程1已启动");
        } 
        private void StartDeamon2()
        {
            Console.WriteLine("守护进程2已启动");
        }
        private void StartTarget()
        {   
            Console.WriteLine("目标进程已启动");
        }
        private void EndDeamon1()
        {
            Console.WriteLine("守护进程1已结束");
        }
        private void EndDeamon2()
        {
            Console.WriteLine("守护进程2已结束");
        }
        private void EndTarget()
        {
            Console.WriteLine("目标进程已结束");
        }
        private void PeekDeamon1()
        {
            Console.WriteLine("守护进程1状态");
        }
        private void PeekDeamon2()
        {
            Console.WriteLine("守护进程2状态");
        }
        private void PeekTarget()
        {
            Console.WriteLine("目标进程状态");
        }
    }
}
