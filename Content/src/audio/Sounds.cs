using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace wastelands.src.audio
{
    public static class Sounds
    {
        public static Song wawtealdbad;

        public static void Load(ContentManager manager)
        {
            wawtealdbad = manager.Load<Song>("audio/wawtealdbad");
        }
    }
}
