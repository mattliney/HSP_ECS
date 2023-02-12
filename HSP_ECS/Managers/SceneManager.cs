using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HSP_ECS
{
    public class SceneManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int mScreenWidth = 1080;
        private int mScreenHeight = 720;

        public SceneManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = mScreenHeight;
            _graphics.PreferredBackBufferWidth = mScreenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            //Texture2D t = Content.Load<Texture2D>("corn2");
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