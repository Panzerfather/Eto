using System;
using Eto.Drawing;
using Eto.Forms;
using sd = System.Drawing;
using swf = System.Windows.Forms;

namespace Eto.WinForms.Forms.ToolBar
{
	public class ToolBarHandler : WindowsControl<swf.ToolStrip, Eto.Forms.ToolBar, Eto.Forms.ToolBar.ICallback>, Eto.Forms.ToolBar.IHandler
	{
		public ToolBarHandler()
		{
			Control = new swf.ToolStrip()
			{
				AutoSize = true,
				LayoutStyle = swf.ToolStripLayoutStyle.StackWithOverflow
			};
		}

		protected override void Initialize()
		{
			base.Initialize();

			Control.ImageScalingSize = Widget.ImageScalingSize.ToSD();
		}

		public void AddItem(ToolItem item, int index)
		{
			Control.Items.Insert(index, (swf.ToolStripItem)item.ControlObject);
		}

		public void Clear()
		{
			Control.Items.Clear();
		}

		public Size ImageScalingSize
		{
			get
			{
				return Control.ImageScalingSize.ToEto();
			}
			set
			{
				Control.ImageScalingSize = value.ToSD();
			}
		}

		public void RemoveItem(ToolItem item)
		{
			Control.Items.Remove((swf.ToolStripItem)item.ControlObject);
		}

		public ToolBarTextAlign TextAlign
		{
			get
			{
				/*switch (control.TextAlign)
				{
					case swf.ToolBarTextAlign.Right:
						return ToolBarTextAlign.Right;
					default:
					case swf.ToolBarTextAlign.Underneath:
						return ToolBarTextAlign.Underneath;
				}
				 */
				return ToolBarTextAlign.Underneath;
			}
			set
			{
				switch (value)
				{
					case ToolBarTextAlign.Right:
						//control.TextAlign = swf.ToolBarTextAlign.Right;
						break;
					case ToolBarTextAlign.Underneath:
						//control.TextAlign = swf.ToolBarTextAlign.Underneath;
						break;
					default:
						throw new NotSupportedException();
				}
			}
		}
	}
}
