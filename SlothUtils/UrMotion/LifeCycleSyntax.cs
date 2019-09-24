using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	using Syntax = FluentSyntax;

	public static class LifeCycleSyntax
	{
		public static MotionBehaviour<V> Lifetime<V, T>(this MotionBehaviour<V> self, T period)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.Lifetime(v, Syntax.AsEnumerator<float, T>(period))),
				(e) => e.Wrap((v) => LifeCycle.Lifetime(v, Syntax.AsEnumerator<float, T>(period))),
				(e) => e.Wrap((v) => LifeCycle.Lifetime(v, Syntax.AsEnumerator<float, T>(period))),
				(e) => e.Wrap((v) => LifeCycle.Lifetime(v, Syntax.AsEnumerator<float, T>(period)))
			);
			return self;
		}

		public static MotionBehaviour<V> StartDelay<V, T>(this MotionBehaviour<V> self, T delay)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.Delay(v, Syntax.AsEnumerator<float, T>(delay))),
				(e) => e.Wrap((v) => LifeCycle.Delay(v, Syntax.AsEnumerator<float, T>(delay))),
				(e) => e.Wrap((v) => LifeCycle.Delay(v, Syntax.AsEnumerator<float, T>(delay))),
				(e) => e.Wrap((v) => LifeCycle.Delay(v, Syntax.AsEnumerator<float, T>(delay)))
			);
			return self;
		}

		public static MotionBehaviour<V> LiveThreshold<V, T>(this MotionBehaviour<V> self, T threshold)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold))),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold))),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold))),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold)))
			);
			return self;
		}

		public static MotionBehaviour<V> LiveThreshold<V, T>(this MotionBehaviour<V> self, T threshold, Func<float, float, bool> comparator)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold), comparator)),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold), comparator)),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold), comparator)),
				(e) => e.Wrap((v) => LifeCycle.LiveThreshold(v, Syntax.AsEnumerator<float, T>(threshold), comparator))
			);
			return self;
		}

		public static MotionBehaviour<V> LiveUntil<V>(this MotionBehaviour<V> self, Func<V, bool> condition)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.LiveUntil(v as IEnumerator<float  >, condition as Func<float,   bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveUntil(v as IEnumerator<Vector2>, condition as Func<Vector2, bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveUntil(v as IEnumerator<Vector3>, condition as Func<Vector3, bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveUntil(v as IEnumerator<Vector4>, condition as Func<Vector4, bool>))
			);
			return self;
		}

		public static MotionBehaviour<V> LiveWhile<V>(this MotionBehaviour<V> self, Func<V, bool> condition)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => LifeCycle.LiveWhile(v as IEnumerator<float  >, condition as Func<float,   bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveWhile(v as IEnumerator<Vector2>, condition as Func<Vector2, bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveWhile(v as IEnumerator<Vector3>, condition as Func<Vector3, bool>)),
				(e) => e.Wrap((v) => LifeCycle.LiveWhile(v as IEnumerator<Vector4>, condition as Func<Vector4, bool>))
			);
			return self;
		}

		public static MotionBehaviour<V> Lifetime<V>(this MotionBehaviour<V> self, Func<float> period)
		{
			return Lifetime<V, Func<float>>(self, period);
		}

		public static MotionBehaviour<V> StartDelay<V>(this MotionBehaviour<V> self, Func<float> delay)
		{
			return StartDelay<V, Func<float>>(self, delay);
		}

		public static MotionBehaviour<V> LiveThreshold<V>(this MotionBehaviour<V> self)
		{
			return LiveThreshold<V, float>(self, 0.01f);
		}

		public static MotionBehaviour<V> LiveThreshold<V>(this MotionBehaviour<V> self, Func<V> threshold)
		{
			return LiveThreshold<V, Func<V>>(self, threshold);
		}

		public static MotionBehaviour<V> LiveThreshold<V>(this MotionBehaviour<V> self, Func<V> threshold, Func<float, float, bool> comparator)
		{
			return LiveThreshold<V, Func<V>>(self, threshold, comparator);
		}
	}
}