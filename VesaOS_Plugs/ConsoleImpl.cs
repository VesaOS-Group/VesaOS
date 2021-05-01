using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using IL2CPU.API.Attribs;

namespace VesaOS_Plugs
{
    [Plug(Target = typeof(global::System.Console))]
    public static class ConsoleImpl
    {
        public static bool CursorVisible { get { return mCursorVisible; } set { Terminal.DisableCursor(); mCursorVisible = false; } }
        public static ConsoleColor BackgroundColor { get { return Terminal.BackColor; } set { Terminal.BackColor = value; } }
        public static ConsoleColor ForegroundColor { get { return Terminal.TextColor; } set { Terminal.TextColor = value; } }
        private static bool mCursorVisible = true;
        public static void WriteLine(string text) { Terminal.WriteLine(text); }
        public static void WriteLine() { Terminal.WriteLine(""); }
        public static void WriteLine(object text) { Terminal.WriteLine(text.ToString()); }
        public static void Write(string text) { Terminal.Write(text); }
        public static void Write(object text) { Terminal.Write(text.ToString()); }
        public static string ReadLine() { return Terminal.ReadLine(); }
        public static void Clear() { Terminal.Clear(); }

    }
}
