using SWF = System.Windows.Forms;
using Eto.Forms;
using System.Collections.Generic;

namespace Eto.WinForms.Forms.Menu
{
	/// <summary>
	/// Summary description for MenuBarHandler.
	/// </summary>
	public class MenuBarHandler : WidgetHandler<SWF.MenuStrip, MenuBar>, MenuBar.IHandler
	{
		public MenuBarHandler()
		{
			Control = new SWF.MenuStrip { Visible = false };
		}

		#region IMenu Members

		public void AddMenu(int index, MenuItem item)
		{
			Control.Items.Insert(index, (SWF.ToolStripItem)item.ControlObject);
			Control.Visible = true;
		}

		public void RemoveMenu(MenuItem item)
		{
			Control.Items.Remove((SWF.ToolStripItem)item.ControlObject);
			Control.Visible = Control.Items.Count > 0;
		}

		public void Clear()
		{
			Control.Items.Clear();
			Control.Visible = false;
		}

		#endregion

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
