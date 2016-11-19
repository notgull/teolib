using System;
using System.Drawing;
using System.ComponentModel;

namespace teolib
{
	/// <summary>
	/// Represents areas of color in a TextLayer
	/// </summary>
	public class ColorMap //: System.Collections.Generic.IEnumerable<Color[]>
	{
		private ConsoleColor[][] fdata;

		/// <summary>
		/// Gets the color of the text.
		/// </summary>
		/// <value>The color of the text.</value>
		public ConsoleColor[][] TextColor { get { return fdata; } }

		private ConsoleColor[][] bdata;

		/// <summary>
		/// Gets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public ConsoleColor[][] BackgroundColor { get { return bdata; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="teolib.ColorMap"/> class.
		/// </summary>
		/// <param name="fdata">Front color data.</param>
		/// <param name="bdata">Back color data.</param>
		public ColorMap (ConsoleColor[][] fdata, ConsoleColor[][] bdata)
		{
			this.fdata = fdata;
			this.bdata = bdata;
		}


	}
}

