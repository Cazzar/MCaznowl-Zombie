﻿using System;
using System.IO;

namespace MCForge
{
    class WOMBeat : Beat
    {
        public string URL { get { return "https://direct.worldofminecraft.com/hb.php"; } }
        public string Parameters { get; set; }
        public bool Log { get { return false; } }

        public void Prepare()
        {
            if (Player.number * 2 >= Player.number - 1)
            {
                Parameters += "&salt=" + Server.salt +
                    "&users=" + Player.number;
            }
            else
            {
                Parameters += "&salt=" + Server.salt +
                    "&users=" + Player.number * 2;
            }
        }

        public void OnPump(string line)
        {
            // Only run the code below if we receive a response
            /*if (!String.IsNullOrEmpty(line.Trim()))
            {
                string newHash = line.Substring(line.LastIndexOf('=') + 1);

                // Run this code if we don't already have a hash or if the hash has changed
                if (String.IsNullOrEmpty(Server.Hash) || !newHash.Equals(Server.Hash))
                {
                    Server.Hash = newHash;
                    string serverURL = line;

                    //serverURL = "http://" + serverURL.Substring(serverURL.IndexOf('.') + 1);
                    //Server.s.UpdateUrl(serverURL);
                    //File.WriteAllText("text/externalurl.txt", serverURL);
                    //Server.s.Log("URL found: " + serverURL);
                }
            }*/
        }
    }
}