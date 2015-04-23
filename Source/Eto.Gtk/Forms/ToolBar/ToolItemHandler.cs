using Eto.Forms;
using Eto.Drawing;

namespace Eto.GtkSharp.Forms.ToolBar
{
	public interface IToolBarItemHandler
	{
		void CreateControl(ToolBarHandler handler, int index);
	}

	public abstract class ToolItemHandler<TControl, TWidget> : WidgetHandler<TControl, TWidget>, ToolItem.IHandler, IToolBarItemHandler
		where TControl: Gtk.Widget
		where TWidget: ToolItem
	{
		bool enabled = true;
		Image image;
		Size imageSize = new Size(16, 16);

		protected Gtk.Image GtkImage { get; set; }

		public abstract void CreateControl(ToolBarHandler handler, int index);

		public virtual void CreateFromCommand(Command command)
		{
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				enabled = value;
				if (Control != null)
					Control.Sensitive = value;
			}
		}

		public Image Image
		{
			get { return image; }
			set
			{
				image = value;
				GtkImage = image.ToGtk(Gtk.IconSize.SmallToolbar);
			}
		}

		public Size ImageScalingSize
		{
			get
			{
				return imageSize;
			}
			set
			{
				imageSize = value;

				if (imageSize == new Size(16, 16))
				{
					GtkImage = image.ToGtk(Gtk.IconSize.SmallToolbar);
				}
				else
				{
					GtkImage = image.ToGtk(Gtk.IconSize.LargeToolbar);
				}
			}
		}

		public string Text { get; set; }

		public string ToolTip { get; set; }
	}
}
