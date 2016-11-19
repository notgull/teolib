using System;
using System.Threading;
namespace teolib
{
	/// <summary>
	/// Helper class for Teolib functions
	/// </summary>
	public static class Teolib
	{
		/// <summary>
		/// Retrieves an empty character array, made entirely out of spaces
		/// </summary>
		/// <returns>The empty char array.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
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

		/// <summary>
		/// Gets the output manager.
		/// </summary>
		/// <returns>The output manager.</returns>
		public static OutputManager GetOutputManager() {
			if (outputMan == null)
				outputMan = new OutputManager ();
			return outputMan;
		}

		/// <summary>
		/// Divides then floors.
		/// </summary>
		/// <returns>The floored quotient</returns>
		/// <param name="dividend">Dividend.</param>
		/// <param name="divisor">Divisor.</param>
		public static int DivideThenFloor(int dividend, int divisor) {
			decimal d1 = (dividend as IConvertible).ToDecimal (Thread.CurrentThread.CurrentCulture);
			decimal d2 = (divisor as IConvertible).ToDecimal (Thread.CurrentThread.CurrentCulture);
			decimal d3 = d1 / d2;
			decimal d4 = Math.Floor (d3);
			return (d4 as IConvertible).ToInt32 (Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>
		/// Dividess then ceils.
		/// </summary>
		/// <returns>The ceiled quotient</returns>
		/// <param name="dividend">Dividend.</param>
		/// <param name="divisor">Divisor.</param>
		public static int DivideThenCeil(int dividend, int divisor) {
			decimal d1 = (dividend as IConvertible).ToDecimal (Thread.CurrentThread.CurrentCulture);
			decimal d2 = (divisor as IConvertible).ToDecimal (Thread.CurrentThread.CurrentCulture);
			decimal d3 = d1 / d2;
			decimal d4 = Math.Ceiling (d3);
			return (d4 as IConvertible).ToInt32 (Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>
		/// Gets the output manager.
		/// </summary>
		/// <returns>The output manager.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static OutputManager GetOutputManager(int height) {
			if (outputMan == null)
				outputMan = new OutputManager (Console.Out, height);
			else
				throw new Exception ("The OutputManager has already been initialized!");
			return outputMan;
		}

		/// <summary>
		/// Gets an empty map, made entirely out of spaces
		/// </summary>
		/// <returns>The empty map.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static TextMap GetEmptyMap(int width, int height) {
			return new TextMap(width, height);
		}

		/// <summary>
		/// Gets an empty layer, made entirely out of spaces
		/// </summary>
		/// <returns>The empty layer.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static TextLayer GetEmptyLayer(int width, int height) {
			return new TextLayer(width, height);
		}

		/// <summary>
		/// Determines if is number odd the specified i.
		/// </summary>
		/// <returns><c>true</c> if is number odd the specified i; otherwise, <c>false</c>.</returns>
		/// <param name="i">The index.</param>
		public static bool IsNumberOdd(int i) {
			decimal d = ((decimal)i) / 2;
			decimal f = Math.Floor (d);

			return d != f;
		}
	}
}

