using System;
using Eto.Drawing;
using Eto.Forms;
using sw = Windows.UI.Xaml;
using swc = Windows.UI.Xaml.Controls;

namespace Eto.WinRT.Forms.ToolBar
{
	/// <summary>
	/// Control to display a tool bar containing a single <see cref="ToolBar"/> control
	/// </summary>
	/// <copyright>(c) 2015 by Nicolas Pöhlmann</copyright>
	/// <license type="BSD-3">See LICENSE for full terms</license>
	public class ToolBarViewHandler : WpfControl<swc.CommandBar, ToolBarView, ToolBarView.ICallback>, ToolBarView.IHandler
	{
        Eto.Forms.ToolBar content;
		ContextMenu contextMenu;
		DockPosition dock = DockPosition.None;
		static readonly object minimumSizeKey = new object();

		public ToolBarViewHandler()
		{
			Control = new swc.CommandBar();
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		public Size ClientSize
		{
			get
			{
#if TODO_XAML
				if (!Control.IsLoaded && clientSize != null)
					return clientSize.Value;
#endif
				return Control.GetSize();
			}
			set
			{
				Control.SetSize(value);
			}
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
#if TODO_XAML
				Control.ContextMenu = contextMenu != null ? ((ContextMenuHandler)contextMenu.Handler).Control : null;
#else
				throw new NotImplementedException();
#endif
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

		public Size MinimumSize
		{
			get { return Widget.Properties.Get<Size?>(minimumSizeKey) ?? Size.Empty; }
			set
			{
				if (value != MinimumSize)
				{
					Widget.Properties[minimumSizeKey] = value;
					Control.MinHeight = value.Height;
					Control.MinWidth = value.Width;
				}
			}
		}
		
		public virtual Padding Padding
		{
			get { return this.Control.Padding.ToEto(); }
			set { this.Control.Padding = value.ToWpf(); }
		}
		
		public bool RecurseToChildren
		{
			get { return true; }
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
                    this.Control = null;
                }

                content = value;

                if (content != null)
                {
                    swc.CommandBar control = (swc.CommandBar)content.ControlObject;
                    //control.Dock = (swf.DockStyle)Enum.Parse(typeof(swf.DockStyle), value.ToString());
                    this.Control = control;
                }

                if (Widget.Loaded)
                    ResumeLayout();
            }
        }
	}
}
