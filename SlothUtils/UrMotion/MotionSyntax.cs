using UnityEngine;

namespace SlothMotion
{
	public static class MotionSyntax
	{
		public static T FrameRate<T>(this T self, float fps) where T : MotionBehaviourBase
		{
			self.FrameRate = fps;
			return self;
		}

		public static T Next<T>(this T self, Callback callback) where T : MotionBehaviourBase
		{
			if (self.enabled || !self.IsComplete) {
				self.OnComplete += callback;
			} else {
				callback();
			}
			return self;
		}

		public static void PauseMotions(this GameObject g)
		{
			PauseMotions<MotionBehaviourBase>(g);
		}

		public static void PauseMotions<T>(this GameObject g) where T : MotionBehaviourBase
		{
			foreach (var m in g.GetComponents<T>()) {
				if (!m.IsComplete) {
					m.enabled = false;
				}
			}
		}

		public static void ResumeMotions(this GameObject g)
		{
			ResumeMotions<MotionBehaviourBase>(g);
		}

		public static void ResumeMotions<T>(this GameObject g) where T : MotionBehaviourBase
		{
			foreach (var m in g.GetComponents<T>()) {
				if (!m.IsComplete) {
					m.enabled = true;
				}
			}
		}
	}
}