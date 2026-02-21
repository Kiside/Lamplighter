
namespace SystemLamplighter.Interfaces;

// Interfaccia per il sistema di evidenziazione di elementi
public interface IHighlightSystem
{
	public void Highlight(IHighlightable target);
	public void Unhighlight(IHighlightable target);
}