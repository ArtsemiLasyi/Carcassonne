using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    public static class GameSettings
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1080;
        public const int MAXPLAYERS = 5;
        public const bool ISFULLSCREEN = false;
        public static GameState GAMESTATE = GameState.chat;
        public static string TEXTUREBACKGROUND = "Images/menu/treeTextureNew1";
        public static string TEXTUREEXIT = "Images/menu/menuTxtExit";
        public static string TEXTUREMAINBACKGROUND = "Images/menu/menuBack";

        public enum GameState
        {
            chat,
            game
        }
    }
}
