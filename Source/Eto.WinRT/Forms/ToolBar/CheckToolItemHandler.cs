using System;
using Eto.Drawing;
using Eto.Forms;
using sw = Windows.UI.Xaml;
using swc = Windows.UI.Xaml.Controls;
using swm = Windows.UI.Xaml.Media;

namespace Eto.WinRT.Forms.ToolBar
{
	public class CheckToolItemHandler : ToolItemHandler<swc.Primitives.ToggleButton, CheckToolItem>, CheckToolItem.IHandler
	{
		Image image;
		Size imageSize = new Size(16, 16);
		readonly swc.Image swcImage;
		readonly swc.TextBlock label;

		public CheckToolItemHandler ()
		{
			Control = new swc.Primitives.ToggleButton {
				IsThreeState = false
			};
			swcImage = new swc.Image { MaxHeight = imageSize.Height, MaxWidth = imageSize.Width };
			label = new swc.TextBlock()
			{
				VerticalAlignment = sw.VerticalAlignment.Center
			};
			label = new swc.TextBlock ();
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
			set { Control.IsEnabled = value; }
		}

		public override Image Image
		{
			get { return image; }
			set
			{
				image = value;
				swcImage.Source = image.ToWpf(imageSize.Width);
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
			get { return label.Text.ToEtoMneumonic(); }
			set { label.Text = value.ToWpfMneumonic(); }
		}

		public override string ToolTip
		{
#if TODO_XAML
			get { return Control.ToolTip as string; }
			set { Control.ToolTip = value; }
#else
			get; set;
#endif
		}
	}
}
