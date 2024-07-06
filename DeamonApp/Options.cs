using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeamonApp
{
    /// <summary>
    /// 命令行选项
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 查看所有进程
        /// </summary>
        [Option('p',"peek",Default =false,HelpText ="Peek Process")]
        public bool Peek {  get; set; }
        /// <summary>
        /// 是否结束监视
        /// </summary>
        [Option('e',"end", Default = false , HelpText = "End supervise.")]
        public bool End { get; set; }

    }
}
