using System;
using Eto.Drawing;
using Eto.Forms;
using sd = System.Drawing;
using swf = System.Windows.Forms;

namespace Eto.WinForms.Forms.ToolBar
{
	public class ToolBarViewHandler : WindowsControl<swf.ToolStrip, ToolBarView, ToolBarView.ICallback>, ToolBarView.IHandler
	{
		Eto.Forms.ToolBar content;
		DockPosition dock = DockPosition.None;
		static readonly object minimumSizeKey = new object();

		public ToolBarViewHandler()
		{
			this.Control = new swf.ToolStrip
			{
				AutoSize = true,
				Dock = swf.DockStyle.Fill,
				Font = sd.SystemFonts.DefaultFont,
				ForeColor = sd.SystemColors.ControlText
			};
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		public Control Content
		{
			get { return null; }
			set { }
		}

		public DockPosition Dock
		{
			get { return dock; }
			set
			{
				this.Control.Dock = (swf.DockStyle)Enum.Parse(typeof(swf.DockStyle), value.ToString());
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
					SetMinimumSize(useCache: true);
				}
			}
		}

		public virtual Padding Padding
		{
			get { return this.Control.Padding.ToEto(); }
			set { this.Control.Padding = value.ToSWF(); }
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
					swf.ToolStrip control = ((swf.ToolStrip)content.ControlObject);
					control.Dock = (swf.DockStyle)Enum.Parse(typeof(swf.DockStyle), dock.ToString());
					this.Control = control;
				}

				if (Widget.Loaded)
					ResumeLayout();
			}
		}
	}
}

