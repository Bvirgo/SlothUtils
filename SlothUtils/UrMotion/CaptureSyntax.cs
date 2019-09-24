using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	using Syntax = FluentSyntax;
	using Cap = Capture;

	public static class CaptureSyntax
	{
		public static MotionBehaviour<V> Capture<V>(this MotionBehaviour<V> self, out IEnumerator<V> capture)
		{
			var res = new IEnumerator<V>[] { null };
			Syntax.Resolve<V>(self,
				(e) => e.Wrap((v) => Cap.Create(v, (IEnumerator<float  >[])(object)res)),
				(e) => e.Wrap((v) => Cap.Create(v, (IEnumerator<Vector2>[])(object)res)),
				(e) => e.Wrap((v) => Cap.Create(v, (IEnumerator<Vector3>[])(object)res)),
				(e) => e.Wrap((v) => Cap.Create(v, (IEnumerator<Vector4>[])(object)res))
			);
			capture = res[0];
			return self;
		}
	}
}