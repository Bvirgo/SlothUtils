using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class Composing
	{
		public static IEnumerator<float> Union(IEnumerator<float> a, IEnumerator<float> b)
		{
			while (a.MoveNext() && b.MoveNext()) {
				yield return a.Current + b.Current;
			}
		}

		public static IEnumerator<Vector2> Union(IEnumerator<Vector2> a, IEnumerator<Vector2> b)
		{
			while (a.MoveNext() && b.MoveNext()) {
				yield return a.Current + b.Current;
			}
		}

		public static IEnumerator<Vector3> Union(IEnumerator<Vector3> a, IEnumerator<Vector3> b)
		{
			while (a.MoveNext() && b.MoveNext()) {
				yield return a.Current + b.Current;
			}
		}

		public static IEnumerator<Vector4> Union(IEnumerator<Vector4> a, IEnumerator<Vector4> b)
		{
			while (a.MoveNext() && b.MoveNext()) {
				yield return a.Current + b.Current;
			}
		}
	}
}