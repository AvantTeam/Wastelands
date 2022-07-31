using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using wastelands.src.map;
using wastelands.src.graphics;
using wastelands.src.collisions;
using wastelands.src.utils;

namespace wastelands.src.game
{
    public class TestGameMode : GameMode
    {
        private Tilemap tilemap = new Tilemap();
        private SpriteBatch spriteBatch, slimeBatch;
        private Effect auraEffect, slimeEffect;
        private QuadTree quadTree;
        private Texture2D slimeTest;
        private Vector2 playerPosition = Vector2.Zero;
        private Point point32 = new Point(32, 32);

        public override void Initialize()
        {
            TilemapData tilemapData = MapGen.PlaceRooms(MapGen.Generate(3));
            tilemap.AddChunk(tilemapData.tiles, Vector2.Zero);

            quadTree = new QuadTree(0, tilemap.GetBounds());
            tilemap.AddToQuadTree(quadTree);
        }

        public override void LoadContent(ContentManager content, GraphicsDevice device)
        {
            spriteBatch = new SpriteBatch(device);
            slimeBatch = new SpriteBatch(device);
            auraEffect = Effects.auraEffect.Clone();
            slimeEffect = Effects.slimeEffect.Clone();
            slimeTest = content.Load<Texture2D>("sprites/slimeTest");
        }

        public override void Update(GameTime gameTime)
        {
            Vars.camera.Update();

            playerPosition = Vars.camera.position + Vars.screenSize / 2;
        }

        public override void Draw(GameTime gameTime, GraphicsDevice device)
        {
            auraEffect.Parameters["Resolution"].SetValue(new Vector2(device.Viewport.Width, device.Viewport.Height));
            auraEffect.Parameters["ColorAddition"].SetValue(new Vector4(0.2f, 0.1f, 0f, 0f));

            slimeEffect.Parameters["Resolution"].SetValue(new Vector2(device.Viewport.Width, device.Viewport.Height));
            slimeEffect.Parameters["ColorAddition"].SetValue(new Vector4(0.2f, 0.1f, 0f, 0f));
            slimeEffect.Parameters["Color"].SetValue(new Vector4(0.50588235294f, 0.75686274509f, 0.5725490196f, 0f));
            slimeEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: auraEffect);
            slimeBatch.Begin(samplerState: SamplerState.PointClamp, effect: slimeEffect);

            Draww.DrawSprite(slimeBatch, slimeTest, Vars.screenSize / 2f - Vars.camera.position);

            tilemap.Draw(spriteBatch);

            spriteBatch.End();
            slimeBatch.End();
        }
    }
}
