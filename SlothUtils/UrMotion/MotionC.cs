using UnityEngine;

namespace SlothMotion
{
	public class MotionA : MotionVec1C<MotionA>
	{
		protected override float value {
			get {
				return col.a;
			}
			set {
				var c = col;
				c.a = value;
				col = c;
			}
		}
	}

	public class MotionC : MotionVec3C<MotionC>
	{
		protected override Vector3 value {
			get {
				return new Vector3(col.r, col.g, col.b);
			}
			set {
				var c = col;
				c.r = value.x;
				c.g = value.y;
				c.b = value.z;
				col = c;
			}
		}
	}

	public class MotionCA : MotionVec4C<MotionCA>
	{
		protected override Vector4 value {
			get {
				return (Vector4)col;
			}
			set {
				col = (Color)value;
			}
		}
	}
}