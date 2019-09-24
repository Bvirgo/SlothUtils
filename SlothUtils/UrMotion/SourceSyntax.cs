using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	using Syntax = FluentSyntax;

	public static class SourceSyntax
	{
		public static IEnumerator<V> Sin<V, T1, T2>(this Source.SourceDimension<V> self, T1 radius, T2 freq, float fps = 0f)
		{
			var res = default(IEnumerator<V>);
			Syntax.Resolve<V>(
				() => res = Source.Sin(Syntax.AsEnumerator<float,   T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Sin(Syntax.AsEnumerator<Vector2, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Sin(Syntax.AsEnumerator<Vector3, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Sin(Syntax.AsEnumerator<Vector4, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>
			);
			return res;
		}

		public static IEnumerator<V> Cos<V, T1, T2>(this Source.SourceDimension<V> self, T1 radius, T2 freq, float fps = 0f)
		{
			var res = default(IEnumerator<V>);
			Syntax.Resolve<V>(
				() => res = Source.Cos(Syntax.AsEnumerator<float,   T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Cos(Syntax.AsEnumerator<Vector2, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Cos(Syntax.AsEnumerator<Vector3, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>,
				() => res = Source.Cos(Syntax.AsEnumerator<Vector4, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), fps) as IEnumerator<V>
			);
			return res;
		}

		public static IEnumerator<Vector2> Circular<T1, T2>(this Source.SourceDimension<Vector2> self, T1 radius, T2 speed, float fps = 0f)
		{
			return Source.Circular(Syntax.AsEnumerator<float, T1>(radius), Syntax.AsEnumerator<float, T2>(speed), fps);
		}

		public static IEnumerator<Vector2> Lissajous<T1, T2, T3, T4>(this Source.SourceDimension<Vector2> self, T1 A, T2 B, T3 a, T4 b, float delta, float fps = 0f)
		{
			return Source.Lissajous(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), Syntax.AsEnumerator<float, T3>(a), Syntax.AsEnumerator<float, T4>(b), delta, fps);
		}

		public static IEnumerator<Vector2> Cycloid<T1, T2, T3>(this Source.SourceDimension<Vector2> self, T1 A, T2 B, float rm, T3 speed, float fps = 0f)
		{
			return Source.Cycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rm, Syntax.AsEnumerator<float, T3>(speed), fps);
		}

		public static IEnumerator<Vector2> Epicycloid<T1, T2, T3>(this Source.SourceDimension<Vector2> self, T1 A, T2 B, float rc, float rm, T3 speed, float fps = 0f)
		{
			return Source.Epicycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rc, rm, Syntax.AsEnumerator<float, T3>(speed), fps);
		}

		public static IEnumerator<Vector2> Hypocycloid<T1, T2, T3>(this Source.SourceDimension<Vector2> self, T1 A, T2 B, float rc, float rm, T3 speed, float fps = 0f)
		{
			return Source.Hypocycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rc, rm, Syntax.AsEnumerator<float, T3>(speed), fps);
		}
	}
}