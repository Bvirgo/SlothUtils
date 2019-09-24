using UnityEngine;

namespace SlothMotion
{
	using MotionBehaviourVec1 = MotionBehaviour<float>;
	using MotionBehaviourVec2 = MotionBehaviour<Vector2>;
	using MotionBehaviourVec3 = MotionBehaviour<Vector3>;

	public static class MotionPSyntax
	{
		public static MotionBehaviourVec1 MotionX(this GameObject g)
		{
			return SlothMotion.MotionX.Add(g);
		}

		public static MotionBehaviourVec1 MotionY(this GameObject g)
		{
			return SlothMotion.MotionY.Add(g);
		}

		public static MotionBehaviourVec1 MotionZ(this GameObject g)
		{
			return SlothMotion.MotionZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionXY(this GameObject g)
		{
			return SlothMotion.MotionXY.Add(g);
		}

		public static MotionBehaviourVec2 MotionXZ(this GameObject g)
		{
			return SlothMotion.MotionXZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionYZ(this GameObject g)
		{
			return SlothMotion.MotionYZ.Add(g);
		}

		public static MotionBehaviourVec3 MotionXYZ(this GameObject g)
		{
			return SlothMotion.MotionXYZ.Add(g);
		}

		public static MotionBehaviourVec1 MotionWX(this GameObject g)
		{
			return SlothMotion.MotionWX.Add(g);
		}

		public static MotionBehaviourVec1 MotionWY(this GameObject g)
		{
			return SlothMotion.MotionWY.Add(g);
		}

		public static MotionBehaviourVec1 MotionWZ(this GameObject g)
		{
			return SlothMotion.MotionWZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionWXY(this GameObject g)
		{
			return SlothMotion.MotionWXY.Add(g);
		}

		public static MotionBehaviourVec2 MotionWXZ(this GameObject g)
		{
			return SlothMotion.MotionWXZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionWYZ(this GameObject g)
		{
			return SlothMotion.MotionWYZ.Add(g);
		}

		public static MotionBehaviourVec3 MotionWXYZ(this GameObject g)
		{
			return SlothMotion.MotionWXYZ.Add(g);
		}

		public static MotionBehaviourVec2 MotionP(this GameObject g)
		{
			return SlothMotion.MotionP.Add(g);
		}

		public static MotionBehaviourVec3 MotionP3(this GameObject g)
		{
			return SlothMotion.MotionP3.Add(g);
		}

		public static MotionBehaviourVec2 MotionWP(this GameObject g)
		{
			return SlothMotion.MotionWP.Add(g);
		}

		public static MotionBehaviourVec3 MotionWP3(this GameObject g)
		{
			return SlothMotion.MotionWP3.Add(g);
		}
	}
}