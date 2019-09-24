using UnityEngine;
using UnityEngine.UI;

namespace SlothMotion
{
	public abstract class MotionVec1<T> : MotionBehaviour<float> where T : MotionBehaviour<float>
	{
		public static MotionBehaviour<float> Add(GameObject g)
		{
			return Get<T>(g);
		}

		protected override float Add(float a, float b)
		{
			return a + b;
		}
	}

	public abstract class MotionVec1T<T> : MotionVec1<T> where T : MotionBehaviour<float>
	{
		public abstract Vector3 vector { get; set; }

		Transform t;

		protected Transform GetTransform()
		{
			return t ?? (t = transform);
		}
	}

	public abstract class MotionVec1P<T> : MotionVec1T<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1WP<T> : MotionVec1T<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1R<T> : MotionVec1T<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1WR<T> : MotionVec1T<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1S<T> : MotionVec1T<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1G<T> : MotionVec1<T> where T : MotionBehaviour<float>
	{
		Graphic g;

		protected Graphic GetGraphic()
		{
			return g ?? (g = GetComponent<Graphic>());
		}
	}

	public abstract class MotionVec1C<T> : MotionVec1G<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1M<T> : MotionVec1<T> where T : MotionBehaviour<float>
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

	public abstract class MotionVec1MC<T> : MotionVec1M<T> where T : MotionBehaviour<float>
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