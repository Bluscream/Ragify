using Rage;
using Ragify.Widgets;
using System;
using System.Collections.Generic;

namespace Ragify
{
	public static class WidgetManager
	{
		public static Dictionary<string, BaseWidget> Registered;

		public static Dictionary<string, bool> Drawn;

		public static void Initialize()
		{
			WidgetManager.Registered = new Dictionary<string, BaseWidget>();
			WidgetManager.Drawn = new Dictionary<string, bool>();
		}

		public static void Register(string name, BaseWidget widget)
		{
			widget.Initialize();
			Game.FrameRender += new EventHandler<GraphicsEventArgs>(widget.Draw);
			WidgetManager.Drawn.Add(name, true);
			WidgetManager.Registered.Add(name, widget);
		}

		public static BaseWidget GetWidget(string name)
		{
			return WidgetManager.Registered[name];
		}

		public static void DisableWidget(string name)
		{
			WidgetManager.Drawn[name] = false;
		}

		public static void EnableWidget(string name)
		{
			WidgetManager.Drawn[name] = true;
		}
	}
}
