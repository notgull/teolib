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
using System.ComponentModel;

namespace teolib
{
	public class Site : ISite
	{
		private IComponent component;
		private IContainer container;
		private bool designMode;
		private string name;

		public Site (IComponent component, IContainer container, bool designMode, string name)
		{
			this.component = component;
			this.container = container;
		    this.designMode = designMode;
			this.name = name;
		}

		public Site (IComponent component, IContainer container, bool designMode) : this(component, container, designMode, component.GetType().FullName) {

		}

		public Site (IComponent component, IContainer container) : this(component, container, false) {
		}

		public IComponent Component { get { return this.component; } }
		public IContainer Container { get { return this.container; } }
		public bool DesignMode { get { return this.designMode; } }
		public string Name {
			get { return name; }
			set { name = value; }
		}
	}
}

