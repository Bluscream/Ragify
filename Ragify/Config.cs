using Rage;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Ragify
{
	public static class Config
	{
        public static InitializationFile initializationFile;

        public static Dictionary<string, Keys> Mappings;

		public static void MapKey(string slot, string key)
		{
			if (!Config.Mappings.ContainsKey(slot))
			{
				Config.Mappings.Add(slot, (Keys)Enum.Parse(typeof(Keys), key, true));
				return;
			}
			Config.Mappings[slot] = (Keys)Enum.Parse(typeof(Keys), key, true);
		}

		public static Keys GetKey(string slot)
		{
			return Config.Mappings[slot];
		}

		public static void Load()
		{
            var ini = "Plugins/Ragify.ini";
            try
            {
                (new FileInfo(ini)).Directory.Create();
                Config.Mappings = new Dictionary<string, Keys>();
                initializationFile = new InitializationFile(ini);
                if (!File.Exists(ini))
                {
                    Game.Console.Print($"[Ragify] File {ini} doesn't exist, creating new one.");
                    initializationFile.Write("General", "debug", "false");
                    initializationFile.Write("General", "checkForUpdates", "true");
                    initializationFile.Write("General", "showDisplayOnStartup", "true");
                    initializationFile.Write("General", "showSubtitles", "true");
                    initializationFile.Write("General", "showNotifications", "false");
                    initializationFile.Write("General", "showHelpNotifications", "false");
                    initializationFile.Write("Controls", "toggleDisplay", "Numpad8");
                    initializationFile.Write("Controls", "togglePlayback", "Numpad0");
                    initializationFile.Write("Controls", "nextTrack", "Numpad3");
                    initializationFile.Write("Controls", "previousTrack", "Numpad1");
                    initializationFile.Write("Controls", "volumeUp", "Numpad5");
                    initializationFile.Write("Controls", "volumeDown", "Numpad2");
                }
                Config.MapKey("nextTrack", initializationFile.ReadString("Controls", "nextTrack", "Numpad3"));
                Config.MapKey("previousTrack", initializationFile.ReadString("Controls", "previousTrack", "Numpad1"));
                Config.MapKey("togglePlayback", initializationFile.ReadString("Controls", "togglePlayback", "Numpad0"));
                Config.MapKey("volumeUp", initializationFile.ReadString("Controls", "volumeUp", "Numpad5"));
                Config.MapKey("volumeDown", initializationFile.ReadString("Controls", "volumeDown", "Numpad2"));
                Config.MapKey("toggleDisplay", initializationFile.ReadString("Controls", "toggleDisplay", "Numpad8"));
            } catch
            {
                Game.Console.Print($"[Ragify] ERROR: Unable to read {ini}. Please validate it exists and is set up correctly!");
            }
		}
	}
}
