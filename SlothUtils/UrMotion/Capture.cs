using System.Collections.Generic;

namespace SlothMotion
{
	public static class Capture
	{
		public static IEnumerator<V> Create<V>(IEnumerator<V> source, out IEnumerator<V> destination)
		{
			var capture = new CaptureHolder<V>();
			destination = capture.Enumerator;
			return CaptureEnumerator(source, capture);
		}

		public static IEnumerator<V> Create<V>(IEnumerator<V> source, IEnumerator<V>[] destination)
		{
			var capture = new CaptureHolder<V>();
			destination[0] = capture.Enumerator;
			return CaptureEnumerator(source, capture);
		}

		static IEnumerator<V> CaptureEnumerator<V>(IEnumerator<V> source, CaptureHolder<V> capture)
		{
			while (source.MoveNext()) {
				var val = source.Current;
				capture.Value = val;
				yield return val;
			}
		}

		class CaptureHolder<V>
		{
			public V Value;
			public IEnumerator<V> Enumerator {
				get {
					for (;;) {
						yield return Value;
					}
				}
			}
		}
	}
}