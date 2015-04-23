using System;
using Eto.Drawing;
using Eto.Forms;

namespace Eto.GtkSharp.Forms.ToolBar
{
	public class ToolBarHandler : GtkControl<Gtk.Toolbar, Eto.Forms.ToolBar, Eto.Forms.ToolBar.ICallback>, Eto.Forms.ToolBar.IHandler
	{
		#if GTK2
		Gtk.RadioButton radioGroup;
		public GLib.SList RadioGroup
		{
			get { return (radioGroup ?? (radioGroup = new Gtk.RadioButton("g"))).Group; }
		}
		#elif GTK3
		Gtk.RadioToolButton radioGroup;
		public Gtk.RadioToolButton RadioGroup
		{
			get { return (radioGroup ?? (radioGroup = new Gtk.RadioToolButton(new Gtk.RadioToolButton[0]))); }
		}
		#endif

		Size imageSize = new Size(16, 16);

		public ToolBarHandler()
		{
			Control = new Gtk.Toolbar();
			//Control.ShowArrow = false;
			//control.ToolbarStyle = Gtk.ToolbarStyle.Both;
		}
		
		public void AddItem(ToolItem item, int index)
		{
			((IToolBarItemHandler)item.Handler).CreateControl(this, index);
		}

		public void Clear()
		{
			foreach (Gtk.Widget w in Control.Children)
			{
				Control.Remove(w);
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
					Control.IconSize = Gtk.IconSize.SmallToolbar;
				}
				else
				{
					Control.IconSize = Gtk.IconSize.LargeToolbar;
				}
			}
		}

		public void RemoveItem(ToolItem item)
		{
			if (item.ControlObject != null)
				Control.Remove((Gtk.Widget)item.ControlObject);
		}

		public ToolBarTextAlign TextAlign
		{
			get
			{
				switch (Control.ToolbarStyle)
				{
					case Gtk.ToolbarStyle.BothHoriz:
						return ToolBarTextAlign.Right;
					default:
						return ToolBarTextAlign.Underneath;
				}
			}
			set
			{
				switch (value)
				{
					case ToolBarTextAlign.Right:
						Control.ToolbarStyle = Gtk.ToolbarStyle.BothHoriz;
						break;
					default:
						Control.ToolbarStyle = Gtk.ToolbarStyle.Both;
						break;
				}
			}
		}
	}
}
