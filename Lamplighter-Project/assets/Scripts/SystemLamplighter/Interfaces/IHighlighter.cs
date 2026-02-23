using SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per l'inizializzazione dei elementi che possono evidenziare
/// </summary>
public interface IHighlighter
{
	public void InitHighlighterSystem(IHighlightSystem highlightSystem);
}