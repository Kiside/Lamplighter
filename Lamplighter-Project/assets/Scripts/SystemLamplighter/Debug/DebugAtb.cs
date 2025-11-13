using Godot;
using System;
using System.Collections.Generic;
using SystemLamplighter.ATB;
using SystemLamplighter;

public partial class DebugAtb : Node
{
	[ExportGroup("Atb")]
	[Export]
	public NodePath Atb;

	[ExportGroup("Add Characters")]
	[Export]
	private OptionButton _optionButtonWhoAdd;
	[Export]
	private SpinBox maxNumberToAdd;

	[ExportGroup("Move Ally")]
	[Export]
	private SpinBox allyNumberIndex;
	[Export]
	private Slider sliderAlly;

	[ExportGroup("Avatar")]
	[Export]
	Image _alliesImage;
	[Export]
	Image _enemiesImage;

	private ActionTimeBattleController _atbController;



	public override void _Ready()
	{
		base._Ready();

		if (Atb is null)
			Log.PrintWarning("There is no Atb");
		if (_optionButtonWhoAdd is null)
			Log.PrintWarning("There is no _optionButtonWhoAdd");
		if (maxNumberToAdd is null)
			Log.PrintWarning("There is no maxNumberToAdd");
		if (allyNumberIndex is null)
			Log.PrintWarning("There is no allyNumberIndex");
		if (sliderAlly is null)
			Log.PrintWarning("There is no sliderAlly");
		if (_alliesImage is null)
			Log.PrintWarning("There is no _alliesImage");
		if (_enemiesImage is null)
			Log.PrintWarning("There is no _enemiesImage");


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
		int maxValue = maxNumberToAdd.GetLineEdit().Text.ToInt();

		switch (_optionButtonWhoAdd.GetSelectedId())
		{
			case 0:
				AddingCharacter(AtbCharacterType.ALLY, maxValue, _alliesImage);
				break;
			case 1:
				AddingCharacter(AtbCharacterType.ENEMY, maxValue, _enemiesImage);
				break;
			case 2:
				AddingCharacter(AtbCharacterType.ALLY, maxValue, _alliesImage);
				AddingCharacter(AtbCharacterType.ENEMY, maxValue, _enemiesImage);
				break;

		}
	}

	private void AddingCharacter(AtbCharacterType type, int maxValue, Image image)
	{
		Random random = new Random();
		for (int i = 0; i < maxValue; i++)
		{
			string name = $"Ciccio{i}";
			double speed = 0.05 + random.NextDouble() * (0.3 - 0.05);
			AtbCharacter character = new AtbCharacter(image, (float)speed, name, type);
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
		_atbController.CallViewUpdatePosition(value, allyNumberIndex.Prefix.ToInt());
	}

}
