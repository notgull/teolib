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
using System.Collections.Generic;

namespace teolib
{
	public class TextLayerCollection : List<TextLayer>
	{
		public TextLayerCollection ()
		{
		}

		public TextLayerCollection(IEnumerable<TextLayer> layer) : base(layer) {
		}

		public TextLayer MergeLayers() {
			TextLayer curr = null;
			foreach (TextLayer tl in this) {
				if (curr == null)
					curr = tl;
				else
					curr = tl.MergeLayer (curr);
			}
			return curr;
		}
	}
}

