#define DEFAULT
using Rage;
#if DEFAULT
using SpotifyAPI.Local; //Base Namespace
using SpotifyAPI.Local.Enums; //Enums
using SpotifyAPI.Local.Models; //Models for the JSON-responses
#endif
using System;

namespace Ragify
{
	public static class Context
	{
#if DEFAULT
        public static SpotifyLocalAPI Spotify = null;

		public static Track CurrentTrack = null;
#endif
        public static bool Playing = false;

		public static double TrackTime = 0.0;

		public static float Volume = 0f;

		public static int HideVolume = 0;

		public static bool Displayed = false;

		public static void Initialize()
        {
#if DEFAULT
            Context.Spotify = new SpotifyLocalAPI();
            if (!SpotifyLocalAPI.IsSpotifyRunning())
            {
                Game.Console.Print("[Ragify] Spotify is not running!");
                Game.DisplaySubtitle("[Ragify] Spotify is not running!");
                return; //Make sure the spotify client is running
            }
            else
            {
                Game.Console.Print("[Ragify] Spotify is running.");
            }
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                Game.Console.Print("[Ragify] Spotify Web Helper is not running!");
                Game.DisplaySubtitle("[Ragify] Spotify Web Helper is not running!");
                return; //Make sure the WebHelper is running
            }
            else
            {
                Game.Console.Print("[Ragify] Spotify Webhelper is running.");
            }
            if (!Spotify.Connect())
            {
                Game.Console.Print("[Ragify] Could not connect to Spotify!");
                Game.DisplaySubtitle("[Ragify] Could not connect to Spotify!");
                return; //We need to call Connect before fetching infos, this will handle Auth stuff
            } else
            {
                Game.Console.Print("[Ragify] Connected to Spotify.");
            }
            Context.Spotify.ListenForEvents = true;
			if (Context.Spotify.GetStatus().Track != null)
			{
				Context.CurrentTrack = Context.Spotify.GetStatus().Track;
                Game.Console.Print($"[Ragify] Already playing \"{Context.CurrentTrack.TrackResource.Name}\" by \"{Context.CurrentTrack.ArtistResource.Name}\"");
            }
			Context.Playing = Context.Spotify.GetStatus().Playing;
			Context.Spotify.OnPlayStateChange += new EventHandler<PlayStateEventArgs>(Context.EventPlayStateChange);
			Context.Spotify.OnTrackChange += new EventHandler<TrackChangeEventArgs>(Context.EventTrackChange);
			Context.Spotify.OnTrackTimeChange += new EventHandler<TrackTimeChangeEventArgs>(Context.EventTrackTimeChange);
#endif
        }
#if DEFAULT
        private static void EventPlayStateChange(object sender, PlayStateEventArgs args)
		{
			Context.Playing = args.Playing;
		}

		private static void EventTrackChange(object sender, TrackChangeEventArgs args)
		{
			if (args.NewTrack.IsAd())
			{
				return;
			}
			Context.CurrentTrack = args.NewTrack;
			if (!Context.Displayed)
			{
                if(Config.initializationFile.ReadBoolean("General", "showSubtitles", true))
				    Game.DisplaySubtitle("~g~" + args.NewTrack.ArtistResource.Name + " - " + args.NewTrack.TrackResource.Name);
                if (Config.initializationFile.ReadBoolean("General", "showNotifications", false))
                    Game.DisplayNotification($"{args.NewTrack.ArtistResource.Name}\n{args.NewTrack.TrackResource.Name}");
                if (Config.initializationFile.ReadBoolean("General", "showHelpNotifications", false))
                    Game.DisplayHelp($"{args.NewTrack.ArtistResource.Name}\n{args.NewTrack.TrackResource.Name}");

            }
		}

		private static void EventTrackTimeChange(object sender, TrackTimeChangeEventArgs args)
		{
			Context.TrackTime = args.TrackTime;
		}
#endif
	}
}
