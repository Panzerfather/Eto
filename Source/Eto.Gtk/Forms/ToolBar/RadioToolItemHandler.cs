using System;
using Eto.Forms;

namespace Eto.GtkSharp.Forms.ToolBar
{
	public class RadioToolItemHandler : ToolItemHandler<Gtk.RadioToolButton, RadioToolItem>, RadioToolItem.IHandler
	{
		protected class RadioToolItemConnector : WeakConnector
		{
			public new RadioToolItemHandler Handler { get { return (RadioToolItemHandler)base.Handler; } }

			public void HandleToggled(object sender, EventArgs e)
			{
				Handler.Widget.OnClick(EventArgs.Empty);
			}
		}

		protected new RadioToolItemConnector Connector { get { return (RadioToolItemConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new RadioToolItemConnector();
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

		public override void CreateControl(ToolBarHandler handler, int index)
		{
			Gtk.Toolbar tb = handler.Control;

			Control = new Gtk.RadioToolButton(handler.RadioGroup)
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
	}
}
