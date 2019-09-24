using UnityEngine;

namespace SlothMotion
{
	public class MotionSX : MotionVec1S<MotionSX>
	{
		protected override float value {
			get {
				return vector.x;
			}
			set {
				var v = vector;
				v.x = value;
				vector = v;
			}
		}
	}

	public class MotionSY : MotionVec1S<MotionSY>
	{
		protected override float value {
			get {
				return vector.y;
			}
			set {
				var v = vector;
				v.y = value;
				vector = v;
			}
		}
	}

	public class MotionSZ : MotionVec1S<MotionSZ>
	{
		protected override float value {
			get {
				return vector.z;
			}
			set {
				var v = vector;
				v.z = value;
				vector = v;
			}
		}
	}

	public class MotionSXY : MotionVec2S<MotionSXY>
	{
		override protected Vector2 value {
			get {
				return new Vector2(vector.x, vector.y);
			}
			set {
				Vector3 v = vector;
				v.x = value.x;
				v.y = value.y;
				vector = v;
			}
		}
	}

	public class MotionSXZ : MotionVec2S<MotionSXZ>
	{
		protected override Vector2 value {
			get {
				return new Vector2(vector.x, vector.z);
			}
			set {
				var v = vector;
				v.x = value.x;
				v.z = value.y;
				vector = v;
			}
		}
	}

	public class MotionSYZ : MotionVec2S<MotionSYZ>
	{
		protected override Vector2 value {
			get {
				return new Vector2(vector.y, vector.z);
			}
			set {
				var v = vector;
				v.y = value.x;
				v.z = value.y;
				vector = v;
			}
		}
	}

	public class MotionSXYZ : MotionVec3S<MotionSXYZ>
	{
		protected override Vector3 value {
			get {
				return vector;
			}
			set {
				vector = value;
			}
		}
	}

	public class MotionS : MotionSXY
	{
	}
	public class MotionS3 : MotionSXYZ
	{
	}
}