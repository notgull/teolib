﻿/*
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
	public sealed class OutputManager : Container, IDisposable
	{
		private TextLayerCollection collection;
		private TextWriter output;
		private int margin;
		private int width = -1;
		private int height = -1;

		private const int defaultHeight = 30;
		private const int defaultWidth = 101;

		internal OutputManager (TextWriter output, int margin)
		{
			this.output = output;
			collection = new TextLayerCollection ();
			this.margin = margin;
			this.height = margin;
			this.width = Console.BufferWidth;
			this.Add (collection);
			Refresh ();
		}

		public TextLayer MakeLayer(int width, int height, bool xyView) {
			if (this.width != -1 && this.height != -1)
				throw new ArgumentException ("Use the version with no parameters to add to already existing layers.", "width");

			return MakeLayerInternal (width, height, xyView);
		}

		public TextLayer MakeLayer(bool xyView) {
			return MakeLayerInternal (width, height, xyView);
		}

		public TextLayer MakeLayer() {
			return MakeLayer (true);
		}

		private TextLayer MakeLayerInternal(int width, int height, bool xyView) {
			TextLayer layer = new TextLayer (width, height, xyView);
			this.width = width;
			this.height = height;
			this.margin = margin;
			collection.Add (layer);
			//this.Add (layer);
			return layer;
		}

		internal OutputManager (TextWriter output) : this(output, Console.BufferHeight) { }

		internal OutputManager() : this(Console.Out) {}

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



			foreach (char[] row in MergedLayer.Data) {
				Console.WriteLine (new String (row));
			}
		}
	}
}
		
