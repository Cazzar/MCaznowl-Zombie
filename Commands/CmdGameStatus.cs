using System;
using System.IO;

namespace MCForge
{
    public class CmdGameStatus : Command
    {
        public override string name { get { return "gamestatus"; } }
        public override string shortcut { get { return "gs"; } }
        public override string type { get { return "zombie"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdGameStatus() { }
        public override void Use(Player p, string message)
        {
            p.SendMessage(c.red + Server.ZombieModeOn + Server.DefaultColor + " <- should always be true");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gamestatus - Checks zombie games's status.");
        }
    }
}