using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using wastelands.src.map;
using wastelands.src.graphics;
using wastelands.src.collisions;
using System;

namespace wastelands.src.game
{
    public class TestGameMode : GameMode
    {
        private Tilemap tilemap = new Tilemap();
        private SpriteBatch spriteBatch;
        private Effect auraEffect;
        private QuadTree quadTree;
        private QuadTree drawQuadTree;
        private Texture2D pixel;
        private Vector2 playerPosition = Vector2.Zero;
        private Point point32 = new Point(32, 32);

        public override void Initialize()
        {
            TilemapData tilemapData = MapGen.PlaceRooms(MapGen.Generate(30));
            tilemap.AddChunk(tilemapData.tiles, Vector2.Zero);

            quadTree = new QuadTree(0, tilemap.GetBounds());
            tilemap.AddToQuadTree(quadTree);
            drawQuadTree = quadTree;
        }

        public override void LoadContent(ContentManager content, GraphicsDevice device)
        {
            spriteBatch = new SpriteBatch(device);
            auraEffect = Effects.auraEffect.Clone();
            pixel = content.Load<Texture2D>("sprites/pixel");
        }

        public override void Update(GameTime gameTime)
        {
            Vars.camera.Update();

            playerPosition = Vars.camera.position + Vars.screenSize / 2;

            /*quadTree.Clear();
            tilemap.AddToQuadTree(quadTree);
            drawQuadTree = quadTree;*/
        }

        public override void Draw(GameTime gameTime, GraphicsDevice device)
        {
            auraEffect.Parameters["Resolution"].SetValue(new Vector2(device.Viewport.Width, device.Viewport.Height));
            auraEffect.Parameters["ColorAddition"].SetValue(new Vector4(0.2f, 0.1f, 0f, 0f));

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: auraEffect);

            tilemap.Draw(spriteBatch);
            if(drawQuadTree != null)
            {
                Rectangle lol = new Rectangle(playerPosition.ToPoint(), point32);

                foreach (Collider collider in drawQuadTree.GetColliders())
                {
                    Rectangle rect = collider.bounds;
                    spriteBatch.Draw(pixel, new Rectangle(rect.X - (int)Vars.camera.position.X - 16, rect.Y - (int)Vars.camera.position.Y - 16, 32, 32), Color.Gray);
                    //spriteBatch.Draw(pixel, new Rectangle(rect.X - (int)Vars.camera.position.X - 16, rect.Y - (int)Vars.camera.position.Y - 16, 32, 32), Color.White);
                }

                foreach (Collider collider in drawQuadTree.GetPossibleColliders(lol))
                {
                    Rectangle rect = collider.bounds;
                    if (Math.Sqrt(Math.Pow(rect.X - lol.X, 2) + Math.Pow(rect.Y - lol.Y, 2)) > 5 * 32) continue;
                    spriteBatch.Draw(pixel, new Rectangle(rect.X - (int)Vars.camera.position.X - 16, rect.Y - (int)Vars.camera.position.Y - 16, 32, 32), Color.Magenta);
                }

                foreach (Rectangle rect in drawQuadTree.GetNodeBounds())
                {
                    NinePatchRenderable renderable = Tex.Get("rect").createRenderable(rect.Width, rect.Height);
                    renderable.Render(rect.X - (int)Vars.camera.position.X - 16, rect.Y - (int)Vars.camera.position.Y - 16, spriteBatch);
                    //spriteBatch.Draw(pixel, new Rectangle(rect.X - (int)Vars.camera.position.X - 16, rect.Y - (int)Vars.camera.position.Y - 16, 32, 32), Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}
