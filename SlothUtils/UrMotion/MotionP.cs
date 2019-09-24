using UnityEngine;

namespace SlothMotion
{
	public class MotionX : MotionVec1P<MotionX>
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

	public class MotionY : MotionVec1P<MotionY>
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

	public class MotionZ : MotionVec1P<MotionZ>
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

	public class MotionXY : MotionVec2P<MotionXY>
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

	public class MotionXZ : MotionVec2P<MotionXZ>
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

	public class MotionYZ : MotionVec2P<MotionYZ>
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

	public class MotionXYZ : MotionVec3P<MotionXYZ>
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

	public class MotionWX : MotionVec1WP<MotionWX>
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

	public class MotionWY : MotionVec1WP<MotionWY>
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

	public class MotionWZ : MotionVec1WP<MotionWZ>
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

	public class MotionWXY : MotionVec2WP<MotionWXY>
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

	public class MotionWXZ : MotionVec2WP<MotionWXZ>
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

	public class MotionWYZ : MotionVec2WP<MotionWYZ>
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

	public class MotionWXYZ : MotionVec3WP<MotionWXYZ>
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

	public class MotionP : MotionXY
	{
	}
	public class MotionP3 : MotionXYZ
	{
	}
	public class MotionWP : MotionWXY
	{
	}
	public class MotionWP3 : MotionWXYZ
	{
	}
}