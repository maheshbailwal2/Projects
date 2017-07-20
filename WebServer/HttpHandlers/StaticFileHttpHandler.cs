﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Interface;

namespace HttpHandlers
{
    public class StaticFileHttpHandler : IHttpHandler
    {
        string _webServerRootDir;
        public StaticFileHttpHandler(string webServerRootDir)
        {
            _webServerRootDir = webServerRootDir;
        }

        public byte[] ProcessRequest(System.Net.HttpListenerRequest request)
        {
            var filePathOnServer = _webServerRootDir + request.RawUrl.Replace("/", "\\");

            if (filePathOnServer.Contains("?"))
            {
                filePathOnServer = filePathOnServer.Substring(0, filePathOnServer.IndexOf('?'));
            }

            var contentString = File.ReadAllBytes(filePathOnServer);
            Console.WriteLine(request.Url);
            return contentString;

        }
    }
}
