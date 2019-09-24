using System;
using UnityEngine;

namespace SlothMotion
{
	public static class Comparator
	{
		public static readonly Func<float, float, bool> LessThan = (a, b) => a < b;
		public static readonly Func<float, float, bool> GreaterThan = (a, b) => a > b;
		public static readonly Func<float, float, bool> LessThanOrEqualTo = (a, b) => a <= b;
		public static readonly Func<float, float, bool> GreaterThanOrEqualTo = (a, b) => a >= b;
		public static readonly Func<float, float, bool> EqualTo = (a, b) => a == b;
		public static readonly Func<float, float, bool> Approximately = (a, b) => Mathf.Approximately(a, b);
		public static Func<float, float, bool> Near(float threshold)
		{
			return (a, b) => Mathf.Abs(b - a) <= threshold;
		}
	}
}