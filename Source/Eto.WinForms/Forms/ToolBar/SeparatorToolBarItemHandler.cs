using System;
using Eto.Drawing;
using Eto.Forms;
using sd = System.Drawing;
using swf = System.Windows.Forms;

namespace Eto.WinForms.Forms.ToolBar
{
	public class SeparatorToolBarItemHandler : ToolItemHandler<swf.ToolStripSeparator, SeparatorToolItem>, SeparatorToolItem.IHandler
	{
		public SeparatorToolBarItemHandler()
		{
			Control = new swf.ToolStripSeparator();
		}

		public override bool Enabled
		{
			get { return false; }
			set { throw new NotSupportedException(); }
		}

		public override Image Image
		{
			get { return null; }
			set { throw new NotSupportedException(); }
		}

		public override Size ImageScalingSize
		{
			get { return new Size(0, 0); }
			set { throw new NotSupportedException(); }
		}

		public override string Text
		{
			get { return string.Empty; }
			set { throw new NotSupportedException(); }
		}

		public override string ToolTip
		{
			get { return string.Empty; }
			set { throw new NotSupportedException(); }
		}

		public SeparatorToolItemType Type
		{
			get
			{
				return Control.AutoSize ? SeparatorToolItemType.Divider : SeparatorToolItemType.FlexibleSpace;
			}
			set
			{
				switch (value)
				{
					case SeparatorToolItemType.Divider:
						Control.AutoSize = true;
						break;
					default:
						Control.AutoSize = false;
						break;
				}
			}
		}
	}
}
