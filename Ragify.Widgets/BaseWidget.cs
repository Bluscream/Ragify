using Rage;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Ragify.Widgets
{
	public class BaseWidget
	{
		private float ScreenWidth;

		private float ScreenHeight;

		private PointF Position;

		private SizeF Size;

		private Dictionary<string, PointF> LocalPoints;

		private Dictionary<string, SizeF> LocalSizes;

		private Dictionary<string, string> LocalStrings;

		public BaseWidget(float x, float y, float width, float height)
		{
			this.LocalPoints = new Dictionary<string, PointF>();
			this.LocalSizes = new Dictionary<string, SizeF>();
			this.LocalStrings = new Dictionary<string, string>();
			this.ScreenHeight = (float)Game.Resolution.Height;
			this.ScreenWidth = (float)Game.Resolution.Width;
			this.Position = new PointF(x, y);
			this.Size = new SizeF(width, height);
			this.SetMappedPoint("Base", this.Position);
			this.SetMappedSize("Base", this.Size);
		}

		public virtual void Initialize()
		{
		}

		public virtual void Think()
		{
		}

		public virtual void Draw(object sender, GraphicsEventArgs args)
		{
			this.Think();
		}

		public virtual void SetMappedPoint(string name, PointF point)
		{
			if (!this.LocalPoints.ContainsKey(name))
			{
				this.LocalPoints.Add(name, point);
				return;
			}
			this.LocalPoints[name] = point;
		}

		public virtual PointF GetMappedPoint(string name)
		{
			return this.LocalPoints[name];
		}

		public virtual void SetMappedSize(string name, SizeF point)
		{
			if (!this.LocalSizes.ContainsKey(name))
			{
				this.LocalSizes.Add(name, point);
				return;
			}
			this.LocalSizes[name] = point;
		}

		public virtual SizeF GetMappedSize(string name)
		{
			return this.LocalSizes[name];
		}

		public virtual void SetMappedString(string name, string content)
		{
			if (!this.LocalStrings.ContainsKey(name))
			{
				this.LocalStrings.Add(name, content);
				return;
			}
			this.LocalStrings[name] = content;
		}

		public virtual string GetMappedString(string name)
		{
			return this.LocalStrings[name];
		}

		public virtual void SetPositionFromBottomLeftCorner(PointF position)
		{
			this.SetMappedPoint("Base", new PointF(position.X, this.ScreenHeight - position.Y));
		}

		public virtual void SetPositionFromBottomRightCorner(PointF position)
		{
			this.SetMappedPoint("Base", new PointF(this.ScreenWidth - position.X, this.ScreenHeight - position.Y));
		}

		public virtual void SetPositionFromTopRightCorner(PointF position)
		{
			this.SetMappedPoint("Base", new PointF(this.ScreenWidth - position.X, position.Y));
		}

		public virtual void SetPositionFromTopLeftCorner(PointF position)
		{
			this.SetMappedPoint("Base", new PointF(position.X, position.Y));
		}
	}
}
