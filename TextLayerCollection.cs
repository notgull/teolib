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
	/// <summary>
	/// Acts as a collection of TextLayers
	/// </summary>
	public class TextLayerCollection : IList<TextLayer>	{
		/// <summary>
		/// Creates a new TextLayerCollection
		/// </summary>
		public TextLayerCollection ()
		{
			textLayer = new List<TextLayer> ();
		}

		private List<TextLayer> textLayer;

		/// <summary>
		/// Initializes a new instance of the TextLayerCollection class.
		/// </summary>
		/// <param name="layer">Layer to have</param>
		public TextLayerCollection(IEnumerable<TextLayer> layer) : base() {
			textLayer = new List<TextLayer> (layer);
		}

		/// <summary>
		/// Merges the layers.
		/// </summary>
		/// <returns>The merged layer</returns>
		public TextLayer MergeLayers() {
			if (textLayer.Count == 0)
				return null;

			TextLayer curr = null;
			foreach (TextLayer tl in this) {
				if (curr == null) {
					curr = tl;
				} else {
					TextLayer nLayer = tl;
					nLayer.MergeLayer (curr);
					curr = nLayer;
				}
			}
			return curr;
		}

		private void AddInternal(TextLayer tl) {
			textLayer.Add (tl);
		}

		/// <summary>
		/// Add the specified component.
		/// </summary>
		/// <param name="component">Component.</param>
		public void Add (TextLayer component)
		{
			if (component is TextLayer)
				AddInternal (component as TextLayer);
			throw new InvalidCastException ("Could not cast component into TextLayer");
		}

		/// <summary>
		/// Gets or sets the TextLayer at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public TextLayer this [int index] {
			get { return textLayer [index]; }
			set { textLayer [index] = value; }
		}

		protected void Dispose (bool disposing)
		{
			if (disposing)
				OnDispose ();
		}

		public void Dispose() { Dispose(true); }
		~TextLayerCollection() { this.Dispose(false); }

		/// <summary>
		/// Gets the index of the item
		/// </summary>
		/// <returns>The index</returns>
		/// <param name="item">Item.</param>
		public int IndexOf (TextLayer item) {
			return textLayer.IndexOf (item);
		}

		/// <summary>
		/// Insert the specified item at the index
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="item">Item.</param>
		public void Insert (int index, TextLayer item) {
			textLayer.Insert (index, item);
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt (int index) {
			textLayer.RemoveAt (index);
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get { return textLayer.Count; }
		}

		bool ICollection<TextLayer>.IsReadOnly {
			get { return false; }
		}


		void ICollection<TextLayer>.Add (TextLayer item) { AddInternal(item); }

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear () { textLayer.Clear(); }

		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Contains (TextLayer item) { return this.textLayer.Contains(item); }

		public void CopyTo (TextLayer[] array, int arrayIndex) {
			textLayer.CopyTo (array, arrayIndex);
		}

		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove (TextLayer item) { return textLayer.Remove (item); }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<TextLayer> GetEnumerator() {
			return textLayer.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return textLayer.GetEnumerator ();
		}

		/// <summary>
		/// Occurs when disposed.
		/// </summary>
		public event EventHandler Disposed;

		private void OnDispose() {
			EventHandler disposed = Disposed;
			if (disposed != null)
				disposed (this, EventArgs.Empty);
		}

	}
}

