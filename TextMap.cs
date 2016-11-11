/*
 * Copyright J.T. Nunley 2016
 * This file is part of teolib.

    teolib is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    teolib is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with teolib.  If not, see <http://www.gnu.org/licenses/>. 
 * 
*/
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;

namespace teolib
{
	/// <summary>
	/// This represents a simple 2-dimensional block of text. They can be applied to a TextLayer.
	/// </summary>
	public class TextMap : Component, IEnumerable<char[]>, ICloneable {
		/// <summary>
		/// Stores the data used in the text map
		/// </summary>
		private char[][] data;

		/// <summary>
		/// Initializes a new TextMap with an empty char array of the specified width and height
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public TextMap(int width, int height) {
			this.data = Teolib.GetEmptyCharArray (width, height);
		}

		/// <summary>
		/// Initializes a new TextMap with a prespecified set of data.
		/// </summary>
		/// <param name="data">Data.</param>
		public TextMap(char[][] data) {
			this.data = data;
		}

		/// <summary>
		/// Gets the data that is used in the TextMap
		/// </summary>
		/// <value>Returns the data that is used in the TextMap</value>
		public char[][] Data {
			get { return data; }
			protected set { data = value; }
		}

		/// <summary>
		/// Gets the eneumerator for the TextMap
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<char[]> GetEnumerator() {
			return data.ToList ().GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ((data.ToList ()) as IEnumerable).GetEnumerator ();
		}

		/// <summary>
		/// Gets the size of the TextMap
		/// </summary>
		/// <value>The size of the TextMap</value>
		public Size Size {
			get { int width = data.Length;
				int height = data [0].Length;
				return new Size (width, height); }
		}

		/// <summary>
		/// Gets the width of the TextMap
		/// </summary>
		/// <value>The width of the TextMap</value>
		public int Width { get { return Size.Width; } }

		/// <summary>
		/// Gets the height of the TextMap
		/// </summary>
		/// <value>The height of the TextMap</value>
		public int Height { get { return Size.Height; } }
			
		[Obsolete("Use Resize instead of this")]
		public TextMap Clip(int width, int height) {
			if (new Size (width, height) == Size)
				return this;

			return InternalClip (width, height);
		}

		// used to clip stuff
		private TextMap InternalClip(int width, int height) {
			if (new Size (width, height) == Size)
				return this;
			
			char[][] clippedMap = new char[width][];
			for (int i = 0; i < width; i++) {
				clippedMap [i] = new char[height];
				for (int j = 0; j < height; j++)
					clippedMap [i] [j] = data [i] [j];
			}

			return new TextMap (clippedMap);
		}

		[Obsolete("Use Resize instead of this")]
		public TextMap Clip(Size s) {
			return Clip (s.Width, s.Height);
		}

		[Obsolete("Use brackets instead of this")]
		public char GetcharAt(int x, int y) {
			return Data [x] [y];
		}

		[Obsolete("Use brackets instead of this")]
		public void ChangecharAt(int x, int y, char value) {
			data [x] [y] = value;
		}

		/// <summary>
		/// Gets or sets the row at the specified index.
		/// </summary>
		/// <param name="index">Index</param>
		public char[] this[int index] {
			get {
				return Data [index];
			}
			set {
				data [index] = value;
			}
		}


		// internal function used to add white space to a Text Map
		private TextMap ExpandWithWhiteSpace(int width, int height) {
			char[][] expandedMap = new char[width][];
			for (int i = 0; i < width; i++)
				expandedMap [i] = new char[height];

			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					if (i >= Width - 1 && j >= Height - 1)
						expandedMap [i] [j] = Data [i] [j];
					else
						expandedMap [i] [j] = ' ';
			    }
			}

			return new TextMap (expandedMap);
		}

		public TextMap Clone() {
			return internalClone();
		}

		object ICloneable.Clone() {
			return internalClone ();
		}

		protected virtual TextMap internalClone() {
			TextMap tm = new TextMap (Width, Height);
			for (int i = 0; i < Width; i++)
				for (int j = 0; j < Height; j++)
					tm.data [i] [j] = data [i] [j];
			return tm;
		}

		/// <summary>
		/// Resizes the TextMap to a specified size and returns it
		/// </summary>
		/// <param name="width">Width to resize to</param>
		/// <param name="height">Height to resize to</param>
		public TextMap Resize(int width, int height) {
			// first, if both dimensions are smaller than the current ones, return a clipped version
			if (width <= Width && height <= Height)
				return InternalClip (width, height);

			// if both dimensions are bigger, return an expanded version
			if (width > Width && height > Height)
				return ExpandWithWhiteSpace (width, height);

			// if the width is bigger but the height is smaller, expand it, then clip it
			if (width > Width && height <= Height) {
				TextMap xMap = this.ExpandWithWhiteSpace (Width, height);
				return xMap.InternalClip (width, height);
			}

			// if the width is smaller but the height it bigger, do the above
			if (width <= Width && height > Height) {
				TextMap xMap = this.ExpandWithWhiteSpace (width, Height);
				return xMap.InternalClip (width, height);
			}

			// this code should not be run: throw an exception
			throw new Exception("This code should not be run!");
		}
	}
}

