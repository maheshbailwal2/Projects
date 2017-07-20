﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class Logger
    {
        private static object objLock = new object();
        private static string _logFilePath;
        static  Logger()
        {
            var userFullName = RuntimeHelper.GetCurrentUserName().ReverseMe();

            _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SysHealth", userFullName + "_error.log");
          
            if(!Directory.Exists(Path.GetDirectoryName(_logFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
            }
        }

        public static void LogError(Exception ex)
        {
            Write(ex.ToString());
        }

        public static void LogInforamtion(string information)
        {
            Write(information);
        }

        private static void Write(string text)
        {
            try
            {
                lock (objLock)
                {
                    File.AppendAllText(_logFilePath, DateTime.Now.ToString() + Environment.NewLine + text + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
