using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;
	using MotionBehaviourVec4 = MotionBehaviour<Vector4>;

	public static class MotionCSyntax
	{
		public static MotionBehaviourVec1 MotionA(this GameObject g)
		{
			return SlothMotion.MotionA.Add(g);
		}

		public static MotionBehaviourVec3 MotionC(this GameObject g)
		{
			return SlothMotion.MotionC.Add(g);
		}

		public static MotionBehaviourVec4 MotionCA(this GameObject g)
		{
			return SlothMotion.MotionCA.Add(g);
		}
	}
}