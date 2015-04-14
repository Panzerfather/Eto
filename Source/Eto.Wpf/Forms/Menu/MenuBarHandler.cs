using Eto.Forms;
using System.Collections.Generic;
using swc = System.Windows.Controls;

namespace Eto.Wpf.Forms.Menu
{
	public class MenuBarHandler : WidgetHandler<System.Windows.Controls.Menu, MenuBar>, MenuBar.IHandler
	{
		public MenuBarHandler ()
		{
			Control = new swc.Menu ();
		}

		public void AddMenu (int index, MenuItem item)
		{
			Control.Items.Insert(index, item.ControlObject);
		}

		public void RemoveMenu (MenuItem item)
		{
			Control.Items.Remove(item.ControlObject);
		}

		public void Clear ()
		{
			Control.Items.Clear ();
		}

		public void CreateSystemMenu()
		{
			// no system menu items
		}

		public void CreateLegacySystemMenu()
		{
			// no legacy system menu items
		}

		public IEnumerable<Command> GetSystemCommands()
		{
			yield break;
		}

		public ButtonMenuItem ApplicationMenu
		{
			get { return Widget.Items.GetSubmenu("&File", -100); }
		}

		public ButtonMenuItem HelpMenu
		{
			get { return Widget.Items.GetSubmenu("&Help", 1000); }
		}
	}
}
