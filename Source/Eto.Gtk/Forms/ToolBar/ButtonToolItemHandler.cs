using System;
using Eto.Forms;

namespace Eto.GtkSharp.Forms.ToolBar
{
	public class ButtonToolItemHandler : ToolItemHandler<Gtk.ToolButton, ButtonToolItem>, ButtonToolItem.IHandler
	{
		protected class ButtonToolItemConnector : WeakConnector
		{
			public new ButtonToolItemHandler Handler { get { return (ButtonToolItemHandler)base.Handler; } }

			public void HandleClicked(object sender, EventArgs e)
			{
				Handler.Widget.OnClick(e);
			}
		}

		protected new ButtonToolItemConnector Connector
		{
			get { return (ButtonToolItemConnector)base.Connector; }
		}

		protected override WeakConnector CreateConnector()
		{
			return new ButtonToolItemConnector();
		}

		public override void CreateControl(ToolBarHandler handler, int index)
		{
			Gtk.Toolbar toolbar = handler.Control;

			Control = new Gtk.ToolButton(GtkImage, Text)
			{
				CanFocus = false,
				IsImportant = true,
				Sensitive = Enabled,
				TooltipText = this.ToolTip
			};

			toolbar.Insert(Control, index);

			if (toolbar.Visible)
				Control.ShowAll();

			Control.Clicked += Connector.HandleClicked;
		}
	}
}
