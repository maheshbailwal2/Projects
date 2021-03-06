﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using RecordSession;
using Core;
using FileSystem;

namespace Recorder
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            new JarFileAssemblyLoader().Register();
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDIParent1());

                //Application.Run(new VideoControlBox());

                mutex.ReleaseMutex();
            }
            else
            {
               
                MessageBox.Show("only one instance at a time");
                return;
            }

          
        }

      
    }
}
