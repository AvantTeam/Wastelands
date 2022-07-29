using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using wastelands.src.map;
using wastelands.src.graphics;

namespace wastelands.src.game
{
    public class TestGameMode : GameMode
    {
        private Tilemap tilemap = new Tilemap();
        private SpriteBatch spriteBatch;
        private Effect auraEffect;

        public override void Initialize()
        {
            TilemapData tilemapData = MapGen.PlaceRooms(MapGen.Generate(150));
            tilemap.AddChunk(tilemapData.tiles, Vector2.Zero);
        }

        public override void LoadContent(ContentManager content, GraphicsDevice device)
        {
            spriteBatch = new SpriteBatch(device);
            auraEffect = Effects.auraEffect.Clone();
        }

        public override void Update(GameTime gameTime)
        {
            Vars.camera.Update();
        }

        public override void Draw(GameTime gameTime, GraphicsDevice device)
        {
            auraEffect.Parameters["Resolution"].SetValue(new Vector2(device.Viewport.Width, device.Viewport.Height));
            auraEffect.Parameters["ColorAddition"].SetValue(new Vector4(0.2f, 0.1f, 0f, 0f));

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: auraEffect);

            tilemap.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
