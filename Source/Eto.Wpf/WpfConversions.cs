using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using swi = System.Windows.Input;
using swm = System.Windows.Media;
using sw = System.Windows;
using sp = System.Printing;
using swc = System.Windows.Controls;
using swmi = System.Windows.Media.Imaging;
using swd = System.Windows.Documents;
using Eto.Wpf.Drawing;

namespace Eto.Wpf
{
	public static class WpfConversions
	{
		public const float WheelDelta = 120f;

		public static readonly sw.Size PositiveInfinitySize = new sw.Size(double.PositiveInfinity, double.PositiveInfinity);
		public static readonly sw.Size ZeroSize = new sw.Size(0, 0);
		
		public static swm.Color ToWpf(this Color value)
		{

			return swm.Color.FromArgb((byte)(value.A * byte.MaxValue), (byte)(value.R * byte.MaxValue), (byte)(value.G * byte.MaxValue), (byte)(value.B * byte.MaxValue));
		}

		public static swm.Brush ToWpfBrush(this Color value, swm.Brush brush = null)
		{
			var solidBrush = brush as swm.SolidColorBrush;
			if (solidBrush == null || solidBrush.IsSealed)
			{
				solidBrush = new swm.SolidColorBrush();
			}
			solidBrush.Color = value.ToWpf();
			return solidBrush;
		}

		public static Color ToEto(this swm.Color value)
		{
			return new Color { A = value.A / 255f, R = value.R / 255f, G = value.G / 255f, B = value.B / 255f };
		}

		public static Color ToEtoColor(this swm.Brush brush)
		{
			var solidBrush = brush as swm.SolidColorBrush;
			if (solidBrush != null)
				return solidBrush.Color.ToEto();
			return Colors.Transparent;
		}

		public static Padding ToEto(this sw.Thickness value)
		{
			return new Padding((int)value.Left, (int)value.Top, (int)value.Right, (int)value.Bottom);
		}

		public static sw.Thickness ToWpf(this Padding value)
		{
			return new sw.Thickness(value.Left, value.Top, value.Right, value.Bottom);
		}

		public static Rectangle ToEto(this sw.Rect value)
		{
			if (value.IsEmpty)
				return Rectangle.Empty;
			return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
		}

		public static RectangleF ToEtoF(this sw.Rect value)
		{
			if (value.IsEmpty)
				return RectangleF.Empty;
			return new RectangleF((float)value.X, (float)value.Y, (float)value.Width, (float)value.Height);
		}

		public static sw.Rect ToWpf(this Rectangle value)
		{
			return new sw.Rect(value.X, value.Y, value.Width, value.Height);
		}

		public static sw.Int32Rect ToWpfInt32(this Rectangle value)
		{
			return new sw.Int32Rect(value.X, value.Y, value.Width, value.Height);
		}

		public static sw.Rect ToWpf(this RectangleF value)
		{
			return new sw.Rect(value.X, value.Y, value.Width, value.Height);
		}

		public static SizeF ToEto(this sw.Size value)
		{
			return new SizeF((float)value.Width, (float)value.Height);
		}

		public static Size ToEtoSize(this sw.Size value)
		{
			return new Size((int)(double.IsNaN(value.Width) ? -1 : value.Width), (int)(double.IsNaN(value.Height) ? -1 : value.Height));
		}

		public static sw.Size ToWpf(this Size value)
		{
			return new sw.Size(value.Width == -1 ? double.NaN : value.Width, value.Height == -1 ? double.NaN : value.Height);
		}

		public static sw.Size ToWpf(this SizeF value)
		{
			return new sw.Size(value.Width, value.Height);
		}

		public static PointF ToEto(this sw.Point value)
		{
			return new PointF((float)value.X, (float)value.Y);
		}

		public static Point ToEtoPoint(this sw.Point value)
		{
			return new Point((int)value.X, (int)value.Y);
		}

		public static sw.Point ToWpf(this Point value)
		{
			return new sw.Point(value.X, value.Y);
		}

		public static sw.Point ToWpf(this PointF value)
		{
			return new sw.Point(value.X, value.Y);
		}

		public static KeyEventArgs ToEto(this swi.KeyEventArgs e, KeyEventType keyType)
		{
			var key = KeyMap.Convert(e.Key, swi.Keyboard.Modifiers);
			return new KeyEventArgs(key, keyType) { Handled = e.Handled };
		}

		public static MouseEventArgs ToEto(this swi.MouseButtonEventArgs e, sw.IInputElement control, swi.MouseButtonState buttonState = swi.MouseButtonState.Pressed)
		{
			var buttons = MouseButtons.None;
			if (e.ChangedButton == swi.MouseButton.Left && e.LeftButton == buttonState)
				buttons |= MouseButtons.Primary;
			if (e.ChangedButton == swi.MouseButton.Right && e.RightButton == buttonState)
				buttons |= MouseButtons.Alternate;
			if (e.ChangedButton == swi.MouseButton.Middle && e.MiddleButton == buttonState)
				buttons |= MouseButtons.Middle;
			var modifiers = KeyMap.Convert(swi.Key.None, swi.Keyboard.Modifiers);
			var location = e.GetPosition(control).ToEto();

			return new MouseEventArgs(buttons, modifiers, location);
		}

		public static MouseEventArgs ToEto(this swi.MouseEventArgs e, sw.IInputElement control, swi.MouseButtonState buttonState = swi.MouseButtonState.Pressed)
		{
			var buttons = MouseButtons.None;
			if (e.LeftButton == buttonState)
				buttons |= MouseButtons.Primary;
			if (e.RightButton == buttonState)
				buttons |= MouseButtons.Alternate;
			if (e.MiddleButton == buttonState)
				buttons |= MouseButtons.Middle;
			var modifiers = KeyMap.Convert(swi.Key.None, swi.Keyboard.Modifiers);
			var location = e.GetPosition(control).ToEto();

			return new MouseEventArgs(buttons, modifiers, location);
		}

		public static MouseEventArgs ToEto(this swi.MouseWheelEventArgs e, sw.IInputElement control, swi.MouseButtonState buttonState = swi.MouseButtonState.Pressed)
		{
			var buttons = MouseButtons.None;
			if (e.LeftButton == buttonState)
				buttons |= MouseButtons.Primary;
			if (e.RightButton == buttonState)
				buttons |= MouseButtons.Alternate;
			if (e.MiddleButton == buttonState)
				buttons |= MouseButtons.Middle;
			var modifiers = KeyMap.Convert(swi.Key.None, swi.Keyboard.Modifiers);
			var location = e.GetPosition(control).ToEto();
			var delta = new SizeF(0, (float)e.Delta / WheelDelta);

			return new MouseEventArgs(buttons, modifiers, location, delta);
		}

		public static swm.BitmapScalingMode ToWpf(this ImageInterpolation value)
		{
			switch (value)
			{
				case ImageInterpolation.Default:
					return swm.BitmapScalingMode.Unspecified;
				case ImageInterpolation.None:
					return swm.BitmapScalingMode.NearestNeighbor;
				case ImageInterpolation.Low:
					return swm.BitmapScalingMode.LowQuality;
				case ImageInterpolation.Medium:
					return swm.BitmapScalingMode.HighQuality;
				case ImageInterpolation.High:
					return swm.BitmapScalingMode.HighQuality;
				default:
					throw new NotSupportedException();
			}
		}

		public static ImageInterpolation ToEto(this swm.BitmapScalingMode value)
		{
			switch (value)
			{
				case swm.BitmapScalingMode.HighQuality:
					return ImageInterpolation.High;
				case swm.BitmapScalingMode.LowQuality:
					return ImageInterpolation.Low;
				case swm.BitmapScalingMode.NearestNeighbor:
					return ImageInterpolation.None;
				case swm.BitmapScalingMode.Unspecified:
					return ImageInterpolation.Default;
				default:
					throw new NotSupportedException();
			}
		}

		public static sp.PageOrientation ToSP(this PageOrientation value)
		{
			switch (value)
			{
				case PageOrientation.Portrait:
					return sp.PageOrientation.Portrait;
				case PageOrientation.Landscape:
					return sp.PageOrientation.Landscape;
				default:
					throw new NotSupportedException();
			}
		}

		public static PageOrientation ToEto(this sp.PageOrientation? value)
		{
			if (value == null)
				return PageOrientation.Portrait;
			switch (value.Value)
			{
				case sp.PageOrientation.Landscape:
					return PageOrientation.Landscape;
				case sp.PageOrientation.Portrait:
					return PageOrientation.Portrait;
				default:
					throw new NotSupportedException();
			}
		}

		public static swc.PageRange ToPageRange(this Range<int> range)
		{
			return new swc.PageRange(range.Start, range.End);
		}

		public static Range<int> ToEto(this swc.PageRange range)
		{
			return new Range<int>(range.PageFrom, range.PageTo);
		}

		public static swc.PageRangeSelection ToSWC(this PrintSelection value)
		{
			switch (value)
			{
				case PrintSelection.AllPages:
					return swc.PageRangeSelection.AllPages;
				case PrintSelection.SelectedPages:
					return swc.PageRangeSelection.UserPages;
				default:
					throw new NotSupportedException();
			}
		}

		public static PrintSelection ToEto(this swc.PageRangeSelection value)
		{
			switch (value)
			{
				case swc.PageRangeSelection.AllPages:
					return PrintSelection.AllPages;
				case swc.PageRangeSelection.UserPages:
					return PrintSelection.SelectedPages;
				default:
					throw new NotSupportedException();
			}
		}

		public static Size GetSize(this sw.FrameworkElement element)
		{
			if (!double.IsNaN(element.ActualWidth) && !double.IsNaN(element.ActualHeight))
				return new Size((int)element.ActualWidth, (int)element.ActualHeight);
			return new Size((int)(double.IsNaN(element.Width) ? -1 : element.Width), (int)(double.IsNaN(element.Height) ? -1 : element.Height));
		}

		public static void SetSize(this sw.FrameworkElement element, Size size)
		{
			element.Width = size.Width == -1 ? double.NaN : size.Width;
			element.Height = size.Height == -1 ? double.NaN : size.Height;
		}

		public static void SetSize(this sw.FrameworkElement element, sw.Size size)
		{
			element.Width = size.Width;
			element.Height = size.Height;
		}

		public static FontStyle Convert(sw.FontStyle fontStyle, sw.FontWeight fontWeight)
		{
			var style = FontStyle.None;
			if (fontStyle == sw.FontStyles.Italic)
				style |= FontStyle.Italic;
			if (fontStyle == sw.FontStyles.Oblique)
				style |= FontStyle.Italic;
			if (fontWeight == sw.FontWeights.Bold)
				style |= FontStyle.Bold;
			return style;
		}

		public static FontDecoration Convert(sw.TextDecorationCollection decorations)
		{
			var decoration = FontDecoration.None;
			if (decorations != null)
			{
				if (sw.TextDecorations.Underline.All(decorations.Contains))
					decoration |= FontDecoration.Underline;
				if (sw.TextDecorations.Strikethrough.All(decorations.Contains))
					decoration |= FontDecoration.Strikethrough;
			}
			return decoration;
		}

		public static Bitmap ToEto(this swmi.BitmapSource bitmap)
		{
			return new Bitmap(new BitmapHandler(bitmap));
		}

		public static swmi.BitmapSource ToWpf(this Image image, int? size = null)
		{
			if (image == null)
				return null;
			var imageHandler = image.Handler as IWpfImage;
			if (imageHandler != null)
				return imageHandler.GetImageClosestToSize(size);
			return image.ControlObject as swmi.BitmapSource;
		}

		public static swc.Image ToWpfImage(this Image image, int? size = null)
		{
			var source = image.ToWpf(size);
			if (source == null)
				return null;
			var swcImage = new swc.Image { Source = source };
			if (size != null)
			{
				swcImage.MaxWidth = size.Value;
				swcImage.MaxHeight = size.Value;
			}
			return swcImage;
		}

		public static swm.Pen ToWpf(this Pen pen, bool clone = false)
		{
			var p = (swm.Pen)pen.ControlObject;
			if (clone)
				p = p.Clone();
			return p;
		}

		public static swm.PenLineJoin ToWpf(this PenLineJoin value)
		{
			switch (value)
			{
				case PenLineJoin.Miter:
					return swm.PenLineJoin.Miter;
				case PenLineJoin.Bevel:
					return swm.PenLineJoin.Bevel;
				case PenLineJoin.Round:
					return swm.PenLineJoin.Round;
				default:
					throw new NotSupportedException();
			}
		}

		public static PenLineJoin ToEto(this swm.PenLineJoin value)
		{
			switch (value)
			{
				case swm.PenLineJoin.Bevel:
					return PenLineJoin.Bevel;
				case swm.PenLineJoin.Miter:
					return PenLineJoin.Miter;
				case swm.PenLineJoin.Round:
					return PenLineJoin.Round;
				default:
					throw new NotSupportedException();
			}
		}

		public static swm.PenLineCap ToWpf(this PenLineCap value)
		{
			switch (value)
			{
				case PenLineCap.Butt:
					return swm.PenLineCap.Flat;
				case PenLineCap.Round:
					return swm.PenLineCap.Round;
				case PenLineCap.Square:
					return swm.PenLineCap.Square;
				default:
					throw new NotSupportedException();
			}
		}

		public static PenLineCap ToEto(this swm.PenLineCap value)
		{
			switch (value)
			{
				case swm.PenLineCap.Flat:
					return PenLineCap.Butt;
				case swm.PenLineCap.Round:
					return PenLineCap.Round;
				case swm.PenLineCap.Square:
					return PenLineCap.Square;
				default:
					throw new NotSupportedException();
			}
		}

		public static swm.Brush ToWpf(this Brush brush, bool clone = false)
		{
			var b = (swm.Brush)brush.ControlObject;
			if (clone)
				b = b.Clone();
			return b;
		}

		public static swm.Matrix ToWpf(this IMatrix matrix)
		{
			return (swm.Matrix)matrix.ControlObject;
		}

		public static swm.Transform ToWpfTransform(this IMatrix matrix)
		{
			return new swm.MatrixTransform(matrix.ToWpf());
		}

		public static IMatrix ToEtoMatrix(this swm.Transform transform)
		{
			return new MatrixHandler(transform.Value);
		}

		public static swm.PathGeometry ToWpf(this IGraphicsPath path)
		{
			return (swm.PathGeometry)path.ControlObject;
		}

		public static swm.GradientSpreadMethod ToWpf(this GradientWrapMode wrap)
		{
			switch (wrap)
			{
				case GradientWrapMode.Reflect:
					return swm.GradientSpreadMethod.Reflect;
				case GradientWrapMode.Repeat:
					return swm.GradientSpreadMethod.Repeat;
				default:
					throw new NotSupportedException();
			}
		}

		public static GradientWrapMode ToEto(this swm.GradientSpreadMethod spread)
		{
			switch (spread)
			{
				case swm.GradientSpreadMethod.Reflect:
					return GradientWrapMode.Reflect;
				case swm.GradientSpreadMethod.Repeat:
					return GradientWrapMode.Repeat;
				default:
					throw new NotSupportedException();
			}
		}

		public static WindowStyle ToEto(this sw.WindowStyle style)
		{
			switch (style)
			{
				case sw.WindowStyle.None:
					return WindowStyle.None;
				case sw.WindowStyle.ThreeDBorderWindow:
					return WindowStyle.Default;
				default:
					throw new NotSupportedException();
			}
		}

		public static sw.WindowStyle ToWpf(this WindowStyle style)
		{
			switch (style)
			{
				case WindowStyle.None:
					return sw.WindowStyle.None;
				case WindowStyle.Default:
					return sw.WindowStyle.ThreeDBorderWindow;
				default:
					throw new NotSupportedException();
			}
		}

		public static CalendarMode ToEto(this swc.CalendarSelectionMode mode)
		{
			switch (mode)
			{
				case System.Windows.Controls.CalendarSelectionMode.SingleDate:
					return CalendarMode.Single;
				case System.Windows.Controls.CalendarSelectionMode.SingleRange:
					return CalendarMode.Range;
				case System.Windows.Controls.CalendarSelectionMode.MultipleRange:
				case System.Windows.Controls.CalendarSelectionMode.None:
				default:
					throw new NotSupportedException();
			}
		}

		public static swc.CalendarSelectionMode ToWpf(this CalendarMode mode)
		{
			switch (mode)
			{
				case CalendarMode.Single:
					return swc.CalendarSelectionMode.SingleDate;
				case CalendarMode.Range:
					return swc.CalendarSelectionMode.SingleRange;
				default:
					throw new NotSupportedException();
			}
		}

		public static TextAlignment ToEto(this sw.HorizontalAlignment align)
		{
			switch (align)
			{
				case sw.HorizontalAlignment.Left:
					return TextAlignment.Left;
				case sw.HorizontalAlignment.Right:
					return TextAlignment.Right;
				case sw.HorizontalAlignment.Center:
					return TextAlignment.Center;
				default:
					throw new NotSupportedException();
			}
		}

		public static sw.HorizontalAlignment ToWpf(this TextAlignment align)
		{
			switch (align)
			{
				case TextAlignment.Center:
					return sw.HorizontalAlignment.Center;
				case TextAlignment.Left:
					return sw.HorizontalAlignment.Left;
				case TextAlignment.Right:
					return sw.HorizontalAlignment.Right;
				default:
					throw new NotSupportedException();
			}
		}

		public static sw.TextAlignment ToWpfTextAlignment(this TextAlignment align)
		{
			switch (align)
			{
				case TextAlignment.Center:
					return sw.TextAlignment.Center;
				case TextAlignment.Left:
					return sw.TextAlignment.Left;
				case TextAlignment.Right:
					return sw.TextAlignment.Right;
				default:
					throw new NotSupportedException();
			}
		}

		public static VerticalAlignment ToEto(this sw.VerticalAlignment align)
		{
			switch (align)
			{
				case sw.VerticalAlignment.Top:
					return VerticalAlignment.Top;
				case sw.VerticalAlignment.Bottom:
					return VerticalAlignment.Bottom;
				case sw.VerticalAlignment.Center:
					return VerticalAlignment.Center;
				case sw.VerticalAlignment.Stretch:
					return VerticalAlignment.Stretch;
				default:
					throw new NotSupportedException();
			}
		}

		public static sw.VerticalAlignment ToWpf(this VerticalAlignment align)
		{
			switch (align)
			{
				case VerticalAlignment.Top:
					return sw.VerticalAlignment.Top;
				case VerticalAlignment.Bottom:
					return sw.VerticalAlignment.Bottom;
				case VerticalAlignment.Center:
					return sw.VerticalAlignment.Center;
				case VerticalAlignment.Stretch:
					return sw.VerticalAlignment.Stretch;
				default:
					throw new NotSupportedException();
			}
		}

		public static Font SetEtoFont(this swc.Control control, Font font, Action<sw.TextDecorationCollection> setDecorations = null)
		{
			if (control == null) return font;
			if (font != null)
			{
				((FontHandler)font.Handler).Apply(control, setDecorations);
			}
			else
			{
				control.SetValue(swc.Control.FontFamilyProperty, swc.Control.FontFamilyProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontStyleProperty, swc.Control.FontStyleProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontWeightProperty, swc.Control.FontWeightProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontSizeProperty, swc.Control.FontSizeProperty.DefaultMetadata.DefaultValue);
			}
			return font;
		}

		public static Font SetEtoFont(this swd.TextRange control, Font font)
		{
			if (control == null) return font;
			if (font != null)
			{
				((FontHandler)font.Handler).Apply(control);
			}
			else
			{
				control.ApplyPropertyValue(swd.TextElement.FontFamilyProperty, swc.Control.FontFamilyProperty.DefaultMetadata.DefaultValue);
				control.ApplyPropertyValue(swd.TextElement.FontStyleProperty, swc.Control.FontStyleProperty.DefaultMetadata.DefaultValue);
				control.ApplyPropertyValue(swd.TextElement.FontWeightProperty, swc.Control.FontWeightProperty.DefaultMetadata.DefaultValue);
				control.ApplyPropertyValue(swd.TextElement.FontSizeProperty, swc.Control.FontSizeProperty.DefaultMetadata.DefaultValue);
				control.ApplyPropertyValue(swd.Inline.TextDecorationsProperty, new sw.TextDecorationCollection());
			}
			return font;
		}

		public static FontFamily SetEtoFamily(this swd.TextRange control, FontFamily fontFamily)
		{
			if (control == null) return fontFamily;
			if (fontFamily != null)
			{
				((FontFamilyHandler)fontFamily.Handler).Apply(control);
			}
			else
			{
				control.ApplyPropertyValue(swd.TextElement.FontFamilyProperty, swc.Control.FontFamilyProperty.DefaultMetadata.DefaultValue);
			}
			return fontFamily;
		}

		public static Font SetEtoFont(this swc.TextBlock control, Font font, Action<sw.TextDecorationCollection> setDecorations = null)
		{
			if (control == null) return font;
			if (font != null)
			{
				((FontHandler)font.Handler).Apply(control, setDecorations);
			}
			else
			{
				control.SetValue(swc.Control.FontFamilyProperty, swc.Control.FontFamilyProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontStyleProperty, swc.Control.FontStyleProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontWeightProperty, swc.Control.FontWeightProperty.DefaultMetadata.DefaultValue);
				control.SetValue(swc.Control.FontSizeProperty, swc.Control.FontSizeProperty.DefaultMetadata.DefaultValue);
			}
			return font;
		}

	}
}
