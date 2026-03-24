using System;

public class Identification
{
	public Guid ID { get; set; }
	public string Name { get; set; }

	public Identification(string name)
	{
		Name = name;
		ID = Guid.NewGuid();
	}
}