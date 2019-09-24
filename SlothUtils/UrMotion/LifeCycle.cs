using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class LifeCycle
	{
		public static IEnumerator<V> Lifetime<V>(IEnumerator<V> source, IEnumerator<float> period)
		{
			var time = 0f;
			while (source.MoveNext() && period.MoveNext()) {
				if (time >= period.Current) {
					yield break;
				}
				yield return source.Current;
				time += 1f;
			}
		}

		public static IEnumerator<V> Delay<V>(IEnumerator<V> source, IEnumerator<float> delay)
		{
			var time = 0f;
			for (;;) {
				if (delay.MoveNext()) {
					if (time >= delay.Current) {
						break;
					}
				} else {
					yield break;
				}
				yield return default(V);
				time += 1f;
			}
			while (source.MoveNext()) {
				yield return source.Current;
			}
		}

		public static IEnumerator<float> LiveThreshold(IEnumerator<float> source, IEnumerator<float> threshold)
		{
			return LiveThreshold(source, threshold, Comparator.LessThan);
		}

		public static IEnumerator<Vector2> LiveThreshold(IEnumerator<Vector2> source, IEnumerator<float> threshold)
		{
			return LiveThreshold(source, threshold, Comparator.LessThan);
		}

		public static IEnumerator<Vector3> LiveThreshold(IEnumerator<Vector3> source, IEnumerator<float> threshold)
		{
			return LiveThreshold(source, threshold, Comparator.LessThan);
		}

		public static IEnumerator<Vector4> LiveThreshold(IEnumerator<Vector4> source, IEnumerator<float> threshold)
		{
			return LiveThreshold(source, threshold, Comparator.LessThan);
		}

		public static IEnumerator<float> LiveThreshold(IEnumerator<float> source, IEnumerator<float> threshold, Func<float, float, bool> comparator)
		{
			while (source.MoveNext() && threshold.MoveNext()) {
				var val = source.Current;
				var th = threshold.Current;
				if (comparator(val, th)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector2> LiveThreshold(IEnumerator<Vector2> source, IEnumerator<float> threshold, Func<float, float, bool> comparator)
		{
			while (source.MoveNext() && threshold.MoveNext()) {
				var val = source.Current;
				var th = threshold.Current;
				if (comparator(val.sqrMagnitude, th)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector3> LiveThreshold(IEnumerator<Vector3> source, IEnumerator<float> threshold, Func<float, float, bool> comparator)
		{
			while (source.MoveNext() && threshold.MoveNext()) {
				var val = source.Current;
				var th = threshold.Current;
				if (comparator(val.sqrMagnitude, th)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector4> LiveThreshold(IEnumerator<Vector4> source, IEnumerator<float> threshold, Func<float, float, bool> comparator)
		{
			while (source.MoveNext() && threshold.MoveNext()) {
				var val = source.Current;
				var th = threshold.Current;
				if (comparator(val.sqrMagnitude, th)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<float> LiveUntil(IEnumerator<float> source, Func<float, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector2> LiveUntil(IEnumerator<Vector2> source, Func<Vector2, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector3> LiveUntil(IEnumerator<Vector3> source, Func<Vector3, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<Vector4> LiveUntil(IEnumerator<Vector4> source, Func<Vector4, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield break;
				} else {
					yield return val;
				}
			}
		}

		public static IEnumerator<float> LiveWhile(IEnumerator<float> source, Func<float, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield return val;
				} else {
					yield break;
				}
			}
		}

		public static IEnumerator<Vector2> LiveWhile(IEnumerator<Vector2> source, Func<Vector2, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield return val;
				} else {
					yield break;
				}
			}
		}

		public static IEnumerator<Vector3> LiveWhile(IEnumerator<Vector3> source, Func<Vector3, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield return val;
				} else {
					yield break;
				}
			}
		}

		public static IEnumerator<Vector4> LiveWhile(IEnumerator<Vector4> source, Func<Vector4, bool> condition)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				if (condition(val)) {
					yield return val;
				} else {
					yield break;
				}
			}
		}
	}
}