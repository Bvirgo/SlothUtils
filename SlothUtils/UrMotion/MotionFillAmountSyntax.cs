using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;
	using MotionBehaviourVec4 = MotionBehaviour<Vector4>;

	public static class MotionFillAmountSyntax
	{
		public static MotionBehaviourVec1 MotionFillAmount(this GameObject g)
		{
			return SlothMotion.MotionFillAmount.Add(g);
		}
	}
}