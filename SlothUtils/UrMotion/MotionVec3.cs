using UnityEngine;
using UnityEngine.UI;

namespace SlothMotion
{
	public abstract class MotionVec3<T> : MotionBehaviour<Vector3> where T : MotionBehaviour<Vector3>
	{
		public static MotionBehaviour<Vector3> Add(GameObject g)
		{
			return Get<T>(g);
		}

		protected override Vector3 Add(Vector3 a, Vector3 b)
		{
			return a + b;
		}
	}

	public abstract class MotionVec3T<T> : MotionVec3<T> where T : MotionBehaviour<Vector3>
	{
		public abstract Vector3 vector { get; set; }

		Transform t;

		protected Transform GetTransform()
		{
			return t ?? (t = transform);
		}
	}

	public abstract class MotionVec3P<T> : MotionVec3T<T> where T : MotionBehaviour<Vector3>
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

	public abstract class MotionVec3WP<T> : MotionVec3T<T> where T : MotionBehaviour<Vector3>
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

	public abstract class MotionVec3R<T> : MotionVec3T<T> where T : MotionBehaviour<Vector3>
	{
		override public Vector3 vector {
			get {
				return GetTransform().localEulerAngles;
			}
			set {
				GetTransform().localEulerAngles = value;
			}
		}
	}

	public abstract class MotionVec3WR<T> : MotionVec3T<T> where T : MotionBehaviour<Vector3>
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

	public abstract class MotionVec3S<T> : MotionVec3T<T> where T : MotionBehaviour<Vector3>
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

	public abstract class MotionVec3G<T> : MotionVec3<T> where T : MotionBehaviour<Vector3>
	{
		Graphic g;

		protected Graphic GetGraphic()
		{
			return g ?? (g = GetComponent<Graphic>());
		}
	}

	public abstract class MotionVec3C<T> : MotionVec3G<T> where T : MotionBehaviour<Vector3>
	{
		protected Color col {
			get {
				return GetGraphic().color;
			}
			set {
				GetGraphic().color = value;
			}
		}
	}

	public abstract class MotionVec3M<T> : MotionVec3<T> where T : MotionBehaviour<Vector3>
	{
		Renderer r;

		protected Renderer GetRenderer()
		{
			return r ?? (r = GetComponent<Renderer>());
		}

		protected Material mat {
			get {
				return GetRenderer().material;
			}
			set {
				GetRenderer().material = value;
			}
		}
	}

	public abstract class MotionVec3MC<T> : MotionVec3M<T> where T : MotionBehaviour<Vector3>
	{
		protected Color col {
			get {
				return mat.color;
			}
			set {
				var m = mat;
				m.color = value;
				mat = m;
			}
		}
	}
}