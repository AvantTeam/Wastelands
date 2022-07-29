using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using wastelands.src.map;

namespace wastelands.src.game
{
    public class TestGameMode : GameMode
    {
        public Tilemap tilemap = new Tilemap();

        public override void Initialize()
        {
            TilemapData tilemapData = MapGen.PlaceRooms(MapGen.Generate(5));
            tilemap.AddChunk(tilemapData.tiles, Vector2.Zero);
        }

        public override void Update(GameTime gameTime)
        {
            Vars.camera.Update();
        }

        public override void Draw(GameTime gameTime, GraphicsDevice device, SpriteBatch batch)
        {
            tilemap.Draw();
        }
    }
}
