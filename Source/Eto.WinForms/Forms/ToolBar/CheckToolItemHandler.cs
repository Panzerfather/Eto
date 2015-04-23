using System;
using Eto.Drawing;
using Eto.Forms;
using sd = System.Drawing;
using swf = System.Windows.Forms;

namespace Eto.WinForms.Forms.ToolBar
{
	public class CheckToolItemHandler : ToolItemHandler<swf.ToolStripButton, CheckToolItem>, CheckToolItem.IHandler
	{
		Image image;
		Size imageSize = new Size(16, 16);

		public CheckToolItemHandler()
		{
			Control = new swf.ToolStripButton
			{
				Tag = this
			};
			Control.Click += control_Click;
		}

		void control_Click(object sender, EventArgs e)
		{
			Control.Checked = !Control.Checked;
			Widget.OnClick(EventArgs.Empty);
		}

		public bool Checked
		{
			get { return Control.Checked; }
			set { Control.Checked = value; }
		}

		public override bool Enabled
		{
			get { return Control.Enabled; }
			set { Control.Enabled = value; }
		}

		public override Image Image
		{
			get { return image; }
			set
			{
				image = value;
				Control.Image = image.ToSD(imageSize.Width); // at the moment only square sizes are available
			}
		}

		public override Size ImageScalingSize
		{
			get
			{
				return imageSize;
			}
			set
			{
				imageSize = value;

				if (image != null)
				{
					Control.Image = image.ToSD(imageSize.Width); // at the moment only square sizes are available
				}
			}
		}

		public override string Text
		{
			get { return Control.Text.ToEtoMnemonic(); }
			set { Control.Text = value.ToPlatformMnemonic(); }
		}

		public override string ToolTip
		{
			get { return Control.ToolTipText as string; }
			set { Control.ToolTipText = value; }
		}
	}
}
