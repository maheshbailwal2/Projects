﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Net;

namespace UploadFolder
{
   public  class HTTPRequestHelper
    {

       public  static string PostUrl(string url)
        {
            string str = "";
        
            try
            {

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                // Send the data.
                WebResponse res = myRequest.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                str = sr.ReadToEnd();
                sr.Close();


            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.InternalServerError)
                {
                    using (StreamReader sr = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        str = sr.ReadToEnd();
                    }
                }

               
            }
            return str;
        }
    }
}
