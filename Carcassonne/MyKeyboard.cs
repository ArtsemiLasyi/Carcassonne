using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Carcassonne
{
    public class MyKeyboard
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static KeyboardState GetCurrentState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static string getInput(string str)
        {
            foreach(var key in currentKeyState.GetPressedKeys())
            {
                if (HasBeenPressed(key))
                {
                    try
                    {
                        if (isBackSpace(key))
                            str = str.Substring(0, str.Length - 1);
                        if (isLetter(key))
                            str += key.ToString();
                        if (isDot(key))
                            str += ".";
                        if (isNumber(key))
                            str += key.ToString().Substring(1);
                        return str;
                    }
                    catch { return str; }
                }
            }
            return str;
        }


        public static bool isBackSpace(Keys key)
        {
            if (key == Keys.Back)
                return true;
            else
                return false;
        }

        public static bool isEnter(Keys key)
        {
            if (key == Keys.Enter)
                return true;
            else
                return false;
        }

        public static bool isDot(Keys key)
        {
            if (key == Keys.OemPeriod)
                return true;
            else
                return false;
        }

        public static bool isNumber(Keys key)
        {
            if ((key >= Keys.D0)&&(key <= Keys.D9))
                return true;
            else
                return false;
        }

        public static bool isLetter(Keys key)
        {
            if ((key >= Keys.A) && (key <= Keys.Z))
                return true;
            else
                return false;
        }

    }
}
