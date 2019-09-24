using UnityEngine;

namespace SlothMotion
{
	public class MotionRX : MotionVec1R<MotionRX>
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

	public class MotionRY : MotionVec1R<MotionRY>
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

	public class MotionRZ : MotionVec1R<MotionRZ>
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

	public class MotionRXY : MotionVec2R<MotionRXY>
	{
		protected override Vector2 value {
			get {
				return new Vector2(vector.x, vector.y);
			}
			set {
				var v = vector;
				v.x = value.x;
				v.y = value.y;
				vector = v;
			}
		}
	}

	public class MotionRXZ : MotionVec2R<MotionRXZ>
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

	public class MotionRYZ : MotionVec2R<MotionRYZ>
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

	public class MotionRXYZ : MotionVec3R<MotionRXYZ>
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

	public class MotionWRX : MotionVec1WR<MotionWRX>
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

	public class MotionWRY : MotionVec1WR<MotionWRY>
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

	public class MotionWRZ : MotionVec1WR<MotionWRZ>
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

	public class MotionWRXY : MotionVec2WR<MotionWRXY>
	{
		protected override Vector2 value {
			get {
				return new Vector2(vector.x, vector.y);
			}
			set {
				var v = vector;
				v.x = value.x;
				v.y = value.y;
				vector = v;
			}
		}
	}

	public class MotionWRXZ : MotionVec2WR<MotionWRXZ>
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

	public class MotionWRYZ : MotionVec2WR<MotionWRYZ>
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

	public class MotionWRXYZ : MotionVec3WR<MotionWRXYZ>
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

	public class MotionR : MotionRZ
	{
	}
	public class MotionR3 : MotionRXYZ
	{
	}
	public class MotionWR : MotionWRZ
	{
	}
	public class MotionWR3 : MotionWRXYZ
	{
	}
}