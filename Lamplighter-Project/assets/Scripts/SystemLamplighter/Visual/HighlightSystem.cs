using SystemLamplighter.Interfaces;

public class HighlightSystem : IHighlightSystem
{
	public void Highlight(IHighlightable highlightable)
	{
		highlightable.ApplyHighlight();
	}

	public void Unhighlight(IHighlightable highlightable)
	{
		highlightable.RemoveHighlight();
	}
}