using System;
using Eto.Forms;

namespace Eto.GtkSharp.Forms.ToolBar
{
	public class CheckToolItemHandler : ToolItemHandler<Gtk.ToggleToolButton, CheckToolItem>, CheckToolItem.IHandler
	{
		protected class CheckToolItemConnector : WeakConnector
		{
			public new CheckToolItemHandler Handler { get { return (CheckToolItemHandler)base.Handler; } }

			public void HandleToggled(object sender, EventArgs e)
			{
				Handler.Widget.OnClick(EventArgs.Empty);
			}
		}

		protected new CheckToolItemConnector Connector { get { return (CheckToolItemConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new CheckToolItemConnector();
		}

		public override void CreateControl(ToolBarHandler handler, int index)
		{
			Gtk.Toolbar tb = handler.Control;

			Control = new Gtk.ToggleToolButton()
			{
				Active = false,
				CanFocus = false,
				IconWidget = GtkImage,
				IsImportant = true,
				Label = Text,
				Sensitive = Enabled,
				TooltipText = this.ToolTip
			};

			tb.Insert(Control, index);

			if (tb.Visible)
				Control.ShowAll();

			Control.Toggled += Connector.HandleToggled;
		}

		public bool Checked
		{
			get { return (Control != null) ? Control.Active : false; }
			set
			{
				if (Control != null)
					Control.Active = value;
			}
		}
	}
}
