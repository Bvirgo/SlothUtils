using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlothMotion
{
	public static class Source
	{
		public static float DefaultFrameRate = 60f;

		public static void ValidateFrameRate(ref float fps)
		{
			if (fps == 0f) {
				fps = DefaultFrameRate;
			}
		}

		public class SourceDimension<V> {}
		public static readonly SourceDimension<float> Float = new SourceDimension<float>();
		public static readonly SourceDimension<float> Single = new SourceDimension<float>();
		public static readonly SourceDimension<Vector2> Vector2 = new SourceDimension<Vector2>();
		public static readonly SourceDimension<Vector3> Vector3 = new SourceDimension<Vector3>();
		public static readonly SourceDimension<Vector4> Vector4 = new SourceDimension<Vector4>();

		public static IEnumerator<V> Constant<V>(V val)
		{
			for (;;) {
				yield return val;
			}
		}

		public static IEnumerator<V> Function<V>(Func<V> f)
		{
			for (;;) {
				yield return f();
			}
		}

		public static IEnumerator<float> Sin(IEnumerator<float> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Sin(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector2> Sin(IEnumerator<Vector2> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Sin(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector3> Sin(IEnumerator<Vector3> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Sin(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector4> Sin(IEnumerator<Vector4> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Sin(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<float> Cos(IEnumerator<float> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Cos(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector2> Cos(IEnumerator<Vector2> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Cos(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector3> Cos(IEnumerator<Vector3> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Cos(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector4> Cos(IEnumerator<Vector4> radius, IEnumerator<float> freq, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && freq.MoveNext()) {
				yield return Mathf.Cos(angle) * radius.Current;
				angle += (1.0f / fps) * Mathf.PI * 2f * freq.Current;
			}
		}

		public static IEnumerator<Vector2> Circular(IEnumerator<float> radius, IEnumerator<float> speed, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (radius.MoveNext() && speed.MoveNext()) {
				var x = Mathf.Cos(angle) * radius.Current;
				var y = Mathf.Sin(angle) * radius.Current;
				yield return new Vector2(x, y);
				angle += (1.0f / fps) * Mathf.PI * 2f * speed.Current;
			}
		}

		public static IEnumerator<Vector2> Lissajous(IEnumerator<float> A, IEnumerator<float> B, IEnumerator<float> a, IEnumerator<float> b, float delta, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angleA = 0f;
			var angleB = 0f;
			while (A.MoveNext() && B.MoveNext() && a.MoveNext() && b.MoveNext()) {
				var x = A.Current * Mathf.Cos(angleA);
				var y = B.Current * Mathf.Sin(angleB + delta);
				yield return new Vector2(x, y);
				angleA += (1.0f / fps) * Mathf.PI * 2f * a.Current;
				angleB += (1.0f / fps) * Mathf.PI * 2f * b.Current;
			}
		}

		public static IEnumerator<Vector2> Cycloid(IEnumerator<float> A, IEnumerator<float> B, float rm, IEnumerator<float> speed, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (A.MoveNext() && B.MoveNext() && speed.MoveNext()) {
				var x = A.Current * (rm * (angle - Mathf.Sin(angle)));
				var y = B.Current * (rm * (1f - Mathf.Cos(angle)));
				yield return new Vector2(x, y);
				angle += (1.0f / fps) * Mathf.PI * 2f * speed.Current;
			}
		}

		public static IEnumerator<Vector2> Epicycloid(IEnumerator<float> A, IEnumerator<float> B, float rc, float rm, IEnumerator<float> speed, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (A.MoveNext() && B.MoveNext() && speed.MoveNext()) {
				var x = A.Current * ((rc + rm) * Mathf.Cos(angle) - rm * Mathf.Cos((rc + rm) / rm * angle));
				var y = B.Current * ((rc + rm) * Mathf.Sin(angle) - rm * Mathf.Sin((rc + rm) / rm * angle));
				yield return new Vector2(x, y);
				angle += (1.0f / fps) * Mathf.PI * 2f * speed.Current;
			}
		}

		public static IEnumerator<Vector2> Hypocycloid(IEnumerator<float> A, IEnumerator<float> B, float rc, float rm, IEnumerator<float> speed, float fps = 0f)
		{
			ValidateFrameRate(ref fps);
			var angle = 0f;
			while (A.MoveNext() && B.MoveNext() && speed.MoveNext()) {
				var x = A.Current * ((rc - rm) * Mathf.Cos(angle) + rm * Mathf.Cos((rc - rm) / rm * angle));
				var y = B.Current * ((rc - rm) * Mathf.Sin(angle) - rm * Mathf.Sin((rc - rm) / rm * angle));
				yield return new Vector2(x, y);
				angle += (1.0f / fps) * Mathf.PI * 2f * speed.Current;
			}
		}
	}
}