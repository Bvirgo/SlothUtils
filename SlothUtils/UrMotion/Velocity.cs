using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class Velocity
	{
		public static IEnumerator<V> Constant<V>(V val)
		{
			return Source.Constant(val);
		}

		public static IEnumerator<float> Accel(IEnumerator<float> a)
		{
			var v = default(float);
			while (a.MoveNext()) {
				v += a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector2> Accel(IEnumerator<Vector2> a)
		{
			var v = default(Vector2);
			while (a.MoveNext()) {
				v += a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector3> Accel(IEnumerator<Vector3> a)
		{
			var v = default(Vector3);
			while (a.MoveNext()) {
				v += a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector4> Accel(IEnumerator<Vector4> a)
		{
			var v = default(Vector4);
			while (a.MoveNext()) {
				v += a.Current;
				yield return v;
			}
		}

		public static IEnumerator<float> AccelByRatio(float iv, IEnumerator<float> a)
		{
			var v = iv;
			while (a.MoveNext()) {
				v *= a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector2> AccelByRatio(Vector2 iv, IEnumerator<float> a)
		{
			var v = iv;
			while (a.MoveNext()) {
				v *= a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector3> AccelByRatio(Vector3 iv, IEnumerator<float> a)
		{
			var v = iv;
			while (a.MoveNext()) {
				v *= a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector4> AccelByRatio(Vector4 iv, IEnumerator<float> a)
		{
			var v = iv;
			while (a.MoveNext()) {
				v *= a.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector2> Angle(IEnumerator<Vector2> source, IEnumerator<float> angle)
		{
			while (source.MoveNext() && angle.MoveNext()) {
				yield return Quaternion.Euler(0f, 0f, angle.Current) * source.Current;
			}
		}

		public static IEnumerator<Vector3> Angle(IEnumerator<Vector3> source, IEnumerator<Vector3> angle)
		{
			while (source.MoveNext() && angle.MoveNext()) {
				yield return Quaternion.Euler(angle.Current) * source.Current;
			}
		}
	}
}