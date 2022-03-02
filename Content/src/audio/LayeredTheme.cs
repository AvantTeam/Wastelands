using Microsoft.Xna.Framework.Media;

namespace wastelands.src.audio
{
    public class LayeredTheme
    {
        public Song
            main,
            attack,
            boss;

        public LayeredTheme(Song main, Song attack, Song boss)
        {
            this.main = main;
            this.attack = attack;
            this.boss = boss;
        }
    }
}
