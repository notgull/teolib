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

namespace teolib
{
	public class TextMap {
		private char[][] data;
		private Size size;

		public TextMap(char[][] data) {
			this.data = data;
			int width = data.Length;
			int height = data [0].Length;
			size = new Size (width, height);
		}

		public char[][] Data {
			get { return data; }
		}

		public Size Size {
			get { return size; }
		}

		public TextMap Clip(int width, int height) {
			if (new Size (width, height) == size)
				return this;

			char[][] clippedMap = new char[width][];
			for (int i = 0; i < width; i++) {
				clippedMap [i] = new char[height];
				for (int j = 0; j < height; j++)
					clippedMap [i] [j] = data [i] [j];
			}

			return new TextMap (clippedMap);
		}

		public TextMap Clip(Size s) {
			return Clip (s.Width, s.Height);
		}

		public char GetcharAt(int x, int y) {
			return Data [x] [y];
		}

		public void ChangecharAt(int x, int y, char value) {
			data [x] [y] = value;
		}
	}
}

