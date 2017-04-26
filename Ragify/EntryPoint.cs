#define DEFAULT
using Rage;
using Rage.Attributes;
using Ragify.Widgets;
using System;
using System.Drawing;

namespace Ragify
{
	public static class EntryPoint
	{
        [ConsoleCommand]
        private static void Command_SpotifyInfo()
        {
            var t = Context.Spotify.GetStatus();
            Game.Console.Print($"SpotifyAPI Running: {isEnabled(t.Running)}");
            Game.Console.Print($"SpotifyAPI Online: {isEnabled(t.Online)}");
            Game.Console.Print($"Spotify Server Time: {t.ServerTime}");
            Game.Console.Print($"SpotifyAPI Version: {t.Version}");
            Game.Console.Print($"Spotify Client Version: {t.ClientVersion}");
            Game.Console.Print($"Spotify Muted: {isEnabled(Context.Spotify.IsSpotifyMuted())}");
            Game.Console.Print($"Spotify Volume: {Context.Spotify.GetSpotifyVolume()}");
            Game.Console.Print($"Private Session: {isEnabled(t.OpenGraphState.PrivateSession)}");
            Game.Console.Print($"Posting Disabled: {isEnabled(t.OpenGraphState.PostingDisabled)}");
        }
        [ConsoleCommand]
        private static void Command_TrackInfo()
        {
            var t = Context.Spotify.GetStatus();
            Game.Console.Print($"Track: {Context.CurrentTrack.TrackResource.Name}");
            Game.Console.Print($"Artist: {Context.CurrentTrack.ArtistResource.Name}");
            Game.Console.Print($"Time: {t.PlayingPosition}/{Context.CurrentTrack.Length}");
        }
        [ConsoleCommand]
        private static void Command_Play(string uri = null, string context = "")
        {
            if (uri != null)
                Context.Spotify.PlayURL(uri, context); 
            else
                Context.Spotify.Play();
        }
        [ConsoleCommand]
        private static void Command_Pause()
        {
            Context.Spotify.Pause();
        }
        [ConsoleCommand]
        private static void Command_Next()
        {
            Context.Spotify.Skip();
        }
        [ConsoleCommand]
        private static void Command_Previous()
        {
            Context.Spotify.Previous();
        }
        [ConsoleCommand]
        private static void Command_Mute()
        {
            Context.Spotify.Mute();
        }
        [ConsoleCommand]
        private static void Command_Unmute()
        {
            Context.Spotify.UnMute();
        }
        [ConsoleCommand]
        private static void Command_Volume(float volume)
        {
            Context.Spotify.SetSpotifyVolume(volume);
        }

        private static string isEnabled(bool boolean)
        {
            if (boolean)
                return "Yes";
            else
                return "No";
        }

        public static void Main()
		{
			Context.Initialize();
			Config.Load();
			WidgetManager.Initialize();
			WidgetManager.Register("Track", new TrackWidget(0f, 0f, 300f, 60f));
			WidgetManager.Register("Progress", new ProgressWidget(0f, 0f, 300f, 5f));
			WidgetManager.Register("Update", new UpdateWidget(0f, 0f, 300f, 30f));
			WidgetManager.Drawn["Update"] = false;
			WidgetManager.GetWidget("Track").SetPositionFromBottomRightCorner(new PointF(300f, 60f));
			WidgetManager.GetWidget("Update").SetPositionFromBottomRightCorner(new PointF(300f, 90f));
			WidgetManager.GetWidget("Progress").SetPositionFromBottomRightCorner(new PointF(300f, 5f));
            Game.Console.Print("[Ragify] Loaded successfully."); 
            Game.DisplayHelp("Ragify has successfully loaded.");
            Game.DisplayHelp("Use the [CTRL] key in combination with a command to control Spotify.");
            if (Config.initializationFile.ReadBoolean("General", "checkForUpdates", true))
                Updates.CheckForUpdates();
            else
                Game.Console.Print("[Ragify] Skipped update check based on config value");
            if (!Config.initializationFile.ReadBoolean("General", "showDisplayOnStartup", true))
            {
                WidgetManager.Drawn["Track"] = false;
                WidgetManager.Drawn["Progress"] = false;
                Context.Displayed = false;
            }
            while (true)
			{
				if (Game.IsControlKeyDownRightNow)
				{
					if (Game.IsKeyDown(Config.GetKey("toggleDisplay")) && WidgetManager.Drawn.ContainsKey("Track") && WidgetManager.Drawn.ContainsKey("Progress"))
					{
						WidgetManager.Drawn["Track"] = !WidgetManager.Drawn["Track"];
						WidgetManager.Drawn["Progress"] = !WidgetManager.Drawn["Progress"];
						Context.Displayed = !Context.Displayed;
					}
					if (Game.IsKeyDown(Config.GetKey("togglePlayback")))
					{
						if (Context.Playing)
						{
							Game.DisplaySubtitle("Playback paused");
#if DEFAULT
                            Context.Spotify.Pause();
#endif
						}
						else
                        {
#if DEFAULT
                            Context.Spotify.Play();
#endif
							Game.DisplaySubtitle("Playback resumed");
						}
					}
					if (Game.IsKeyDown(Config.GetKey("nextTrack")))
                    {
#if DEFAULT
                        Context.Spotify.Skip();
#endif
                        Game.DisplaySubtitle("Skipped track");
                    }
					if (Game.IsKeyDown(Config.GetKey("previousTrack")))
                    {
#if DEFAULT
						Context.Spotify.Previous();
#endif
                        Game.DisplaySubtitle("Previous track");
                    }
                    if (Game.IsKeyDownRightNow(Config.GetKey("volumeUp")))
                    {
#if DEFAULT
                        float num = Context.Spotify.GetSpotifyVolume();
						if ((double)num <= 99.0)
						{
							num += 1f;
							Context.Spotify.SetSpotifyVolume(num);
                            Game.DisplaySubtitle($"Set Volume to {num}");
                        }
#endif
					}
					if (Game.IsKeyDownRightNow(Config.GetKey("volumeDown")))
                    {
#if DEFAULT
                        float num2 = Context.Spotify.GetSpotifyVolume();
						if ((double)num2 >= 1.0)
						{
							num2 -= 1f;
							Context.Spotify.SetSpotifyVolume(num2);
                            Game.DisplaySubtitle($"Set Volume to {num2}");
                        }
#endif
                    }
				}
				GameFiber.Yield();
			}
		}
	}
}
