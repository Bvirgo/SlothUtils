using UnityEngine;

namespace SlothMotion
{
	public abstract class MotionVec2<T> : MotionBehaviour<Vector2> where T : MotionBehaviour<Vector2>
	{
		public static MotionBehaviour<Vector2> Add(GameObject g)
		{
			return Get<T>(g);
		}

		protected override Vector2 Add(Vector2 a, Vector2 b)
		{
			return a + b;
		}
	}

	public abstract class MotionVec2T<T> : MotionVec2<T> where T : MotionBehaviour<Vector2>
	{
		public abstract Vector3 vector { get; set; }

		Transform t;

		protected Transform GetTransform()
		{
			return t ?? (t = transform);
		}
	}

	public abstract class MotionVec2P<T> : MotionVec2T<T> where T : MotionBehaviour<Vector2>
	{
		public override Vector3 vector {
			get {
				return GetTransform().localPosition;
			}
			set {
				GetTransform().localPosition = value;
			}
		}
	}

	public abstract class MotionVec2WP<T> : MotionVec2T<T> where T : MotionBehaviour<Vector2>
	{
		public override Vector3 vector {
			get {
				return GetTransform().position;
			}
			set {
				GetTransform().position = value;
			}
		}
	}

	public abstract class MotionVec2R<T> : MotionVec2T<T> where T : MotionBehaviour<Vector2>
	{
		public override Vector3 vector {
			get {
				return GetTransform().localEulerAngles;
			}
			set {
				GetTransform().localEulerAngles = value;
			}
		}
	}

	public abstract class MotionVec2WR<T> : MotionVec2T<T> where T : MotionBehaviour<Vector2>
	{
		public override Vector3 vector {
			get {
				return GetTransform().eulerAngles;
			}
			set {
				GetTransform().eulerAngles = value;
			}
		}
	}

	public abstract class MotionVec2S<T> : MotionVec2T<T> where T : MotionBehaviour<Vector2>
	{
		public override Vector3 vector {
			get {
				return GetTransform().localScale;
			}
			set {
				GetTransform().localScale = value;
			}
		}
	}
}