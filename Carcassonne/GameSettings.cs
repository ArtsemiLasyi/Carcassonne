using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    public static class GameSettings
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1080;
        public const int MAXPLAYERS = 5;
        public const bool ISFULLSCREEN = true;
        public static GameState GAMESTATE = GameState.chat;
        public static string TEXTUREBACKGROUND = "Images/menu/treeTextureNew1";
        public static string TEXTUREEXIT = "Images/menu/menuTxtExit";
        public static string TEXTUREMAINBACKGROUND = "Images/menu/menuBack";
        public static string TEXTUREHELP = "Images/menu/menuTxtHelp";
        public static string TEXTURENEWGAME = "Images/menu/menuTxtStartGame";
        public static string TEXTURELOGO = "Images/menu/menuGameNameEn";
        public static string TEXTUREINPUTFORM = "Images/menu/inputForm";
        public static string TEXTURESTARTNEWGAME = "Images/menu/menuStartNewGame";
        public static string TEXTUREJOIN = "Images/menu/menuTxtJoin";

        public enum GameState
        {
            chat,
            game
        }
    }
}
