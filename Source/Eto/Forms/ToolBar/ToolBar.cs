using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Eto.Drawing;

namespace Eto.Forms
{
	/// <summary>
	/// Text alignment hint for items in a <see cref="ToolBar"/>
	/// </summary>
	/// <remarks>
	/// Note that some platforms may define the visual style of toolbar items and this just serves as a hint for platforms
	/// that support such features (e.g. windows).
	/// </remarks>
	/// <copyright>(c) 2014 by Curtis Wensley</copyright>
	/// <license type="BSD-3">See LICENSE for full terms</license>
	public enum ToolBarTextAlign
	{
		/// <summary>
		/// Text will be shown to the right of the toolbar item, if available
		/// </summary>
		Right,
		/// <summary>
		/// Text will be shown below the toolbar items
		/// </summary>
		Underneath
	}

	/// <summary>
	/// Toolbar widget for use on a <see cref="ToolBarView"/>.
	/// </summary>
	/// <copyright>(c) 2014 by Curtis Wensley</copyright>
	/// <copyright>(c) 2015 by Nicolas Pöhlmann</copyright>
	/// <license type="BSD-3">See LICENSE for full terms</license>
	[ContentProperty("Items")]
	[Handler(typeof(ToolBar.IHandler))]
	public class ToolBar : Widget
	{
		internal new IHandler Handler { get { return (IHandler)base.Handler; } }

		ToolItemCollection items;

		/// <summary>
		/// Gets the collection of items in the toolbar.
		/// </summary>
		/// <value>The tool item collection.</value>
		public ToolItemCollection Items
		{
			get
			{
				return items ?? (items = new ToolItemCollection(this));
			}
		}

		/// <summary>
		/// Gets or sets the image scaling size.
		/// </summary>
		/// <value>The image scaling size.</value>
		public Size ImageScalingSize
		{
			get
			{
				return Handler.ImageScalingSize;
			}
			set
			{
				Handler.ImageScalingSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the text alignment hint.
		/// </summary>
		/// <remarks>
		/// Note that some platforms may define the visual style of toolbar items and this just serves as a hint for platforms
		/// that support such features (e.g. windows).
		/// </remarks>
		/// <value>The text alignment hint.</value>
		public ToolBarTextAlign TextAlign
		{
			get { return Handler.TextAlign; }
			set { Handler.TextAlign = value; }
		}

		/// <summary>
		/// Called when the tool item is loaded to be shown on the form.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal protected virtual void OnLoad(EventArgs e)
		{
			foreach (var item in Items)
				item.OnLoad(e);
		}

		/// <summary>
		/// Called when the tool item is removed from a form.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		internal protected virtual void OnUnLoad(EventArgs e)
		{
			foreach (var item in Items)
				item.OnUnLoad(e);
		}

		/// <summary>
		/// Handler interface for the <see cref="ToolBar"/>.
		/// </summary>
		public new interface IHandler : Widget.IHandler
		{
			/// <summary>
			/// Adds a tool item at the specified index.
			/// </summary>
			/// <param name="item">Item to add.</param>
			/// <param name="index">Index of the button to add.</param>
			void AddItem(ToolItem item, int index);

			/// <summary>
			/// Clears all buttons from the toolbar
			/// </summary>
			void Clear();

			/// <summary>
			/// Gets or sets the image scaling size for the toolbar.
			/// </summary>
			/// <value>The image scaling size.</value>
			Size ImageScalingSize { get; set; }

			/// <summary>
			/// Removes the specified tool item.
			/// </summary>
			/// <param name="item">Item to remove.</param>
			void RemoveItem(ToolItem item);

			/// <summary>
			/// Gets or sets the text alignment hint.
			/// </summary>
			/// <remarks>
			/// Note that some platforms may define the visual style of toolbar items and this just serves as a hint for platforms
			/// that support such features (e.g. windows).
			/// </remarks>
			/// <value>The text alignment hint.</value>
			ToolBarTextAlign TextAlign { get; set; }
		}
	}
}
