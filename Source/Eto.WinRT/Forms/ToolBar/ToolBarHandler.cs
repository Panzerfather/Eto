using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using swc = Windows.UI.Xaml.Controls;
using swi = Windows.UI.Xaml.Input;
using swm = Windows.UI.Xaml.Media;

namespace Eto.WinRT.Forms.ToolBar
{
	/// <summary>
	/// Control to hold a tool bar which can be displayed via <see cref="ToolBarView"/> control
	/// </summary>
	/// <copyright>(c) 2015 by Nicolas Pöhlmann</copyright>
	/// <license type="BSD-3">See LICENSE for full terms</license>
	public class ToolBarHandler : WidgetHandler<swc.CommandBar, Eto.Forms.ToolBar, Eto.Forms.ToolBar.ICallback>, Eto.Forms.ToolBar.IHandler
	{
		Size imageSize = new Size(16, 16);

		public ToolBarHandler()
		{
			this.Control = new swc.CommandBar
			{
				FontFamily = new swm.FontFamily("Segoe UI")
			};
		}

		public void AddItem(ToolItem item, int index)
		{
			Control.PrimaryCommands.Add((swc.AppBarButton)item.ControlObject);

			ItemsRescale();
		}

		public void Clear()
		{
			Control.PrimaryCommands.Clear();
		}

		public Size ImageScalingSize
		{
			get
			{
				return imageSize;
			}
			set
			{
				imageSize = value;
				ItemsRescale();
			}
		}

		public void ItemsRescale()
		{
			foreach (var item in Control.PrimaryCommands)
			{
				swc.StackPanel stackPanel = null;

				if (item is swc.AppBarButton)
				{
					stackPanel = ((swc.AppBarButton)item).Content as swc.StackPanel;
				}
				else if (item is swc.Primitives.ToggleButton)
				{
					stackPanel = ((swc.Primitives.ToggleButton)item).Content as swc.StackPanel;
				}

				if (stackPanel != null)
				{
					foreach (var image in stackPanel.Children.OfType<swc.Image>())
					{
						image.MaxHeight = imageSize.Height;
						image.MaxWidth = imageSize.Width;
						image.Height = imageSize.Height;
						image.Width = imageSize.Width;
					}
				}
			}
		}

		public void RemoveItem(ToolItem item)
		{
			Control.PrimaryCommands.Remove((swc.AppBarButton)item.ControlObject);
		}

		public ToolBarTextAlign TextAlign
		{
			get
			{
				return ToolBarTextAlign.Underneath;
			}
			set
			{
			}
		}
	}
}