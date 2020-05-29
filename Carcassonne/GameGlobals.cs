using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Carcassonne
{
    public static class GameGlobals
    {
        public static Server Server;
        public static Player Player;
        public const int PORT = 8888;
        public const int WIDTH = 1920;
        public const int HEIGHT = 1080;
        public const int MAXPLAYERS = 2;
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
        public static string TEXTURENEWSERVER = "Images/menu/menuNew";
        public static string TEXTURESENDTEXT = "Images/menu/sendText";
        public static string TEXTURECHAT = "Images/menu/menuChat";
        public static string[] NICKNAMES = { "PLAYER1", "PLAYER2" };
        public static string message = "";
        public static string chat = "";

        public enum GameState
        {
            chat,
            game
        }

        public static void Initialize()
        {
            chat = "";
            message = "";
        }

        public static bool isIP(string ip)
        {
            Regex regex = new Regex(@"([0-9]{1,3}[\.]){3}[0-9]{1,3}");
            MatchCollection matches = regex.Matches(ip);
            if (matches.Count == 1)
                return true;
            else
                return false;
        }

        public static string UpdateString(string str)
        {
            MyKeyboard.GetCurrentState();
            str = MyKeyboard.getInput(str);
            if (str.Length > 23)
                str = str.Substring(0, str.Length - 1);
            return str;
        }

        public static string UpdateString(string str, int maxlength)
        {
            MyKeyboard.GetCurrentState();
            str = MyKeyboard.getInput(str);
            if (str.Length > maxlength)
                str = str.Substring(0, str.Length - 1);
            return str;
        }

        public static void StartServer()
        {
            try
            {
                Server = new Server();
                Task.Factory.StartNew(Server.Listen);
            }
            catch (Exception ex){ Server.Disconnect();}
        }

        public static void StopServer()
        {
            if (Server != null)
            {
                Server.Disconnect();
            }
        }

        public static string LocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return null;
        }
    }
}
