using System;
using UnityEngine;

namespace SlothMotion
{
	using Syntax = FluentSyntax;

	public static class MovementSyntax
	{
		public static MotionBehaviour<V> Sin<V, T1, T2>(this MotionBehaviour<V> self, T1 radius, T2 freq)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Movement.Sin(Syntax.AsEnumerator<float,   T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Sin(Syntax.AsEnumerator<Vector2, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Sin(Syntax.AsEnumerator<Vector3, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Sin(Syntax.AsEnumerator<Vector4, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate))
			);
			return self;
		}

		public static MotionBehaviour<V> Cos<V, T1, T2>(this MotionBehaviour<V> self, T1 radius, T2 freq)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Movement.Cos(Syntax.AsEnumerator<float,   T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Cos(Syntax.AsEnumerator<Vector2, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Cos(Syntax.AsEnumerator<Vector3, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate)),
				(e) => e.Add(Movement.Cos(Syntax.AsEnumerator<Vector4, T1>(radius), Syntax.AsEnumerator<float, T2>(freq), self.FrameRate))
			);
			return self;
		}

		public static MotionBehaviour<Vector2> Circular<T1, T2>(this MotionBehaviour<Vector2> self, T1 radius, T2 speed)
		{
			self.Add(Movement.Circular(Syntax.AsEnumerator<float, T1>(radius), Syntax.AsEnumerator<float, T2>(speed), self.FrameRate));
			return self;
		}

		public static MotionBehaviour<Vector2> Lissajous<T1, T2, T3, T4>(this MotionBehaviour<Vector2> self, T1 A, T2 B, T3 a, T4 b, float delta)
		{
			self.Add(Movement.Lissajous(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), Syntax.AsEnumerator<float, T3>(a), Syntax.AsEnumerator<float, T4>(b), delta, self.FrameRate));
			return self;
		}

		public static MotionBehaviour<Vector2> Cycloid<T1, T2, T3>(this MotionBehaviour<Vector2> self, T1 A, T2 B, float rm, T3 speed)
		{
			self.Add(Movement.Cycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rm , Syntax.AsEnumerator<float, T3>(speed), self.FrameRate));
			return self;
		}

		public static MotionBehaviour<Vector2> Epicycloid<T1, T2, T3>(this MotionBehaviour<Vector2> self, T1 A, T2 B, float rc, float rm, T3 speed)
		{
			self.Add(Movement.Epicycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rc, rm , Syntax.AsEnumerator<float, T3>(speed), self.FrameRate));
			return self;
		}

		public static MotionBehaviour<Vector2> Hypocycloid<T1, T2, T3>(this MotionBehaviour<Vector2> self, T1 A, T2 B, float rc, float rm, T3 speed)
		{
			self.Add(Movement.Hypocycloid(Syntax.AsEnumerator<float, T1>(A), Syntax.AsEnumerator<float, T2>(B), rc, rm , Syntax.AsEnumerator<float, T3>(speed), self.FrameRate));
			return self;
		}

		public static MotionBehaviour<V> Sin<V>(this MotionBehaviour<V> self, Func<V> radius, Func<float> freq)
		{
			return Sin<V, Func<V>, Func<float>>(self, radius, freq);
		}

		public static MotionBehaviour<V> Cos<V>(this MotionBehaviour<V> self, Func<V> radius, Func<float> freq)
		{
			return Cos<V, Func<V>, Func<float>>(self, radius, freq);
		}

		public static MotionBehaviour<Vector2> Circular(this MotionBehaviour<Vector2> self, Func<float> radius, Func<float> speed)
		{
			return Circular<Func<float>, Func<float>>(self, radius, speed);
		}

		public static MotionBehaviour<Vector2> Lissajous(this MotionBehaviour<Vector2> self, Func<float> A, Func<float> B, Func<float> a, Func<float> b, float delta)
		{
			return Lissajous<Func<float>, Func<float>, Func<float>, Func<float>>(self, A, B, a, b, delta);
		}

		public static MotionBehaviour<Vector2> Cycloid(this MotionBehaviour<Vector2> self, Func<float> A, Func<float> B, float rm, Func<float> speed)
		{
			return Cycloid<Func<float>, Func<float>, Func<float>>(self, A, B, rm, speed);
		}

		public static MotionBehaviour<Vector2> Epicycloid(this MotionBehaviour<Vector2> self, Func<float> A, Func<float> B, float rc, float rm, Func<float> speed)
		{
			return Epicycloid<Func<float>, Func<float>, Func<float>>(self, A, B, rc, rm, speed);
		}

		public static MotionBehaviour<Vector2> Hypocycloid(this MotionBehaviour<Vector2> self, Func<float> A, Func<float> B, float rc, float rm, Func<float> speed)
		{
			return Hypocycloid<Func<float>, Func<float>, Func<float>>(self, A, B, rc, rm, speed);
		}
	}
}