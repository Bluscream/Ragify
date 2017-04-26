#define DEFAULT
using Rage;
using System;
using System.Drawing;

namespace Ragify.Widgets
{
	public class ProgressWidget : BaseWidget
	{
		private float Progress;

		public ProgressWidget(float x, float y, float width, float height) : base(x, y, width, height)
		{
		}

		public override void Think()
		{
			this.Progress = (float)(Context.TrackTime / (double)this.GetMappedSize("Base").Width) * this.GetMappedSize("Base").Width;
			this.SetMappedSize("Progress", new SizeF(this.Progress, this.GetMappedSize("Base").Height));
		}

		public override void Draw(object sender, GraphicsEventArgs args)
		{
			if (!WidgetManager.Drawn["Progress"])
			{
				return;
			}
			base.Draw(sender, args);
			Rage.Graphics graphics = args.Graphics;
#if DEFAULT
            if (Context.CurrentTrack != null)
			{
				graphics.DrawRectangle(new RectangleF(this.GetMappedPoint("Base"), this.GetMappedSize("Progress")), Color.FromArgb(27, 216, 94));
			}
#endif
		}
	}
}
