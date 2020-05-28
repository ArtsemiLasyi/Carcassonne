using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MenuScreen MainMenu = new MenuScreen(new List<GraphObject> { });
        GameScreen GameMenu = new GameScreen(new List<GraphObject> { });
        HelpScreen HelpMenu = new HelpScreen(new List<GraphObject> { });
        StartGameScreen GameProcess = new StartGameScreen(new List<GraphObject> { });
        Screens gameScreens = new Screens(new List<Screen>());
        List<Song> playlist = new List<Song>();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = GameSettings.ISFULLSCREEN;
            graphics.PreferredBackBufferWidth = GameSettings.WIDTH;
            graphics.PreferredBackBufferHeight = GameSettings.HEIGHT;
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

            // TODO: use this.Content to load your game content here
            MainMenu.LoadContent(Content, GameSettings.WIDTH, GameSettings.HEIGHT);
            GameMenu.LoadContent(Content, GameSettings.WIDTH, GameSettings.HEIGHT);
            HelpMenu.LoadContent(Content, GameSettings.WIDTH, GameSettings.HEIGHT);
            GameProcess.LoadContent(Content, GameSettings.WIDTH, GameSettings.HEIGHT);
            gameScreens.screenList.Add(MainMenu);
            gameScreens.screenList.Add(GameMenu);
            gameScreens.screenList.Add(HelpMenu);
            gameScreens.screenList.Add(GameProcess);
            Song maintheme = Content.Load<Song>("Music/maintheme");
            Song gametheme = Content.Load<Song>("Music/Adrian von Ziegler - At the Summertide Feast");
            Song gametheme1 = Content.Load<Song>("Music/Crusader Kings II OST - In Taberna (192  kbps)");
            Song gametheme2 = Content.Load<Song>("Music/kingdom_come_deliverance_03. Till Our Heads Turn White");
            Song gametheme3 = Content.Load<Song>("Music/kingdom_come_deliverance_23. Poverty And Famine");
            Song gametheme4 = Content.Load<Song>("Music/Medieval and Action Music (Kingdom Come Deliverance Original Soundtrack)/2. Fill Them Up Once More");
            playlist.Add(gametheme);
            playlist.Add(gametheme1);
            playlist.Add(gametheme2);
            playlist.Add(gametheme3);
            playlist.Add(gametheme4);
            playlist.Add(maintheme);
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

            // TODO: Add your update logic here
            foreach (Screen screen in gameScreens.screenList)
            {
                if (screen.IsActive)
                {
                    screen.Update(gameTime);
                    switch (screen.changeScreen)
                    {
                        case PossibleScreen.main:
                            MainMenu.Show();
                            break;
                        case PossibleScreen.game:
                            GameMenu.Show();
                            break;
                        case PossibleScreen.help:
                            HelpMenu.Show();
                            break;
                        case PossibleScreen.startgame:
                            GameProcess.Show();
                            break;
                        case PossibleScreen.quit:
                            Exit();
                            break;
                    }
                    break;
                }
            }
            UpdateSong(playlist);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Screen screen in gameScreens.screenList)
            {
                if (screen.IsActive)
                {
                    screen.Draw(gameTime, spriteBatch);
                }
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }

        private void UpdateSong(List<Song> playlist)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                Random rnd = new Random();
                int x = rnd.Next(playlist.Count);
                int i = 0;
                foreach (var song in playlist)
                {
                    if (x == i)
                    {
                        MediaPlayer.Play(song);
                        break;
                    }
                    i++;
                }
            }
        }
    }
}
