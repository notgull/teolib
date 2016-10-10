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
using System.IO;
using System.ComponentModel;

namespace teolib
{
	public class OutputManager : Component, IDisposable
	{
		private TextLayerCollection collection;
		private TextWriter output;
		private int margin;

		public OutputManager (TextWriter output, int margin)
		{
			this.output = output;
			collection = new TextLayerCollection ();
			this.margin = margin;
			Refresh ();
		}

		public OutputManager (TextWriter output) : this(output, Console.BufferHeight) { }

		public OutputManager() : this(Console.Out) {}

		public TextLayer MergedLayer { get { return this.collection.MergeLayers(); } }

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing)
				output.Dispose ();
		}

		public int Margin {
			get { return margin; }
			set { margin = value; }
		}

		public TextLayerCollection Layers {
			get { return collection; }
		}

		public void Refresh() {
			for (int i = 0; i < margin; i++)
				Console.WriteLine ();

			// we need a row-by-row view, not an xy view. let's compile that
			char[][] mapping = MergedLayer.Mapping;
			char[][] flippedMapping = new char[mapping.Length][];
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

			foreach (char[] row in flippedMapping) {
				Console.WriteLine (new String (row));
			}
		}
	}
}
		
