using System;

namespace wastelands
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new Wastelands();
            game.Run();
        }
    }
}
