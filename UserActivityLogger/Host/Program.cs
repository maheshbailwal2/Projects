﻿using Core;
using System;
using System.Configuration;
using System.IO;
using FileSystem;
using UserActivityLogger;
using Castle.Windsor;

using System.Collections.Generic;
using System.Threading;

namespace Host
{
    class Program
    {
        
       static Dictionary<string, string> defaultSettings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
         { "DataFolder", @"C:\WINDOWS\SysWin" },
         { "StorageConnectionString", "DefaultEndpointsProtocol=https;AccountName=mediavaletdevasia;AccountKey=wtjIEIXRmtA6tHUW5zkhYwc1cCYhlFhsW8z2Cf3TUpKacrrnYWBaLUUrQDacfH3kQ3XhftEhVt3f2ONZQhCMog==" },
         { "FileSystemType", "AZUREBLOB" },
         { "ArchiveLocation", "activityarchive" }};

        static void Main(string[] args)
        {
            try
            {
                new JarFileAssemblyLoader().Register();
                Start(args);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                System.Environment.Exit(0);
            }
        }
        private static void Start(string[] args)
        {

            if (SingleInstance.IsApplicationAlreadyRunning("UserActivityLoggerHost"))
            {
                Logger.LogInforamtion("Already Running");
                return;
            }

            if (true || args.Length > 0 && args[0] == "hidden")
            {
                Logger.LogInforamtion("Running with hidden");
                new UnhandledExceptionHandlercs().Register(Logger.LogError);
                ProcessHelper.RecreateProcessOnExit();

                IWindsorContainer windsorContainer = new WindsorContainer();
                CastleWireUp.WireUp(windsorContainer, defaultSettings);

                var startUp = windsorContainer.Resolve<IStartUp>();
                startUp.Start(TimeSpan.FromSeconds(2));
            }
            else
            {
                ProcessHelper.RunHidden(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
        }
    }
}
