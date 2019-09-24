using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SlothMotion;

/// <summary>
/// Motion使用案例
/// 链接：https://github.com/beinteractive/UrMotion
/// </summary>
public class Example : MonoBehaviour
{
	IEnumerator Start()
	{
		yield return new WaitForSeconds(0.3f);

		var g = transform.Find("Image").gameObject;

		// Uniform move
		g.MotionX().Velocity(3f);

		// Uniform move (Moving Y)
		// g.MotionY().Velocity(3f);

		// Uniform move (Moving X & Y)
		// g.MotionP().Velocity(new Vector2(3f, 2f));

		// Accel move
		// g.MotionX().Accel(0.3f);

		// Accel move with initial speed
		// g.MotionX().Velocity(1f).Accel(0.3f);

		// Accel move with initial speed
		// g.MotionX().Velocity(-6f).Accel(0.3f);

		// Accel by ratio
		// g.MotionX().AccelByRatio(10f, 0.9f);

		// Sin move
		// g.MotionX().Sin(83f, 0.5f);

		// Circular move
		// g.MotionP().Circular(83f, 0.5f);

		// Lissajous move
		// g.MotionP().Lissajous(83f, 51f, 0.6f, 1.2f, 0f);

		/**
		for (;;) {

			var p = new Vector2(Random.Range(-300f, 300f), Random.Range(-160f, 160f));

			// Aiming with uniform move
			// g.MotionP().AimAt(p, 10f);

			// Aiming with common ratio
			// g.MotionP().AimRatioAt(p, 0.15f);

			// Aiming with spring move
			g.MotionP().AimSpringAt(p, 0.15f, 0.8f);

			// Aiming with exponential interpolation
			// g.MotionP().AimExpoAt(p, 0.15f);

			// Aiming with critically damped spring smoothing
			// g.MotionP().AimCriticalDampingAt(p, 0.15f);

			yield return new WaitForSeconds(0.8f);
		}
		/**/

		// Velocity by perlin noise
		// g.MotionP().Perlin(new Vector2(0.4f, 0.8f)).AmplifyComponents(new Vector2(3f, 2f));

		// Fractional brownian motion
		// g.MotionP().Fbm(new Vector2(0.4f, 0.8f), 3).AmplifyComponents(new Vector2(3f, 2f));

		// Change velocity by time with sin curve
		// g.MotionX().Velocity(Source.Float.Sin(2f, 1f).Offset(2f));

		// Change radius by time with sin curve
		// g.MotionX().Sin(Source.Float.Sin(51f, 0.5f).Offset(51f), 1f);

		// Custom parameter function
		// g.MotionX().Velocity(() => Random.Range(-10f, 10f));

		// Finish velocity effect after 15fr.
		// g.MotionX().Accel(0.3f).Lifetime(15f);

		// Start velocity effect after 15fr.
		// g.MotionX().Accel(0.3f).Delay(15f);

		// Finish velocity effect if it is less than 0.01f and then destroy a GameObject
		// g.MotionX().AccelByRatio(10f, 0.9f).LiveThreshold(0.01f).Then(() => Destroy(g));

		// Complex motions

		// Parabola
		/**
		g.MotionX().Velocity(6f);
		g.MotionY().Velocity(18f).Accel(-0.98f);
		/**/

		// Spiral
		// g.MotionP().Circular(Source.Float.Sin(51f, 0.5f).Offset(51f), 2f);

		// Lissajous + Lissajous
		// g.MotionP().Lissajous(83f, 51f, 0.6f, 1.2f, 0f).Lissajous(24f, 32f, 2.4f, 0.8f, 0f);

		// Lissajous with directon
		/**
		UseImage2(g);
		var vel = default(IEnumerator<Vector2>);
		g.MotionP().Lissajous(83f, 51f, 0.6f, 1.2f, 0f).Capture(out vel);
		g.MotionR().AimRatioAt(vel.ToAngle().Offset(-90f), 1f);
		/**/

		// Floating scaling
		// g.MotionS().AccelByRatio(Vector2.one * 0.4f, 0.85f).Sin(Vector2.one * 0.5f, 0.5f);

		// Aiming + Circular move
		/**
		for (;;) {

			var p = new Vector2(Random.Range(-300f, 300f), Random.Range(-160f, 160f));

			var vel = default(IEnumerator<Vector2>);
			var m = g.MotionP();
			m.AimSpringAt(p, 0.1f, 0.45f).Capture(out vel);
			m.Circular(vel.Magnitude().Amplify(2f), 2f);

			yield return new WaitForSeconds(0.8f);
		}
		/**/

		// Scaling by velocity
		/**
		for (;;) {

			var p = new Vector2(Random.Range(-300f, 300f), Random.Range(-160f, 160f));

			var vel = default(IEnumerator<Vector2>);
			g.MotionP().AimExpoAt(p, 0.15f).Capture(out vel);
			g.MotionS().AimSpringAt(vel.Magnitude().Amplify(0.075f).Offset(1f).ToVector2(), 0.12f, 0.7f);

			yield return new WaitForSeconds(0.8f);
		}
		/**/

		// Circular + Noise
		// g.MotionP().Circular(83f, 0.25f).Fbm(new Vector2(2f, 3f), 3).Amplify(6f);

		// Follow mouse position
		/**
		System.Func<Vector2> p = () => GetMousePosition();
		g.MotionP().AimCriticalDampingAt(p, 0.8f);
		/**/

		// Follow + circular move
		/**
		System.Func<Vector2> p = () => GetMousePosition();
		g.MotionP().AimCriticalDampingAt(p, 0.8f).Circular(83f, 1.5f);
		/**/

		// Follow * 3
		/**
		var f1 = GameObject.Instantiate(g); f1.transform.SetParent(g.transform.parent); f1.transform.position = g.transform.position;
		var f2 = GameObject.Instantiate(g); f2.transform.SetParent(g.transform.parent); f2.transform.position = g.transform.position;
		f1.transform.localScale = Vector3.one * 0.9f;
		f2.transform.localScale = Vector3.one * 0.9f * 0.9f;
		for (;;) {

			var p = new Vector2(Random.Range(-300f, 300f), Random.Range(-160f, 160f));

			System.Func<Vector2> gp = () => new Vector2(g.transform.localPosition.x, g.transform.localPosition.y);
			g.MotionP().AimCriticalDampingAt(p, 0.8f);
			f1.MotionP().AimCriticalDampingAt(gp, 0.3f).Delay(6f).AccelByRatio(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 20f, 0.9f);
			f2.MotionP().AimCriticalDampingAt(gp, 0.2f).Delay(9f).AccelByRatio(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 15f, 0.9f);

			yield return new WaitForSeconds(0.8f);
		}
		/**/

		// Aiming + Noise
		/**
		for (;;) {

			var p = new Vector2(Random.Range(-300f, 300f), Random.Range(-160f, 160f));

			var vel = default(IEnumerator<Vector2>);
			var m = g.MotionP();
			m.AimSpringAt(p, 0.1f, 0.45f).Capture(out vel);
			m.Perlin(new Vector2(7f, 11f)).Amplify(vel.Magnitude().Amplify(1.2f));

			yield return new WaitForSeconds(0.8f);
		}
		/**/

		// Spiral #2
		/**
		var prefab = g;
		for (var i = 0; i < 12; ++i) {
			g = GameObject.Instantiate(prefab);
			g.transform.SetParent(prefab.transform.parent);
			g.transform.localPosition = Vector3.zero;
			g.transform.localScale = Vector3.one;

			var angle = 30f * i;
			var radius = Velocity.AccelByRatio(218f, Source.Constant(0.92f)).Offset(83f);
			var speed = Velocity.AccelByRatio(0.75f, Source.Constant(0.94f)).Offset(0.01f);
			g.MotionP().Circular(radius, speed).Angle(angle).Fbm(new Vector2(0f, 1f), 3).AmplifyComponents(new Vector2(0f, 0.3f));
		}
		Destroy(prefab);
		/**/
	}

	void UseImage2(ref GameObject g)
	{
		g.SetActive(false);
		g = transform.Find("Image_2").gameObject;
		g.SetActive(true);
	}

	Vector2 GetMousePosition()
	{
		var canvas = GetComponent<Canvas>();
		var sp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		var lp = default(Vector2);
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, sp, canvas.worldCamera, out lp);
		return lp;
	}
}
