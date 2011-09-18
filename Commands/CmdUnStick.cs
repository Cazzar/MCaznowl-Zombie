using System;
using System.IO;

namespace MCForge
{
    public class CmdUnStick : Command
    {
        public override string name { get { return "unstick"; } }
        public override string shortcut { get { return "us"; } }
        public override string type { get { return "zombie"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdUnStick() { }
        public override void Use(Player p, string message)
        {
            Player who = null;
            if (message == "") { who = p; message = p.name; } else { who = Player.Find(message); }
            ushort x = (ushort)((0.5 + who.level.spawnx) * 32);
            ushort y = (ushort)((1 + who.level.spawny) * 32);
            ushort z = (ushort)((0.5 + who.level.spawnz) * 32);
            if (!p.referee)
            {
                if (Server.infection && !CmdZombieGame.infect.Contains(who))
                {
                    CmdZombieGame.infect.Add(who);
                    CmdZombieGame.players.Remove(who);
                    who.color = c.red;
                    Player.GlobalDie(who, false);
                    Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                }
            }
            unchecked
            {
                who.SendPos((byte)-1, x, y, z,
                            who.level.rotx,
                            who.level.roty);
            }
            who.NoClipcount = 0;
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unstick [player] - Unsticks [player] (NOT IN USE ATM)");
        }
    }
}