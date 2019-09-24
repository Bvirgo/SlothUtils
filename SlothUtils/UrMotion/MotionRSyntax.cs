using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;

	public static class MotionRSyntax
	{
		public static MotionBehaviourVec1 MotionRX(this GameObject g)
		{
			return SlothMotion.MotionRX.Add(g);
		}

		public static MotionBehaviourVec1 MotionRY(this GameObject g)
		{
			return SlothMotion.MotionRY.Add(g);
		}

		public static MotionBehaviourVec1 MotionRZ(this GameObject g)
		{
			return SlothMotion.MotionRZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionRXY(this GameObject g)
		{
			return SlothMotion.MotionRXY.Add(g);
		}

		public static MotionBehaviourVec2 MotionRXZ(this GameObject g)
		{
			return SlothMotion.MotionRXZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionRYZ(this GameObject g)
		{
			return SlothMotion.MotionRYZ.Add(g);
		}

		public static MotionBehaviourVec3 MotionRXYZ(this GameObject g)
		{
			return SlothMotion.MotionRXYZ.Add(g);
		}

		public static MotionBehaviourVec1 MotionWRX(this GameObject g)
		{
			return SlothMotion.MotionWRX.Add(g);
		}

		public static MotionBehaviourVec1 MotionWRY(this GameObject g)
		{
			return SlothMotion.MotionWRY.Add(g);
		}

		public static MotionBehaviourVec1 MotionWRZ(this GameObject g)
		{
			return SlothMotion.MotionWRZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionWRXY(this GameObject g)
		{
			return SlothMotion.MotionWRXY.Add(g);
		}

		public static MotionBehaviourVec2 MotionWRXZ(this GameObject g)
		{
			return SlothMotion.MotionWRXZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionWRYZ(this GameObject g)
		{
			return SlothMotion.MotionWRYZ.Add(g);
		}

		public static MotionBehaviourVec3 MotionWRXYZ(this GameObject g)
		{
			return SlothMotion.MotionWRXYZ.Add(g);
		}

		public static MotionBehaviourVec1 MotionR(this GameObject g)
		{
			return SlothMotion.MotionR.Add(g);
		}

		public static MotionBehaviourVec3 MotionR3(this GameObject g)
		{
			return SlothMotion.MotionR3.Add(g);
		}

		public static MotionBehaviourVec1 MotionWR(this GameObject g)
		{
			return SlothMotion.MotionWR.Add(g);
		}

		public static MotionBehaviourVec3 MotionWR3(this GameObject g)
		{
			return SlothMotion.MotionWR3.Add(g);
		}
	}
}