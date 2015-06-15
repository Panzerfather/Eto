using System;
using Eto.Drawing;
using Eto.Forms;
using Eto.Wpf.Forms.Menu;
using sw = System.Windows;
using swc = System.Windows.Controls;
using swi = System.Windows.Input;

namespace Eto.Wpf.Forms.ToolBar
{
	/// <summary>
	/// Control to display a tool bar containing a single <see cref="ToolBar"/> control
	/// </summary>
	/// <copyright>(c) 2015 by Nicolas Pöhlmann</copyright>
	/// <license type="BSD-3">See LICENSE for full terms</license>
	public class ToolBarViewHandler : WpfContainer<swc.ToolBarTray, ToolBarView, ToolBarView.ICallback>, ToolBarView.IHandler
	{
		Eto.Forms.ToolBar content;
		ContextMenu contextMenu;
		DockPosition dock = DockPosition.None;
		Orientation orientation = Orientation.Horizontal;

		public ToolBarViewHandler()
		{
			Control  = new swc.ToolBarTray
			{
				Orientation = swc.Orientation.Horizontal
			};
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		public override Color BackgroundColor
		{
			get { return Control.Background.ToEtoColor(); }
			set { Control.Background = value.ToWpfBrush(Control.Background); }
		}

		public Control Content
		{
			get { return null; }
			set { }
		}

		public ContextMenu ContextMenu
		{
			get { return contextMenu; }
			set
			{
				contextMenu = value;
				
				Control.ContextMenu = contextMenu != null ? ((ContextMenuHandler)contextMenu.Handler).Control : null;
			}
		}

		public DockPosition Dock
		{
			get { return dock; }
			set
			{
				//this.Control.Dock = (swf.DockStyle)Enum.Parse(typeof(swf.DockStyle), value.ToString());
				dock = value;
			}
		}

		public Orientation Orientation
		{
			get { return orientation; }
			set
			{
				this.Control.Orientation = (swc.Orientation)Enum.Parse(typeof(swc.Orientation), value.ToString());
				orientation = value;
			}
		}

		public virtual Padding Padding
		{
			get
			{
				if (this.Control.ToolBars.Count > 0)
					return this.Control.ToolBars[0].Padding.ToEto();
				return new Padding();

			}
			set
			{
				if (this.Control.ToolBars.Count > 0)
					this.Control.ToolBars[0].Padding = value.ToWpf();
			}
		}

		public override void Remove(sw.FrameworkElement child)
		{
			if (Control.ToolBars.Contains((swc.ToolBar)child))
			{
				Control.ToolBars.Remove((swc.ToolBar)child);
			}
		}

		public Eto.Forms.ToolBar ToolBar
		{
			get { return content; }
			set
			{
				if (Widget.Loaded)
					SuspendLayout();

				if (content != null)
				{
					this.Control.ToolBars.Remove((swc.ToolBar)content.ControlObject);
				}

				content = value;

				if (content != null)
				{
					swc.ToolBar control = (swc.ToolBar)content.ControlObject;
					this.Control.ToolBars.Add((swc.ToolBar)content.ControlObject);
				}

				if (Widget.Loaded)
					ResumeLayout();
			}
		}
	}
}

