using Rage;
using System;
using System.IO;
using System.Net;

namespace Ragify
{
	public static class Updates
	{
		public static bool UpdatesFound;

		public static bool Notified;

		public static string UpdateServer = "http://ragify.riekelt.nu";

		public static string CurrentVersion = "1.3.0";

		public static void CheckForUpdates()
		{
			string text = Updates.Get(Updates.UpdateServer + "/version.txt");
			if (text.Trim() != Updates.CurrentVersion.Trim())
			{
				Updates.Notified = true;
				WidgetManager.Drawn["Update"] = true;
				WidgetManager.Registered["Update"].SetMappedString("Version", text);
			} else
            {
                Game.Console.Print("[Ragify] No new update found");
            }
		}

		private static string Get(string uri)
		{
            try
            {
                string result;
                using (WebResponse response = WebRequest.Create(uri).GetResponse())
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                return result;
            } catch
            {
                Game.Console.Print($"[Ragify] Could not check for update; {uri} unreachable!");
                return null;
            }
		}
	}
}
