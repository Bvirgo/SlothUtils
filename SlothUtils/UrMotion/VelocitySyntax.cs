using System;
using UnityEngine;

namespace SlothMotion
{
	using Vel = Velocity;
	using Syntax = FluentSyntax;

	public static class VelocitySyntax
	{
		public static MotionBehaviour<V> Velocity<V, T>(this MotionBehaviour<V> self, T source)
		{
			self.Add(Syntax.AsEnumerator<V, T>(source));
			return self;
		}

		public static MotionBehaviour<V> Accel<V, T>(this MotionBehaviour<V> self, T accel)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Vel.Accel(Syntax.AsEnumerator<float,   T>(accel))),
				(e) => e.Add(Vel.Accel(Syntax.AsEnumerator<Vector2, T>(accel))),
				(e) => e.Add(Vel.Accel(Syntax.AsEnumerator<Vector3, T>(accel))),
				(e) => e.Add(Vel.Accel(Syntax.AsEnumerator<Vector4, T>(accel)))
			);
			return self;
		}

		public static MotionBehaviour<V> AccelByRatio<V, T>(this MotionBehaviour<V> self, V iv, T accel)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Add(Vel.AccelByRatio((float  )(object)iv, Syntax.AsEnumerator<float, T>(accel))),
				(e) => e.Add(Vel.AccelByRatio((Vector2)(object)iv, Syntax.AsEnumerator<float, T>(accel))),
				(e) => e.Add(Vel.AccelByRatio((Vector3)(object)iv, Syntax.AsEnumerator<float, T>(accel))),
				(e) => e.Add(Vel.AccelByRatio((Vector4)(object)iv, Syntax.AsEnumerator<float, T>(accel)))
			);
			return self;
		}

		public static MotionBehaviour<Vector2> Angle<T>(this MotionBehaviour<Vector2> self, T angle)
		{
			self.Wrap((v) => Vel.Angle(v, Syntax.AsEnumerator<float, T>(angle)));
			return self;
		}

		public static MotionBehaviour<Vector3> Angle<T>(this MotionBehaviour<Vector3> self, T angle)
		{
			self.Wrap((v) => Vel.Angle(v, Syntax.AsEnumerator<Vector3, T>(angle)));
			return self;
		}

		public static MotionBehaviour<V> Velocity<V>(this MotionBehaviour<V> self, Func<V> source)
		{
			return Velocity<V, Func<V>>(self, source);
		}

		public static MotionBehaviour<V> Accel<V>(this MotionBehaviour<V> self, Func<V> accel)
		{
			return Accel<V, Func<V>>(self, accel);
		}

		public static MotionBehaviour<V> AccelByRatio<V>(this MotionBehaviour<V> self, V iv, Func<V> accel)
		{
			return AccelByRatio<V, Func<V>>(self, iv, accel);
		}

		public static MotionBehaviour<Vector2> Angle(this MotionBehaviour<Vector2> self, Func<float> angle)
		{
			return Angle<Func<float>>(self, angle);
		}

		public static MotionBehaviour<Vector3> Angle(this MotionBehaviour<Vector3> self, Func<Vector3> angle)
		{
			return Angle<Func<Vector3>>(self, angle);
		}
	}
}