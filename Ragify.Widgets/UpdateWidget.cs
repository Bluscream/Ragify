using Rage;
using System;
using System.Drawing;

namespace Ragify.Widgets
{
	public class UpdateWidget : BaseWidget
	{
		public int LastCheck;

		public UpdateWidget(float x, float y, float width, float height) : base(x, y, width, height)
		{
		}

		public override void Think()
		{
			this.SetMappedPoint("Text", new PointF(this.GetMappedPoint("Base").X + 10f, this.GetMappedPoint("Base").Y + 5f));
		}

		public override void Draw(object sender, GraphicsEventArgs args)
		{
			if (!WidgetManager.Drawn["Update"])
			{
				return;
			}
			if (this.LastCheck == 0)
			{
				this.LastCheck = Utils.GetCurrentTimestamp() + 120;
			}
			if (this.LastCheck < Utils.GetCurrentTimestamp())
			{
				return;
			}
			base.Draw(sender, args);
			Rage.Graphics expr_44 = args.Graphics;
			expr_44.DrawRectangle(new RectangleF(this.GetMappedPoint("Base"), this.GetMappedSize("Base")), Color.FromArgb(200, 41, 128, 185));
			expr_44.DrawText("An update is available! V" + this.GetMappedString("Version"), "Arial", 17f, this.GetMappedPoint("Text"), Color.FromArgb(255, 255, 255));
		}
	}
}
