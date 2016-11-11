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
using System.ComponentModel;

namespace teolib
{
	public class TextLayerCollection : Container, IList<TextLayer>, IComponent	{
		public TextLayerCollection ()
		{
			textLayer = new List<TextLayer> ();
		}

		private List<TextLayer> textLayer;

		private ISite site;
		public ISite Site {
			get { return site; }
			set { site = value; }
		}

		public override ComponentCollection Components {
			get {
				List<Component> component = new List<Component> ();
				foreach (TextLayer layer in this)
					component.Add (layer);
				return new ComponentCollection (component.ToArray ()); 
			}
		}

		public TextLayerCollection(IEnumerable<TextLayer> layer) : base() {
			textLayer = new List<TextLayer> (layer);
		}

		public TextLayer MergeLayers() {
			TextLayer curr = null;
			foreach (TextLayer tl in this) {
				if (curr == null) {
					curr = TextLayer.CompileLayer (tl);
				} else {
					TextLayer nLayer = TextLayer.CompileLayer (tl);
					nLayer.MergeLayer (curr);
					curr = nLayer;
				}
			}
			return curr;
		}

		private void AddInternal(TextLayer tl) {
			textLayer.Add (tl);
			base.Add (tl);
		}

		public override void Add (IComponent component)
		{
			if (component is TextLayer)
				AddInternal (component as TextLayer);
			throw new InvalidCastException ("Could not cast component into TextLayer");
		}

		public TextLayer this [int index] {
			get { return textLayer [index]; }
			set { textLayer [index] = value; }
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing)
				OnDispose ();
		}

		//
		// Methods
		//
		public int IndexOf (TextLayer item) {
			return textLayer.IndexOf (item);
		}

		public void Insert (int index, TextLayer item) {
			textLayer.Insert (index, item);
		}

		public void RemoveAt (int index) {
			textLayer.RemoveAt (index);
		}

		public int Count {
			get { return textLayer.Count; }
		}

		bool ICollection<TextLayer>.IsReadOnly {
			get { return false; }
		}

		//
		// Methods
		//
		void ICollection<TextLayer>.Add (TextLayer item) { AddInternal(item); }

		public void Clear () { textLayer.Clear(); }

		public bool Contains (TextLayer item) { return this.textLayer.Contains(item); }

		public void CopyTo (TextLayer[] array, int arrayIndex) {
			textLayer.CopyTo (array, arrayIndex);
		}

		public bool Remove (TextLayer item) { return textLayer.Remove (item); }

		public IEnumerator<TextLayer> GetEnumerator() {
			return textLayer.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return textLayer.GetEnumerator ();
		}

		public event EventHandler Disposed;

		private void OnDispose() {
			EventHandler disposed = Disposed;
			if (disposed != null)
				disposed (this, EventArgs.Empty);
		}

	}
}

