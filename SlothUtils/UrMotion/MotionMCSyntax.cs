using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;
	using MotionBehaviourVec4 = MotionBehaviour<Vector4>;

	public static class MotionMCSyntax
	{
		public static MotionBehaviourVec1 MotionMA(this GameObject g)
		{
			return SlothMotion.MotionMA.Add(g);
		}

		public static MotionBehaviourVec3 MotionMC(this GameObject g)
		{
			return SlothMotion.MotionMC.Add(g);
		}

		public static MotionBehaviourVec4 MotionMCA(this GameObject g)
		{
			return SlothMotion.MotionMCA.Add(g);
		}
	}
}