using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using HSP_ECS.Helpers;

namespace HSP_ECS
{
    public class SceneManager : Game
    {
        //Graphics Variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int mScreenWidth = 1080;
        private int mScreenHeight = 720;

        //Resource Loader
        public ResourceLoaderHelper ResourceLoader;

        //Delegates
        public delegate void RenderDelegate();
        public delegate void UpdateDelegate(GameTime pGameTime);

        public RenderDelegate Render;
        public UpdateDelegate Updater;

        public SceneManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = mScreenHeight;
            _graphics.PreferredBackBufferWidth = mScreenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ResourceLoader = new ResourceLoaderHelper(this);

            GameScene gs = new GameScene(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //SpriteBatch.Draw(t, new Vector2(100, 100), Color.White);

            SpriteBatch.End();
        }

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }
    }
}