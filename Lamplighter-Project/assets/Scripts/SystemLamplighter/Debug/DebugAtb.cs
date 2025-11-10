using Godot;
using System;
using System.Collections.Generic;
using SystemLamplighter;

public partial class DebugAtb : Node
{
	[Export]
	public NodePath Atb;
	[Export]
	private TextEdit allyNumberIndex;
	[Export]
	private Slider sliderAlly;
	[Export]
	Image _alliesImage;
	[Export]
	Image _enemiesImage;

	private ActionTimeBattleController _atbController;



	public override void _Ready()
	{
		base._Ready();

		_atbController = GetNode<ActionTimeBattleController>(Atb);
	}

	public void StartCombatClick()
	{
		_atbController.ContinueAtb();
	}

	public void StopCombatClick()
	{
		_atbController.StopAtb();
	}

	public void AddCharacters()
	{
		Log.PrintMessage("Clicked add characters, allies");
		for (int i = 0; i < 2; i++)
		{
			AtbCharacterController atbCharacterController = new AtbCharacterController();
			atbCharacterController.Name = $"Ciccio{i}";
			AtbCharacter character = new AtbCharacter(_alliesImage, 0.1f, atbCharacterController, AtbCharacterType.ALLY);
			AddCharacter(character);
		}

		Log.PrintMessage("Clicked add characters, enemies");
		for (int i = 0; i < 2; i++)
		{
			AtbCharacterController atbCharacterController = new AtbCharacterController();
			atbCharacterController.Name = $"Ciccio{i}";
			AtbCharacter character = new AtbCharacter(_enemiesImage, 0.1f, atbCharacterController, AtbCharacterType.ENEMY);
			AddCharacter(character);
		}

	}

	public void RemoveCharacter()
	{

	}

	public void AddCharacter(AtbCharacter character)
	{
		_atbController.AddCharacter(character);
	}

	public void OnAllySliderChange(float value)
	{
		_atbController.CallViewUpdatePosition(value, allyNumberIndex.Text.ToInt());
	}

}
