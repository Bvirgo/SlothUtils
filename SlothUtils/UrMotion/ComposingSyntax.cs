namespace SlothMotion
{
	using Syntax = FluentSyntax;

	public static class ComposingSyntax
	{
		public static MotionBehaviour<V> Union<V>(this MotionBehaviour<V> self)
		{
			Syntax.Resolve<V>(self,
				(e) => e.Compose((a, b) => Composing.Union(a, b)),
				(e) => e.Compose((a, b) => Composing.Union(a, b)),
				(e) => e.Compose((a, b) => Composing.Union(a, b)),
				(e) => e.Compose((a, b) => Composing.Union(a, b))
			);
			return self;
		}
	}
}