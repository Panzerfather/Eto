using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using sw = System.Windows;
using swc = System.Windows.Controls;
using swi = System.Windows.Input;

namespace Eto.Wpf.Forms.ToolBar
{
	public class ToolBarHandler : WidgetHandler<swc.ToolBar, Eto.Forms.ToolBar, Eto.Forms.ToolBar.ICallback>, Eto.Forms.ToolBar.IHandler
	{
		Size imageSize = new Size(16, 16);

		public ToolBarHandler()
		{
			Control = new swc.ToolBar
			{
				IsTabStop = false,
				Tag = this
			};
			swi.KeyboardNavigation.SetTabNavigation(Control, swi.KeyboardNavigationMode.Continue);
		}

		public void AddItem(ToolItem Item, int index)
		{
			Control.Items.Insert(index, Item.ControlObject);

			ItemsRescale();
		}

		public void Clear()
		{
			Control.Items.Clear();
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
			foreach (var item in Control.Items)
			{
				swc.StackPanel stackPanel = null;

				if (item is swc.Button)
				{
					stackPanel = ((swc.Button)item).Content as swc.StackPanel;
				}
				else if  (item is swc.Primitives.ToggleButton)
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

		public void RemoveItem(ToolItem Item)
		{
			Control.Items.Remove(Item.ControlObject);
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
