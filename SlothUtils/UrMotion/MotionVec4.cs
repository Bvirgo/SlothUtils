using UnityEngine;
using UnityEngine.UI;

namespace SlothMotion
{
	public abstract class MotionVec4<T> : MotionBehaviour<Vector4> where T : MotionBehaviour<Vector4>
	{
		public static MotionBehaviour<Vector4> Add(GameObject g)
		{
			return Get<T>(g);
		}

		protected override Vector4 Add(Vector4 a, Vector4 b)
		{
			return a + b;
		}
	}

	public abstract class MotionVec4G<T> : MotionVec4<T> where T : MotionBehaviour<Vector4>
	{
		Graphic g;

		protected Graphic GetGraphic()
		{
			return g ?? (g = GetComponent<Graphic>());
		}
	}

	public abstract class MotionVec4C<T> : MotionVec4G<T> where T : MotionBehaviour<Vector4>
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

	public abstract class MotionVec4M<T> : MotionVec4<T> where T : MotionBehaviour<Vector4>
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

	public abstract class MotionVec4MC<T> : MotionVec4M<T> where T : MotionBehaviour<Vector4>
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