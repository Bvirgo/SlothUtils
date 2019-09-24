using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;

	public static class MotionSPSyntax
	{
		public static MotionBehaviourVec1 MotionSX(this GameObject g)
		{
			return SlothMotion.MotionSX.Add(g);
		}

		public static MotionBehaviourVec1 MotionSY(this GameObject g)
		{
			return SlothMotion.MotionSY.Add(g);
		}

		public static MotionBehaviourVec1 MotionSZ(this GameObject g)
		{
			return SlothMotion.MotionSZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionSXY(this GameObject g)
		{
			return SlothMotion.MotionSXY.Add(g);
		}

		public static MotionBehaviourVec2 MotionSXZ(this GameObject g)
		{
			return SlothMotion.MotionSXZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionSYZ(this GameObject g)
		{
			return SlothMotion.MotionSYZ.Add(g);
		}

		public static MotionBehaviourVec3 MotionSXYZ(this GameObject g)
		{
			return SlothMotion.MotionSXYZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionS(this GameObject g)
		{
			return SlothMotion.MotionS.Add(g);
		}

		public static MotionBehaviourVec3 MotionS3(this GameObject g)
		{
			return SlothMotion.MotionS3.Add(g);
		}
	}
}