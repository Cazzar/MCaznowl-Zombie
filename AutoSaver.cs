/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl/MCForge)
	
	Dual-licensed under the	Educational Community License, Version 2.0 and
	the GNU General Public License, Version 3 (the "Licenses"); you may
	not use this file except in compliance with the Licenses. You may
	obtain a copy of the Licenses at
	
	http://www.opensource.org/licenses/ecl2.php
	http://www.gnu.org/licenses/gpl-3.0.html
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the Licenses are distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the Licenses for the specific language governing
	permissions and limitations under the Licenses.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;


namespace MCForge
{
    class AutoSaver
    {
        static int _interval;
        string backupPath = @Server.backupLocation;

        static int count = 1;
        public AutoSaver(int interval)
        {
            _interval = interval * 1000;

            Thread runner;
            runner = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    Thread.Sleep(_interval);
                    Server.ml.Queue(delegate
                    {
                        if (!Server.ZombieModeOn || !Server.noLevelSaving)
                        {
                            //Run();
                        }
                    });

                    if (Player.players.Count > 0)
                    {

                        if (!Server.ZombieModeOn) { Command.all.Find("zombiegame").Use(null, ""); }

                        string allCount = "";
                        foreach (Player pl in Player.players) allCount += ", " + pl.name;
                        try { Server.s.Log("!PLAYERS ONLINE: " + allCount.Remove(0, 2), true); }
                        catch { }

                        allCount = "";
                        foreach (Level l in Server.levels) allCount += ", " + l.name;
                        try { Server.s.Log("!LEVELS ONLINE: " + allCount.Remove(0, 2), true); }
                        catch { }
                    }
                }
            }));
            runner.Start();
        }

        /*
        static void Exec()
        {
            Server.ml.Queue(delegate
            {
                Run();
            });
        }*/

        public static void Run()
        {
            /* try
             {
                 count--;

                 Server.levels.ForEach(delegate(Level l)
                 {
                     try
                     {
                         if (!l.changed) return;

                         l.Save();
                         if (count == 0)
                         {
                             int backupNumber = l.Backup();

                             if (backupNumber != -1)
                             {
                                 l.ChatLevel("Backup " + backupNumber + " saved.");
                                 Server.s.Log("Backup " + backupNumber + " saved for " + l.name);
                             }
                         }
                     }
                     catch
                     {
                         Server.s.Log("Backup for " + l.name + " has caused an error.");
                     }
                 });

                 if (count <= 0)
                 {
                     count = 15;
                 }
             }
             catch (Exception e) { Server.ErrorLog(e); }

             try
             {
                 if (Player.players.Count > 0)
                 {
                     List<Player> tempList = new List<Player>();
                     tempList.AddRange(Player.players);
                     foreach (Player p in tempList) { p.save(); }
                     tempList.Clear();
                 }
             }
             catch (Exception e) { Server.ErrorLog(e); }*/
         }
    }
}
