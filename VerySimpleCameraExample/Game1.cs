using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;
namespace VerySimpleCameraExample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 _camPos = Vector2.Zero;
        Matrix _camTransMatrix;
        Texture2D _background;
        AnimatedSprite _player;
        Vector2 WorldBounds = new Vector2(5000, 5000);
        private float speed = 5.0f;
        Vector2 ViewportCentre { get {
                return new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
                } }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("bigback3000x3000");
            _player = new AnimatedSprite(
                Content.Load<Texture2D>("runright"),
                ViewportCentre,
                6
                );
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Update(gameTime);
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _player.Move(new Vector2(1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _player.Move(new Vector2(-1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _player.Move(new Vector2(0, -1) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _player.Move(new Vector2(0, 1) * speed);
            
            _player.position = Vector2.Clamp(_player.position,
                    Vector2.Zero,
                    (new Vector2(_background.Width,_background.Height)
                              - new Vector2(_player.SpriteWidth,_player.SpriteHeight)      )
                );

            _camPos = _player.position -
                ViewportCentre;
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero,
                (new Vector2(_background.Width,_background.Height) - ViewportCentre * 2));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _camTransMatrix = 
                Matrix.CreateTranslation(
                    new Vector3(-_camPos.X, -_camPos.Y, 0));

            spriteBatch.Begin(SpriteSortMode.Immediate,
                                BlendState.AlphaBlend,
                                null, null, null, null, _camTransMatrix);
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _player.Draw(spriteBatch);
            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
