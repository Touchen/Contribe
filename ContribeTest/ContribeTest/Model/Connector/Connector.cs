using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContribeTest.Model
{
    class Connector
    {
        private static Connector _Connector;
        private WebClient _Client;


        /// <summary>
        /// Constructor for the COnnector
        /// </summary>
        private Connector()
        {
            _Connector = this;
            _Client = new WebClient();
        }

        /// <summary>
        /// Using Singleton pattern since there only have to exist one Connector.
        /// </summary>
        /// <returns></returns>
        public static Connector GetInstance()
        {
            if(_Connector == null)
            {
                _Connector = new Connector();
            }
            return _Connector;
        }

        /// <summary>
        /// Gets the string of from the Web with the specific URl.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetStringFromServer(string url)
        {
            return _Client.DownloadString(url);
        }

        /// <summary>
        /// returns a Json readable line. Uses a thrid party library to deserialize the object into a dynamic variable. the third party is called Newtonsoft
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public dynamic JsonConvertFromURL(string url)
        {
            dynamic stuff = JsonConvert.DeserializeObject(GetStringFromServer(url));
            return stuff;
            
        }

        

    }
}
