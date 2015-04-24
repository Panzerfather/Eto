using System;
using Eto.Drawing;
using Eto.Forms;
using sw = System.Windows;
using swc = System.Windows.Controls;
using swm = System.Windows.Media;

namespace Eto.Wpf.Forms.ToolBar
{
	public class CheckToolItemHandler : ToolItemHandler<swc.Primitives.ToggleButton, CheckToolItem>, CheckToolItem.IHandler
	{
        Image image;
		Size imageSize = new Size(16, 16);
		readonly swc.Image swcImage;
		readonly swc.TextBlock label;

		public CheckToolItemHandler ()
		{
			Control = new swc.Primitives.ToggleButton
			{
				IsThreeState = false
			};
			swcImage = new swc.Image { MaxHeight = imageSize.Height, MaxWidth = imageSize.Width };
			label = new swc.TextBlock()
			{
				VerticalAlignment = sw.VerticalAlignment.Center
			};
			var panel = new swc.StackPanel { Orientation = swc.Orientation.Horizontal };
			panel.Children.Add (swcImage);
			panel.Children.Add (label);
			Control.Content = panel;

			Control.Checked += delegate {
				Widget.OnCheckedChanged (EventArgs.Empty);
			};
			Control.Unchecked += delegate {
				Widget.OnCheckedChanged (EventArgs.Empty);
			};
			Control.Click += delegate {
				Widget.OnClick (EventArgs.Empty);
			};
		}

		public bool Checked
		{
			get { return Control.IsChecked ?? false; }
			set { Control.IsChecked = value; }
		}

		public override bool Enabled
		{
			get { return Control.IsEnabled; }
			set
			{
				Control.IsEnabled = value;
				swcImage.IsEnabled = value;
				swcImage.Opacity = 0.5;
			}
		}

		public override Image Image
		{
			get { return image; }
			set
			{
				image = value;
				swcImage.Source = image.ToWpf(imageSize.Width); // at the moment only square sizes are available
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
				swcImage.Source = image.ToWpf(imageSize.Width); // at the moment only square sizes are available
			}
		}

		public override string Text
		{
			get { return label.Text.ToEtoMnemonic(); }
			set { label.Text = value.ToPlatformMnemonic(); }
		}

		public override string ToolTip
		{
			get { return Control.ToolTip as string; }
			set { Control.ToolTip = value; }
		}
	}
}
