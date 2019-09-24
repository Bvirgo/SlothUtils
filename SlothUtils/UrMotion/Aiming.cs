using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class Aiming
	{
		public static IEnumerator<float> AimAt(IEnumerator<float> destination, IEnumerator<float> speed, IEnumerator<float> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var v = Mathf.Min(Mathf.Abs(delta), Mathf.Abs(speed.Current)) * Mathf.Sign(delta);
				yield return v;
			}
		}

		public static IEnumerator<Vector2> AimAt(IEnumerator<Vector2> destination, IEnumerator<float> speed, IEnumerator<Vector2> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var v = delta.normalized * speed.Current;
				if (v.sqrMagnitude < delta.sqrMagnitude) {
					yield return v;
				} else {
					yield return delta;
				}
			}
		}

		public static IEnumerator<Vector3> AimAt(IEnumerator<Vector3> destination, IEnumerator<float> speed, IEnumerator<Vector3> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var v = delta.normalized * speed.Current;
				if (v.sqrMagnitude < delta.sqrMagnitude) {
					yield return v;
				} else {
					yield return delta;
				}
			}
		}

		public static IEnumerator<Vector4> AimAt(IEnumerator<Vector4> destination, IEnumerator<float> speed, IEnumerator<Vector4> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var v = delta.normalized * speed.Current;
				if (v.sqrMagnitude < delta.sqrMagnitude) {
					yield return v;
				} else {
					yield return delta;
				}
			}
		}

		public static IEnumerator<float> AimRatioAt(IEnumerator<float> destination, IEnumerator<float> ratio, IEnumerator<float> current)
		{
			while (destination.MoveNext() && ratio.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * ratio.Current;
			}
		}

		public static IEnumerator<Vector2> AimRatioAt(IEnumerator<Vector2> destination, IEnumerator<float> ratio, IEnumerator<Vector2> current)
		{
			while (destination.MoveNext() && ratio.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * ratio.Current;
			}
		}

		public static IEnumerator<Vector3> AimRatioAt(IEnumerator<Vector3> destination, IEnumerator<float> ratio, IEnumerator<Vector3> current)
		{
			while (destination.MoveNext() && ratio.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * ratio.Current;
			}
		}

		public static IEnumerator<Vector4> AimRatioAt(IEnumerator<Vector4> destination, IEnumerator<float> ratio, IEnumerator<Vector4> current)
		{
			while (destination.MoveNext() && ratio.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * ratio.Current;
			}
		}

		public static IEnumerator<float> AimSpringAt(IEnumerator<float> destination, IEnumerator<float> ratio, IEnumerator<float> bounce, IEnumerator<float> current)
		{
			var v = default(float);
			while (destination.MoveNext() && ratio.MoveNext() && bounce.MoveNext() && current.MoveNext()) {
				v = v * bounce.Current + (destination.Current - current.Current) * ratio.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector2> AimSpringAt(IEnumerator<Vector2> destination, IEnumerator<float> ratio, IEnumerator<float> bounce, IEnumerator<Vector2> current)
		{
			var v = default(Vector2);
			while (destination.MoveNext() && ratio.MoveNext() && bounce.MoveNext() && current.MoveNext()) {
				v = v * bounce.Current + (destination.Current - current.Current) * ratio.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector3> AimSpringAt(IEnumerator<Vector3> destination, IEnumerator<float> ratio, IEnumerator<float> bounce, IEnumerator<Vector3> current)
		{
			var v = default(Vector3);
			while (destination.MoveNext() && ratio.MoveNext() && bounce.MoveNext() && current.MoveNext()) {
				v = v * bounce.Current + (destination.Current - current.Current) * ratio.Current;
				yield return v;
			}
		}

		public static IEnumerator<Vector4> AimSpringAt(IEnumerator<Vector4> destination, IEnumerator<float> ratio, IEnumerator<float> bounce, IEnumerator<Vector4> current)
		{
			var v = default(Vector4);
			while (destination.MoveNext() && ratio.MoveNext() && bounce.MoveNext() && current.MoveNext()) {
				v = v * bounce.Current + (destination.Current - current.Current) * ratio.Current;
				yield return v;
			}
		}

		public static IEnumerator<float> AimExpoAt(IEnumerator<float> destination, IEnumerator<float> speed, IEnumerator<float> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * (1f - Mathf.Exp(-speed.Current));
			}
		}

		public static IEnumerator<Vector2> AimExpoAt(IEnumerator<Vector2> destination, IEnumerator<float> speed, IEnumerator<Vector2> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * (1f - Mathf.Exp(-speed.Current));
			}
		}

		public static IEnumerator<Vector3> AimExpoAt(IEnumerator<Vector3> destination, IEnumerator<float> speed, IEnumerator<Vector3> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * (1f - Mathf.Exp(-speed.Current));
			}
		}

		public static IEnumerator<Vector4> AimExpoAt(IEnumerator<Vector4> destination, IEnumerator<float> speed, IEnumerator<Vector4> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				yield return (destination.Current - current.Current) * (1f - Mathf.Exp(-speed.Current));
			}
		}

		public static IEnumerator<float> AimCriticalDampingAt(IEnumerator<float> destination, IEnumerator<float> speed, IEnumerator<float> current)
		{
			var v = default(float);
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var omega = speed.Current;
				var n1 = v - (current.Current - destination.Current) * (omega * omega);
				var n2 = 1 + omega;
				v = n1 / (n2 * n2);
				yield return v;
			}
		}

		public static IEnumerator<Vector2> AimCriticalDampingAt(IEnumerator<Vector2> destination, IEnumerator<float> speed, IEnumerator<Vector2> current)
		{
			var v = default(Vector2);
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var omega = speed.Current;
				var n1 = v - (current.Current - destination.Current) * (omega * omega);
				var n2 = 1 + omega;
				v = n1 / (n2 * n2);
				yield return v;
			}
		}

		public static IEnumerator<Vector3> AimCriticalDampingAt(IEnumerator<Vector3> destination, IEnumerator<float> speed, IEnumerator<Vector3> current)
		{
			var v = default(Vector3);
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var omega = speed.Current;
				var n1 = v - (current.Current - destination.Current) * (omega * omega);
				var n2 = 1 + omega;
				v = n1 / (n2 * n2);
				yield return v;
			}
		}

		public static IEnumerator<Vector4> AimCriticalDampingAt(IEnumerator<Vector4> destination, IEnumerator<float> speed, IEnumerator<Vector4> current)
		{
			var v = default(Vector4);
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var omega = speed.Current;
				var n1 = v - (current.Current - destination.Current) * (omega * omega);
				var n2 = 1 + omega;
				v = n1 / (n2 * n2);
				yield return v;
			}
		}

		public static IEnumerator<float> AimVelocity(IEnumerator<float> destination, IEnumerator<float> speed, IEnumerator<float> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var v = Mathf.Min(Mathf.Abs(delta), Mathf.Abs(speed.Current)) * Mathf.Sign(delta);
				yield return v;
			}
		}

		public static IEnumerator<Vector2> AimVelocity(IEnumerator<Vector2> destination, IEnumerator<Vector2> speed, IEnumerator<Vector2> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var s = speed.Current;
				var v = default(Vector2);
				v.x = Mathf.Min(Mathf.Abs(delta.x), Mathf.Abs(s.x)) * Mathf.Sign(delta.x);
				v.y = Mathf.Min(Mathf.Abs(delta.y), Mathf.Abs(s.y)) * Mathf.Sign(delta.y);
				yield return v;
			}
		}

		public static IEnumerator<Vector3> AimVelocity(IEnumerator<Vector3> destination, IEnumerator<Vector3> speed, IEnumerator<Vector3> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var s = speed.Current;
				var v = default(Vector3);
				v.x = Mathf.Min(Mathf.Abs(delta.x), Mathf.Abs(s.x)) * Mathf.Sign(delta.x);
				v.y = Mathf.Min(Mathf.Abs(delta.y), Mathf.Abs(s.y)) * Mathf.Sign(delta.y);
				v.z = Mathf.Min(Mathf.Abs(delta.z), Mathf.Abs(s.z)) * Mathf.Sign(delta.z);
				yield return v;
			}
		}

		public static IEnumerator<Vector4> AimVelocity(IEnumerator<Vector4> destination, IEnumerator<Vector4> speed, IEnumerator<Vector4> current)
		{
			while (destination.MoveNext() && speed.MoveNext() && current.MoveNext()) {
				var delta = destination.Current - current.Current;
				var s = speed.Current;
				var v = default(Vector4);
				v.x = Mathf.Min(Mathf.Abs(delta.x), Mathf.Abs(s.x)) * Mathf.Sign(delta.x);
				v.y = Mathf.Min(Mathf.Abs(delta.y), Mathf.Abs(s.y)) * Mathf.Sign(delta.y);
				v.z = Mathf.Min(Mathf.Abs(delta.z), Mathf.Abs(s.z)) * Mathf.Sign(delta.z);
				v.w = Mathf.Min(Mathf.Abs(delta.w), Mathf.Abs(s.w)) * Mathf.Sign(delta.w);
				yield return v;
			}
		}
	}
}