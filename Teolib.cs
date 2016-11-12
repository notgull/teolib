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
	}
}

