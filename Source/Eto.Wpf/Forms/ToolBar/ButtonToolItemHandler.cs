using System;
using Eto.Drawing;
using Eto.Forms;
using sw = System.Windows;
using swc = System.Windows.Controls;
using swm = System.Windows.Media;

namespace Eto.Wpf.Forms.ToolBar
{
	public class ButtonToolItemHandler : ToolItemHandler<swc.Button, ButtonToolItem>, ButtonToolItem.IHandler
	{
		Image image;
		Size imageSize = new Size(16, 16);
		readonly swc.StackPanel panel;
		readonly swc.Image swcImage;
		readonly swc.TextBlock label;

		public ButtonToolItemHandler ()
		{
			Control = new swc.Button();
			swcImage = new swc.Image { MaxHeight = imageSize.Height, MaxWidth = imageSize.Width };
			label = new swc.TextBlock()
			{
				VerticalAlignment = sw.VerticalAlignment.Center
			};
			panel = new swc.StackPanel { Orientation = swc.Orientation.Horizontal };
			panel.Children.Add(swcImage);
			panel.Children.Add(label);
			Control.Content = panel;
			Control.Click += delegate
			{
				Widget.OnClick(EventArgs.Empty);
			};
		}

		public override bool Enabled
		{
			get { return Control.IsEnabled; }
			set
			{
				Control.IsEnabled = value;
				swcImage.IsEnabled = value;
				swcImage.Opacity = (value == false ? 0.5 : 1);
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
				swcImage.MaxHeight = value.Height;
				swcImage.MaxWidth = value.Width;
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
