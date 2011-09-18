/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCLawl/MCForge)
	
	Dual-licensed under the	Educational Community License, Version 2.0 and
	the GNU General Public License, Version 3 (the "Licenses"); you may
	not use this file except in compliance with the Licenses. You may
	obtain a copy of the Licenses at
	
	http://www.osedu.org/licenses/ECL-2.0
	http://www.gnu.org/licenses/gpl-3.0.html
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the Licenses are distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the Licenses for the specific language governing
	permissions and limitations under the Licenses.
*/
using System;

namespace MCForge
{
    public class CmdTime : Command
    {
        public override string name { get { return "time"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "zombie"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdTime() { }

        public override void Use(Player p, string message)
        {
            int time = DateTime.Now.Minute;
            if (Server.infection == true)
            {
                if ((CmdZombieGame.timeMinute - time) == 0)
                {
                    p.SendMessage("Time remaining in minutes: Less than a minute!");
                }
                else if ((CmdZombieGame.timeMinute - time) >= 61)
                {
                    p.SendMessage("Time remaining in minutes: " + Convert.ToString(CmdZombieGame.timeMinute - time - 60));
                }
                else
                {
                    p.SendMessage("Time remaining in minutes: " + Convert.ToString(CmdZombieGame.timeMinute - time));
                }
            }
            else
            {
                p.SendMessage("The round hasn't started yet!");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/time - Shows the server time.");
        }
    }
}