#define DEFAULT
using Rage;
using System;
using System.Drawing;

namespace Ragify.Widgets
{
	public class TrackWidget : BaseWidget
	{
		private string Name;

		private string Artist;

		public TrackWidget(float x, float y, float width, float height) : base(x, y, width, height)
		{
		}

		public override void Initialize()
		{
			this.Name = "";
			this.Artist = "";
		}

		public override void Think()
		{
			this.SetMappedPoint("Name", new PointF(this.GetMappedPoint("Base").X + 10f, this.GetMappedPoint("Base").Y + 10f));
			this.SetMappedPoint("Artist", new PointF(this.GetMappedPoint("Base").X + 10f, this.GetMappedPoint("Base").Y + 30f));
#if DEFAULT
            if (Context.CurrentTrack != null)
			{
				this.Name = Context.CurrentTrack.TrackResource.Name;
				this.Artist = Context.CurrentTrack.ArtistResource.Name;
			}
#endif
		}

		public override void Draw(object sender, GraphicsEventArgs args)
		{
			if (!WidgetManager.Drawn["Track"])
			{
				return;
			}
			base.Draw(sender, args);
			Rage.Graphics expr_20 = args.Graphics;
			expr_20.DrawRectangle(new RectangleF(this.GetMappedPoint("Base"), this.GetMappedSize("Base")), Color.FromArgb(200, 40, 40, 40));
			expr_20.DrawText(this.Name, "Arial", 17f, this.GetMappedPoint("Name"), Color.FromArgb(255, 255, 255));
			expr_20.DrawText(this.Artist, "Arial", 12f, this.GetMappedPoint("Artist"), Color.FromArgb(255, 255, 255));
		}
	}
}
