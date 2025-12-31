
namespace SystemLamplighter.Events
{
	public sealed class AtbCommandPhaseEndEvent
	{
		public IActionData ActionData {get;}

		public AtbCommandPhaseEndEvent(IActionData actionData)
		{
			ActionData = actionData;
		}

	}
}