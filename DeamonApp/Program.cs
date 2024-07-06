using System;
using System.Configuration;
using CommandLine;
using DeamonApp;

public class Program
{
    public static void Main(string[] args)
    {
        HandleOptions handleOptions = new HandleOptions();
        handleOptions.Handle(args);
        foreach (string arg in ConfigurationManager.AppSettings)
        {
            Console.WriteLine(ConfigurationManager.AppSettings[arg]);
        }
    }
}
