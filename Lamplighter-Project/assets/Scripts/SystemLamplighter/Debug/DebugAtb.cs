using Godot;
using System;
using System.Collections.Generic;
using SystemLamplighter.ATB;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using Characters;
using Characters.Playable;
using System.Diagnostics;

public partial class DebugAtb : Node
{
	[Export]
	public Godot.Collections.Array<AbstractCharacterController> _allies;
	[Export]
	public Godot.Collections.Array<AbstractCharacterController> _enemies;

	[ExportGroup("Atb")]
	[Export]
	public NodePath Atb;

	[ExportGroup("BattleMenu")]
	[Export]
	public NodePath BattleMenu;

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
	private BattleMenuController _battleMenuController;



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
		if (BattleMenu is null)
			Log.PrintWarning("There is no BattleMenu");


		_battleMenuController = GetNode<BattleMenuController>(BattleMenu);
		_atbController = GetNode<ActionTimeBattleController>(Atb);
		_atbController.OnCommandEvent += ShowBattleMenu;
	}

	private void AutoAddCharacters()
	{
		if(_allies != null && _allies.Count > 0)
		{
			for (int i = 0; i < _allies.Count; i++)
			{
				
				if(_allies[i] is ICharacterControllerAtb ally)
				{
					AddCharacter(ally.ATB_GetCharacterProperties());
				}	
					
				
			}
		}
		if(_enemies != null && _enemies.Count > 0)
		{
			for (int i = 0; i < _enemies.Count; i++)
			{

				if(_enemies[i] is ICharacterControllerAtb enemy)	
					AddCharacter(enemy.ATB_GetCharacterProperties());
				
			}
		}
	}

	public void ShowBattleMenu()
	{
		_battleMenuController.Show();
	}

	public void StartCombatClick()
	{
		AutoAddCharacters();
		_atbController.ContinueAtb();
	}

	public void StopCombatClick()
	{
		_atbController.StopAtb();
	}

	public void OnAddCharactersClick()
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
		if((_allies == null || _allies.Count == 0) && (_enemies == null || _enemies.Count == 0))
		{
			Random random = new Random();
			for (int i = 0; i < maxValue; i++)
			{
				string name = $"Ciccio{i}";
				double speed = 0.05 + random.NextDouble() * (0.3 - 0.05);
				
				// AtbCharacter character = new AtbCharacter(image, (float)speed, name, type, );
				// AddCharacter(character);
			}
		}
	}

	public void OnRemoveCharacterClick()
	{

	}

	public void AddCharacter(AtbCharacterProperties character)
	{
		_atbController.AddCharacter(character);
	}

	public void OnAllySliderChange(float value)
	{
		_atbController.CallViewUpdatePosition(value, allyNumberIndex.Prefix.ToInt());
	}


	public override void _ExitTree()
	{
		base._ExitTree();

		_atbController.OnCommandEvent -= ShowBattleMenu;
	}
}
