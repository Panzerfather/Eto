using System;
using Eto.Drawing;
using Eto.Forms;

namespace Eto.Test.Commands
{
	public class About : Command
	{
		public About()
		{
			ID = "about";
			Image = TestIcons.TestIcon;
			MenuText = "About Test Application";
			ToolBarText = "About";
			Shortcut = Keys.F11;
			ImageScalingSize = new Size(32, 32);
		}

		protected override void OnExecuted(EventArgs e)
		{
			base.OnExecuted(e);
			// show the about dialog
			var about = new Dialogs.About();
			about.ShowModal(Application.Instance.MainForm);
		}
	}
}

