using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoTileMapEx
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D _character;
        Vector2 ViewportCentre
        {
            get
            {
                return new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            }
        }

        List<Texture2D> tileTextures = new List<Texture2D>();
        int[,] tileMap = new int[,]  
    {
        {2,2,2,2,2,2,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},

    };
        int[,] hiddenMap = new int[,]  
    {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},

    };


        int tileWidth = 64;
        int tileHeight = 64;
        Vector2 cameraPosition = new Vector2(0,0);
        float cameraSpeed = 5;
        private Rectangle _characterRect;
        Vector2 WorldBounds;
        KeyboardState oldState;
        int impassible = 1;
        public enum TileType { Dirt,Grass,Ground,Mud,Road,Rock,Wood};
        SpriteFont debug;
        TileManager _tManager;
        private byte pulseColor;
        Camera cam;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = tileWidth * 12;
            graphics.PreferredBackBufferHeight = tileWidth * 6;
            WorldBounds = new Vector2(tileMap.GetLength(1) * tileWidth, tileMap.GetLength(0) * tileHeight);

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
            _tManager = new TileManager();
            cam = new Camera(ViewportCentre, 
                new Vector2(tileMap.GetLength(1) * tileWidth, 
                                tileMap.GetLength(0) * tileHeight));

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
            
            // assumes dirt is 0
            Texture2D dirt = Content.Load<Texture2D>("Tiles/se_free_dirt_texture");
            tileTextures.Add(dirt);

            Texture2D grass = Content.Load<Texture2D>("Tiles/se_free_grass_texture");
            tileTextures.Add(grass);

            Texture2D ground = Content.Load<Texture2D>("Tiles/se_free_ground_texture");
            tileTextures.Add(ground);

            _character = Content.Load<Texture2D>("chaser");
            debug = Content.Load<SpriteFont>("debug");
            string[] backTileNames = { "free", "grass", "ground" };
            string[] impassibleTiles = {"free","ground"};
            string[] hiddenTileNames = { "NONE", "chest", "key" };

            _tManager.addLayer("hidden", hiddenTileNames, hiddenMap);
            int mapWidth = _tManager.Layers[0].MapWidth;
            _tManager.addLayer("background", backTileNames, tileMap);
            _tManager.ActiveLayer = _tManager.getLayer("background");
            _tManager.ActiveLayer.makeImpassable(impassibleTiles);
            _tManager.CurrentTile = new Tile();
            _tManager.CurrentTile.TileName = "Character";
            _tManager.CurrentTile.X = 6;
            _tManager.CurrentTile.Y = 3;
            _characterRect = new Rectangle(tileWidth * _tManager.CurrentTile.X, tileHeight * _tManager.CurrentTile.Y, tileWidth, tileHeight);
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            // TODO: Add your update logic here
            
            
            KeyboardState keyState = Keyboard.GetState();
            
            
            // check movement of the character against the adjecent tiles in the current active layer
            Tile previousTile = _tManager.CurrentTile;

            if (keyState.IsKeyDown(Keys.W) )
            {
                if (_tManager.ActiveLayer.valid("above", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("above", _tManager.CurrentTile);
            }
            // && !oldState.IsKeyDown(Keys.S)
            if (keyState.IsKeyDown(Keys.S) )
            {
                if (_tManager.ActiveLayer.valid("below", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("below", _tManager.CurrentTile);
            }
            
            if (keyState.IsKeyDown(Keys.A) )
                if (_tManager.ActiveLayer.valid("left", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("left", _tManager.CurrentTile);

            if (keyState.IsKeyDown(Keys.D) )
                if (_tManager.ActiveLayer.valid("right", _tManager.CurrentTile))
                    _tManager.CurrentTile =
                        _tManager.ActiveLayer.getadjacentTile("right", _tManager.CurrentTile);

            //if (keyState.IsKeyDown(Keys.Right) )
            //    cameraPosition += new Vector2(1, 0) * cameraSpeed;
            //if (keyState.IsKeyDown(Keys.Left) )
            //    cameraPosition += new Vector2(-1, 0) * cameraSpeed;
            //if (keyState.IsKeyDown(Keys.Up) )
            //    cameraPosition += new Vector2(0,-1) * cameraSpeed;
            //if (keyState.IsKeyDown(Keys.Down) )
            //    cameraPosition += new Vector2(0,1) * cameraSpeed;

            // Calculate the new rectangle position to draw the character in

            Rectangle r = new Rectangle(_tManager.CurrentTile.X * tileWidth,
                                            _tManager.CurrentTile.Y * tileHeight, tileWidth, tileHeight);
                bool inView = GraphicsDevice.Viewport.Bounds.Contains(r);
                bool passable = _tManager.ActiveLayer.Tiles[_tManager.CurrentTile.Y, _tManager.CurrentTile.X].Passable;
                //Vector2 PossibleCameraMove = new Vector2(_characterRect.X - GraphicsDevice.Viewport.Bounds.Width / 2,
                //                                    _characterRect.Y - GraphicsDevice.Viewport.Bounds.Height / 2);
                if (passable)
                {
                    _characterRect = r;
                }
                else
                {
                    _tManager.CurrentTile = previousTile;
                }
            cam.follow(new Vector2((int)_characterRect.X, (int)_characterRect.Y), 
                GraphicsDevice.Viewport);

            oldState = keyState;
            base.Update(gameTime);
        }

        Vector2 TileDifference(Tile t1, Tile t2)
        {
            Vector2 v1 = new Vector2(t1.X, t1.Y) * tileWidth;
            Vector2 v2 = new Vector2(t2.X, t2.Y) * tileWidth;
            Vector2 result = v1 - v2;
            return result;
        }
     
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //drawUsingTileCamera();
            drawUsingPlayerCamera();
            // TODO: Add your drawing code here
                    base.Draw(gameTime);
        }

        public void drawUsingPlayerCamera()
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, 
                BlendState.AlphaBlend, null, null, null, null, cam.CurrentCameraTranslation);
            TileLayer background = _tManager.getLayer("background");
            List<Tile> surroundingTiles = background.adjacentTo(_tManager.CurrentTile);
            for (int x = 0; x < background.MapWidth; x++)
                for (int y = 0; y < background.MapHeight; y++)
                {
                    int textureIndex = background.Tiles[y, x].Id;
                    Texture2D texture = tileTextures[textureIndex];
                    // Draw surrounding tiles
                    if (surroundingTiles.Contains(background.Tiles[y, x]))
                    {
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth,
                          y * tileHeight,
                          tileWidth,
                          tileHeight),
                            new Color(255, 255, 255, pulseColor++));
                    }
                    else
                    {
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth,
                          y * tileHeight,
                          tileWidth,
                          tileHeight),
                            Color.White);
                    }

                }
            // draw the character

            spriteBatch.Draw(_character, new Rectangle(_tManager.CurrentTile.X * tileWidth,
                          _tManager.CurrentTile.Y * tileHeight,
                          tileWidth,
                          tileHeight),
                            Color.White);

            //spriteBatch.Draw(_character, _characterRect, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(debug, cameraPosition.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(debug, new Vector2(_characterRect.X, _characterRect.Y).ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.End();


        }

        public void drawUsingTileCamera()
        {
            int tileMapWidth = tileMap.GetLength(1); // number of columns
            int tileMapHeight = tileMap.GetLength(0); // number of rows
            spriteBatch.Begin();

            // Draw the background texture
            TileLayer background = _tManager.getLayer("background");
            List<Tile> surroundingTiles = background.adjacentTo(_tManager.CurrentTile);
            for (int x = 0; x < background.MapWidth; x++)
                for (int y = 0; y < background.MapHeight; y++)
                {
                    int textureIndex = background.Tiles[y, x].Id;
                    Texture2D texture = tileTextures[textureIndex];
                    // Draw surrounding tiles
                    if (surroundingTiles.Contains(background.Tiles[y, x]))
                    {
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth - (int)cameraPosition.X,
                          y * tileHeight - (int)cameraPosition.Y,
                          tileWidth,
                          tileHeight),
                            new Color(255, 255, 255, pulseColor++));
                    }
                    else
                    {
                        spriteBatch.Draw(texture,
                            new Rectangle(x * tileWidth - (int)cameraPosition.X,
                          y * tileHeight - (int)cameraPosition.Y,
                          tileWidth,
                          tileHeight),
                            Color.White);
                    }

                }
            // draw the character
            spriteBatch.Draw(_character, new Rectangle(_tManager.CurrentTile.X * tileWidth - (int)cameraPosition.X,
                          _tManager.CurrentTile.Y * tileHeight - (int)cameraPosition.Y,
                          tileWidth,
                          tileHeight),
                            Color.White);

            spriteBatch.End();

        }
    }
}
