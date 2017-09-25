using System;
using Otter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Program
    {
        public static Game game { get; set; } = new Game("IsaacCopy", 1366, 768, 60, false);
        static GameHandler gh = new GameHandler();
        static void Main(string[] args)
        {
            game.MouseVisible = true;
            game.Start(gh.StartScene());
        }
    }
}
