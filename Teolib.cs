using System;

namespace teolib
{
	public static class Teolib
	{
		public static char[][] GetEmptyCharArray(int width, int height) {
			char[][] cArr = new char[width][];
			for (int i = 0; i < width; i++) {
				cArr [i] = new char[height];
				for (int j = 0; j < height; j++)
					cArr [i] [j] = ' ';
			}

			return cArr;
		}

		private static OutputManager outputMan = null;

		public static OutputManager GetOutputManager() {
			if (outputMan == null)
				outputMan = new OutputManager ();
			return outputMan;
		}

		public static OutputManager GetOutputManager(int width, int height) {
			if (outputMan == null)
				outputMan = new OutputManager (width, height);
			else
				throw new Exception ("The OutputManager has already been initialized!");
			return outputMan;
		}

		public static TextMap GetEmptyMap(int width, int height) {
			return new TextMap(width, height);
		}

		public static TextLayer GetEmptyLayer(int width, int height) {
			return new TextLayer(width, height);
		}
	}
}

