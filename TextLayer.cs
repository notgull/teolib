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
	public enum TextMapApplyMode {
		OverwriteAll = 0,
		OverwriteSpacesOnly = 1,
		OverwriteCharsOnly = 2,
	}

	public class TextLayer : System.ComponentModel.Component, ICloneable
	{
		private char[][] mapping;
		private int width;
		private int height;

		private bool xyView;

		public bool XYView { get { return xyView; } set { xyView = value; } }

		public int Width { get { return width; } }

		public int Height { get { return height; } }

		public TextLayer (int width, int height, bool xyView)
		{
			mapping = new char[width][];
			for (int i = 0; i < height; i++)
				mapping [i] = new char[height];

			for (int j = 0; j < width; j++) {
				char[] column = mapping [j];
				for (int k = 0; k < height; k++)
					column [k] = ' ';
			}

			this.width = width;
			this.height = height;
			this.xyView = xyView;
		}

		public static TextLayer CompileLayer(TextLayer layer) { 
			char[][] mapping = MergedLayer.Mapping;
			char[][] flippedMapping;
			if (layer.XYView) {
				flippedMapping = new char[mapping.Length][];
				for (int j = 0; j < mapping.Length; j++)
					flippedMapping [0] = new char[mapping [0].Length];

				for (int k = 0; k < mapping.Length; k++) {
					for (int l = mapping [0].Length - 1; l >= 0; l--) {
						int x = k;
						int y = mapping [0].Length - 1;
						y -= l;
						flippedMapping [x] [y] = mapping [k] [l];
					}
				}
			} else {
				flippedMapping = mapping;
			}

			TextLayer nLayer = new TextLayer (layer.width, layer.height, layer.xyView);
			TextMap nMap = new TextMap (flippedMapping);
			nLayer.AppendMap (nMap);
			return nLayer;
		}

		public TextLayer(int width, int height) : this (width, height, true) { 

		}

		/*public TextLayer (Size size) : this(size.Width, size.Height) {
		}*/

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, int x, int y, int width, int height) {
			TextMap subj = map.Clip (width, height);
			bool forcedClip = false;
			for (int i = x; i < width; i++) {
				int k = i - x;
				for (int j = y; j < height; j++) {
					int l = j - y;
					try {
						if (!appendSpaces && subj.Data[k][l] == ' ')
							continue;
						if (applyMode == TextMapApplyMode.OverwriteAll)
						    mapping[i][j] = subj.Data[k][l];
						else if (applyMode == TextMapApplyMode.OverwriteSpacesOnly && mapping[i][j] == ' ')
							mapping[i][j] = subj.Data[k][l];
						else if (applyMode == TextMapApplyMode.OverwriteCharsOnly && mapping[i][j] != ' ')
							mapping[i][j] = subj.Data[k][l];  
					}
					catch (ArgumentOutOfRangeException) {
						forcedClip = true;
						continue;
					}
				}
			}
			return forcedClip;
		}

		public char[][] Mapping {
			get  { return mapping; }
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Point point, Size size) {
			return AppendMap (map, applyMode, appendSpaces, point.X, point.Y, size.Width, size.Height);
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Point point) {
			return AppendMap (map, applyMode, appendSpaces, point, map.Size);
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, int x, int y) {
			return AppendMap (map, applyMode, appendSpaces, new Point (x, y));
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces, Rectangle rect) {
			return AppendMap (map, applyMode, appendSpaces, rect.Location, rect.Size);
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode, bool appendSpaces) {
			return AppendMap (map, applyMode, appendSpaces, 0, 0, map.Size.Width, map.Size.Height);
		}

		public bool AppendMap(TextMap map, TextMapApplyMode applyMode) {
			return AppendMap (map, applyMode, false);
		}

		public bool AppendMap(TextMap map) {
			return AppendMap (map, TextMapApplyMode.OverwriteAll);
		}

		public TextMap ToMap() {
			return new TextMap (mapping);
		}

		public TextLayer MergeLayer(TextLayer other, bool isOtherTop) {
			TextLayer clone = Clone ();
			if (isOtherTop)
				clone.AppendMap (other.ToMap ());
			else {
				TextLayer result = other.Clone ();
				result.AppendMap (clone.ToMap ());
				clone = result;
			}
			return clone;
		}

		public TextLayer MergeLayer(TextLayer other) { return MergeLayer(other, true); }

		public static TextLayer MergeLayers(TextLayer bottom, TextLayer top) {
			return bottom.MergeLayer (top);
		}

		public char GetLetterAt(int x, int y) {
			return mapping [x] [y];
		}

		public void ChangeLetterAt(int x, int y, char value) {
			mapping [x] [y] = value;
		}

		object ICloneable.Clone() {
			return internalClone();
		}

		public TextLayer Clone() {
			return internalClone();
		}

		private TextLayer internalClone() {
			TextLayer tl = new TextLayer (width, height);
			tl.AppendMap (this.ToMap ());
			return tl;
		}
	}
}

