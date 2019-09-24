using System;
using UnityEngine;

namespace SlothMotion
{
	using Syntax = FluentSyntax;

	public static class AimingSyntax
	{
		public static MotionBehaviour<V> AimAt<V, T1, T2>(this MotionBehaviour<V> self, T1 destination, T2 speed)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimAt(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimAt(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimAt(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimAt(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimRatioAt<V, T1, T2>(this MotionBehaviour<V> self, T1 destination, T2 ratio)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimRatioAt(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimRatioAt(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimRatioAt(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimRatioAt(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimSpringAt<V, T1, T2, T3>(this MotionBehaviour<V> self, T1 destination, T2 ratio, T3 bounce)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimSpringAt(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), Syntax.AsEnumerator<float, T3>(bounce), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimSpringAt(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), Syntax.AsEnumerator<float, T3>(bounce), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimSpringAt(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), Syntax.AsEnumerator<float, T3>(bounce), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimSpringAt(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<float, T2>(ratio), Syntax.AsEnumerator<float, T3>(bounce), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimExpoAt<V, T1, T2>(this MotionBehaviour<V> self, T1 destination, T2 speed)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimExpoAt(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimExpoAt(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimExpoAt(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimExpoAt(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimCriticalDampingAt<V, T1, T2>(this MotionBehaviour<V> self, T1 destination, T2 speed)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimCriticalDampingAt(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimCriticalDampingAt(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimCriticalDampingAt(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimCriticalDampingAt(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<float, T2>(speed), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimVelocity<V, T1, T2>(this MotionBehaviour<V> self, T1 destination, T2 speed)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Aiming.AimVelocity(Syntax.AsEnumerator<float,   T1>(destination), Syntax.AsEnumerator<float,   T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimVelocity(Syntax.AsEnumerator<Vector2, T1>(destination), Syntax.AsEnumerator<Vector2, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimVelocity(Syntax.AsEnumerator<Vector3, T1>(destination), Syntax.AsEnumerator<Vector3, T2>(speed), e.ValueEnumerator)),
				(e) => e.Add(Aiming.AimVelocity(Syntax.AsEnumerator<Vector4, T1>(destination), Syntax.AsEnumerator<Vector4, T2>(speed), e.ValueEnumerator))
			);
			return self;
		}

		public static MotionBehaviour<V> AimAt<V>(this MotionBehaviour<V> self, Func<V> destination, Func<float> speed)
		{
			return AimAt<V>(self, destination, speed);
		}

		public static MotionBehaviour<V> AimRatioAt<V>(this MotionBehaviour<V> self, Func<V> destination, Func<float> ratio)
		{
			return AimRatioAt<V>(self, destination, ratio);
		}

		public static MotionBehaviour<V> AimSpringAt<V>(this MotionBehaviour<V> self, Func<V> destination, Func<float> ratio, Func<float> bounce)
		{
			return AimSpringAt<V>(self, destination, ratio, bounce);
		}

		public static MotionBehaviour<V> AimExpoAt<V>(this MotionBehaviour<V> self, Func<V> destination, Func<float> speed)
		{
			return AimExpoAt<V>(self, destination, speed);
		}

		public static MotionBehaviour<V> AimCriticalDampingAt<V>(this MotionBehaviour<V> self, Func<V> destination, Func<float> speed)
		{
			return AimCriticalDampingAt<V>(self, destination, speed);
		}

		public static MotionBehaviour<V> AimVelocity<V>(this MotionBehaviour<V> self, Func<V> destination, Func<V> speed)
		{
			return AimVelocity<V>(self, destination, speed);
		}
	}
}