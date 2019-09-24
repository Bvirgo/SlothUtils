using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class FluentSyntax
	{
		public static IEnumerator<V> AsEnumerator<V, T>(T source)
		{
			if (typeof(T) == typeof(IEnumerator<V>)) {
				return (IEnumerator<V>)source;
			}
			if (typeof(T) == typeof(IEnumerable<V>)) {
				return ((IEnumerable<V>)source).GetEnumerator();
			}
			if (typeof(T) == typeof(V)) {
				return Source.Constant((V)(object)source);
			}
			if (typeof(T) == typeof(Func<V>)) {
				return Source.Function((Func<V>)(object)source);
			}
			throw new ArgumentException($"Expected {typeof(V).FullName} but was: {typeof(T).FullName}");
		}

		public static void Resolve<V>(MotionBehaviour<V> self, Action<MotionBehaviour<float>> vec1, Action<MotionBehaviour<Vector2>> vec2, Action<MotionBehaviour<Vector3>> vec3, Action<MotionBehaviour<Vector4>> vec4)
		{
			Resolve<V>(
				() => vec1((MotionBehaviour<float  >)(object)self),
				() => vec2((MotionBehaviour<Vector2>)(object)self),
				() => vec3((MotionBehaviour<Vector3>)(object)self),
				() => vec4((MotionBehaviour<Vector4>)(object)self)
			);
		}

		public static void Resolve<V>(Action vec1, Action vec2, Action vec3, Action vec4)
		{
			if (typeof(V) == typeof(float)) {
				vec1();
				return;
			}
			if (typeof(V) == typeof(Vector2)) {
				vec2();
				return;
			}
			if (typeof(V) == typeof(Vector3)) {
				vec3();
				return;
			}
			if (typeof(V) == typeof(Vector4)) {
				vec4();
				return;
			}
			throw new ArgumentException($"{typeof(V).FullName} is not valid.");
		}
	}
}