using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace game
{
    static class Victorina
    {
        static public List<string> list = new List<string>();
        static public int gameDuration = 60; // продолжительность игры
        static public int musicDuration = 10; // время для музыки
        static public bool randomStart = false; // как начинать музыку
        static public string lastFolder = "";
        static public bool allDirectories = false;
        static public string answer = "";

        static public void ReadMusic()
        {
            try
            {
                string[] music_files = Directory.GetFiles(lastFolder, "*.mp3", allDirectories ? SearchOption.AllDirectories : SearchOption.AllDirectories);
                list.Clear();
                list.AddRange(music_files);
            }
            catch
            {
                
            }
        }

        static string regKeyName = "Software\\MyCompanyName\\GuessMelody";

        public static void WriteParam()
        {
            RegistryKey rk = null; // создаем класс для работы с ключами реестра 
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;
                rk.SetValue("LastFolder", lastFolder);
                rk.SetValue("RandomStart", randomStart);
                rk.SetValue("GameDuration", gameDuration);
                rk.SetValue("MusicDuration", musicDuration);
                rk.SetValue("AllDirictories", allDirectories);
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }

        public static void ReadParam()
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    lastFolder = (string)rk.GetValue("LastFolder");
                    gameDuration = (int)rk.GetValue("GameDuration");
                    randomStart = Convert.ToBoolean(rk.GetValue("RandomStart", false));
                    musicDuration = (int)rk.GetValue("MusicDuration");
                    allDirectories = Convert.ToBoolean((string)rk.GetValue("AllDirictories", false));
                }
            }
            finally
            {
                if (rk != null) rk.Close();
            }
        }
    }
}
    