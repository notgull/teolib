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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace teolib
{
	/// <summary>
	/// The mode of which to apply Text Maps to Text Layers
	/// </summary>
	public enum TextMapApplyMode {
		/// <summary>
		/// Overwrite all characters with new characters
		/// </summary>
		OverwriteAll = 0,
		/// <summary>
		/// Only overwrite spaces only
		/// </summary>
		OverwriteSpacesOnly = 1,
		/// <summary>
		/// Only overwrite characters that aren't spaces
		/// </summary>
		OverwriteCharsOnly = 2,
	}

	/// <summary>
	/// Represents one layer of text
	/// </summary>
	public class TextLayer : TextMap, ICloneable
	{
		
		private bool xyView;

		/// <summary>
		/// Determines whether or not the TextLayer is compiled or not
		/// </summary>
		/// <value><c>true</c> if XY view; otherwise, <c>false</c>.</value>
		public bool XYView { get { return xyView; } set { xyView = value; } }


		/// <summary>
		/// Initializes a new instance of the TextLayer class.
		/// </summary>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="xyView">if the layer is autocompiled or not</param>
		public TextLayer (int width, int height, bool xyView) : base(width, height)
		{
			this.xyView = xyView;
		}

		/// <summary>
		/// Compiles a layer to be able to be printed by an OutputManager
		/// </summary>
		/// <returns>The compiled layer.</returns>
		/// <param name="layer">Layer to be cmpiled</param>
		public static TextLayer CompileLayer(TextLayer layer) { 
			char[][] data = layer.data;
			char[][] flippeddata;
			if (layer.XYView) {
				flippeddata = new char[data.Length][];
				for (int j = 0; j < data.Length; j++)
					flippeddata [0] = new char[data [0].Length];

				for (int k = 0; k < data.Length; k++) {
					for (int l = data [0].Length - 1; l >= 0; l--) {
						int x = k;
						int y = data [0].Length - 1;
						y -= l;
						flippeddata [x] [y] = data [k] [l];
					}
				}
			} else {
				flippeddata = data;
			}

			TextLayer nLayer = new TextLayer (layer.Width, layer.Height, layer.xyView);
			TextMap nMap = new TextMap (flippeddata);
			nLayer.AppendMap (nMap);
			return nLayer;
		}

		/// <summary>
		/// Initializes a new instance of the TextLayer class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public TextLayer(int width, int height) : this (width, height, true) { 

		}

		/*public TextLayer (Size size) : this(size.Width, size.Height) {
		}*/

		/// <summary>
		/// Appends a TextMap to this TextLater
		/// </summary>
		/// <returns>True if the map had to be clipped</returns>
		/// <param name="map">TextMap.</param>
		/// <param name="applyMode">Apply mode.</param>
		/// <param name="appendSpaces">If set to <c>true</c> append spaces.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, int x, int y, int width, int height) {
			TextMap subj = map.Clip (width, height);
			bool forcedClip = false;
			char[][] data = Data;
			for (int i = x; i < width; i++) {
				int k = i - x;
				for (int j = y; j < height; j++) {
					int l = j - y;
					try {
						if (!appendSpaces && subj.Data[k][l] == ' ')
							continue;
						if (applyMode == TextMapApplyMode.OverwriteAll)
						    data[i][j] = subj.Data[k][l];
						else if (applyMode == TextMapApplyMode.OverwriteSpacesOnly && data[i][j] == ' ')
							data[i][j] = subj.Data[k][l];
						else if (applyMode == TextMapApplyMode.OverwriteCharsOnly && data[i][j] != ' ')
							data[i][j] = subj.Data[k][l];  
					}
					catch (ArgumentOutOfRangeException) {
						forcedClip = true;
						continue;
					}
				}
			}
			Data = data;
			return forcedClip;
		}

	

		/*public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Point point, Size size) {
			return AppendMap (map, applyMode, appendSpaces, point.X, point.Y, size.Width, size.Height);
		}*/

	    /*
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Point point) {
			return AppendMap (map, applyMode, appendSpaces, point, map.Size);
		}*/

		/// <summary>
		/// Appends a TextMap to this TextLater
		/// </summary>
		/// <returns>True if the map had to be clipped</returns>
		/// <param name="map">TextMap.</param>
		/// <param name="applyMode">Apply mode.</param>
		/// <param name="appendSpaces">If set to <c>true</c> append spaces.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, int x, int y) {
			return AppendMap (map, applyMode, appendSpaces, x, y, map.Width, map.Height);
		}

		/*
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Rectangle rect) {
			return AppendMap (map, applyMode, appendSpaces, rect.Location, rect.Size);
		}
		*/

		/// <summary>
		/// Appends a TextMap to this TextLater
		/// </summary>
		/// <returns>True if the map had to be clipped</returns>
		/// <param name="map">TextMap.</param>
		/// <param name="applyMode">Apply mode.</param>
		/// <param name="appendSpaces">If set to <c>true</c> append spaces.</param>
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces) {
			return AppendMap (map, applyMode, appendSpaces, 0, 0, map.Size.Width, map.Size.Height);
		}

		/// <summary>
		/// Appends a TextMap to this TextLater
		/// </summary>
		/// <returns>True if the map had to be clipped</returns>
		/// <param name="map">TextMap.</param>
		/// <param name="applyMode">Apply mode.</param>
		public bool AppendMap(TextMap map, TextMapApplyMode applyMode) {
			return AppendMap (map, applyMode, false);
		}

		/// <summary>
		/// Appends a TextMap to this TextLater
		/// </summary>
		/// <returns>True if the map had to be clipped</returns>
		/// <param name="map">TextMap.</param>
		public bool AppendMap(TextMap map) {
			return AppendMap (map, TextMapApplyMode.OverwriteAll);
		}

		/// <summary>
		/// Merges this layer with another layer
		/// </summary>
		/// <returns>The merged layer</returns>
		/// <param name="other">Other layer</param>
		/// <param name="isOtherTop">If set to <c>true</c>, the other one is on top</param>
		public TextLayer MergeLayer(TextLayer other, bool isOtherTop) {
			TextLayer clone = (TextLayer)Clone ();
			if (isOtherTop)
				clone.AppendMap (other);
			else {
				TextLayer result = (TextLayer)(other.Clone ());
				result.AppendMap (clone);
				clone = result;
			}
			return clone;
		}

		/// <summary>
		/// Merges this layer with another layer
		/// </summary>
		/// <returns>The merged layer</returns>
		/// <param name="other">Other layer</param>
		public TextLayer MergeLayer(TextLayer other) { return MergeLayer(other, true); }

		/// <summary>
		/// Merges two layers.
		/// </summary>
		/// <returns>The merged layer</returns>
		/// <param name="bottom">Bottom.</param>
		/// <param name="top">Top.</param>
		public static TextLayer MergeLayers(TextLayer bottom, TextLayer top) {
			return bottom.MergeLayer (top);
		}

		/// <summary>
		/// Gets the letter at x and y.
		/// </summary>
		/// <returns>The character</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public char GetLetterAt(int x, int y) {
			return Data [x] [y];
		}

		/// <summary>
		/// Changes the letter at x, y and value.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="value">Value.</param>
		public void ChangeLetterAt(int x, int y, char value) {
			char[][] data = Data;
			data [x] [y] = value;
			Data = data;
		}

		protected override TextMap internalClone() {
			TextLayer tl = new TextLayer (Width, Height);
			TextMap tm = base.internalClone ();
			tl.AppendMap (tm);
			return tl;
		}
	}
}

