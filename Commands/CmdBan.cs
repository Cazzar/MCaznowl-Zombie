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

namespace MCForge
{
    public class CmdBan : Command
    {
        public override string name { get { return "ban"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdBan() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "") { Help(p); return; }

                bool stealth = false; bool totalBan = false;
                if (message[0] == '#')
                {
                    message = message.Remove(0, 1).Trim();
                    stealth = true;
                    Server.s.Log("Stealth Ban Attempted");
                }
                else if (message[0] == '@')
                {
                    totalBan = true;
                    message = message.Remove(0, 1).Trim();
                }

                Player who = Player.Find(message);

                if (who == null)
                {
                    if (!Player.ValidName(message))
                    {
                        Player.SendMessage(p, "Invalid name \"" + message + "\".");
                        return;
                    }
                    if (Server.devs.Contains(message.ToLower()))
                    {
                        Player.SendMessage(p, "You can't ban a MCForge Developer!");
                        if (p != null)
                        {
                            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " attempted to ban a MCForge Developer!");
                        }
                        else
                        {
                            Player.GlobalMessage(Server.DefaultColor + "The Console attempted to ban a MCForge Developer!");
                        }
                        return;
                    }
                    Group foundGroup = Group.findPlayerGroup(message);

                    if (foundGroup.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, "You can't ban a " + foundGroup.name + "!");
                        return;
                    }
                    if (foundGroup.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, message + " is already banned.");
                        return;
                    }

                    foundGroup.playerList.Remove(message);
                    foundGroup.playerList.Save();

                    Player.GlobalMessage(message + " &f(offline)" + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + "!");
                    Group.findPerm(LevelPermission.Banned).playerList.Add(message);
                }
                else
                {
                    if (!Player.ValidName(who.name))
                    {
                        Player.SendMessage(p, "Invalid name \"" + who.name + "\".");
                        return;
                    }
                    if (Server.devs.Contains(who.name.ToLower()))
                    {
                        Player.SendMessage(p, "You can't ban a MCForge Developer!");
                        if (p != null)
                        {
                            Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " attempted to ban a MCForge Developer!");
                        }
                        else
                        {
                            Player.GlobalMessage(Server.DefaultColor + "The Console attempted to ban a MCForge Developer!");
                        }
                        return;
                    }
                    if (who.group.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, "You can't ban a " + who.group.name + "!");
                        return;
                    }
                    if (who.group.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, message + " is already banned.");
                        return;
                    }

                    who.group.playerList.Remove(message);
                    who.group.playerList.Save();

                    if (stealth) Player.GlobalMessageOps(who.color + who.name + Server.DefaultColor + " is now STEALTH &8banned" + Server.DefaultColor + "!");
                    else Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + "!", false);

                    who.group = Group.findPerm(LevelPermission.Banned);
                    who.color = who.group.color;
                    Player.GlobalDie(who, false);
                    Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                    Group.findPerm(LevelPermission.Banned).playerList.Add(who.name);
                }
                Group.findPerm(LevelPermission.Banned).playerList.Save();

                //IRCBot.Say(message + " was banned.");
                Server.IRC.Say(message + " was banned.");
                Server.s.Log("BANNED: " + message.ToLower());

                if (totalBan == true)
                {
                    Command.all.Find("undo").Use(p, message + " 0");
                    Command.all.Find("banip").Use(p, "@ " + message);
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ban <player> - Bans a player without kicking him.");
            Player.SendMessage(p, "Add # before name to stealth ban.");
            Player.SendMessage(p, "Add @ before name to total ban.");
        }
    }
}