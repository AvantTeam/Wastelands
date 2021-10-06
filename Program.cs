using System;
using Microsoft.Xna.Framework;

namespace wastelands
{
    public static class Program
    {
        public static Wastelands game = new Wastelands();

        [STAThread]
        static void Main()
        {
            game.Run();
        }
    }
}
