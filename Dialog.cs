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

namespace teolib
{
	public abstract class Dialog : MarshalByRefObject
	{
		private OutputManager oManager;
		private int width;
		private int height;

		public Dialog (OutputManager oManager, int width, int height)
		{
			this.oManager = oManager;
			this.width = width;
			this.height = height;
		}



		public void Execute() {
			TextLayer layer = oManager.MakeLayer (width, height, false);
			char[][] mapping = new char[width] [height];
			mapping [0] = makeRow (true);
			for (int i = 1; i < height - 1; i++) {
				string addition = "|";
				for (int j = 0; j < width - 2; j++) {
					addition += " ";
				}
				addition += "|";
				mapping [i] = addition.ToCharArray ();
			}

			TextMap clippedMap = SynthesizeMap ().Clip (width - 1, height - 1);
			layer.AppendMap (clippedMap, TextMapApplyMode.OverwriteSpacesOnly, true, 1, 1);
			oManager.Refresh ();
		}

		protected abstract TextMap SynthesizeMap ();

		private char[] makeRow(bool top) {
			string result = "";
			if (top) {
				result += "/";
				for (int i = 0; i < width - 2; i++) {
					result += "‾";
				}
				result += "\\";
			} else {
				result += "\\";
				for (int i = 0; i < width - 2; i++) {
					result += "_";
				}
			}
			return result.ToCharArray ();
		}
	}
}

