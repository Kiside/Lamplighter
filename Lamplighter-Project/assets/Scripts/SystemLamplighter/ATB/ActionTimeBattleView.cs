using Godot;
using System;
using System.Collections.Generic;
using SystemLamplighter.Debug;

namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleView : ControlView
	{
		[Export]
		private Control _playerContainer;
		[Export]
		private Control _enemyContainer;

		[Export]
		private Vector2 avatarSize = Vector2.Zero;
		[Export]
		private Vector2 avatarMinimumSize = Vector2.Zero;
		[Export]
		TextureRect.ExpandModeEnum avatarExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;

		//private List<float> _charactersPosition;
		private List<TextureRect> _characters;

		private const float TEMP_DEFAULT_SIZE_AVATAR = 15;


		public override void Init()
		{
			DebugLamplighter.Assert(_enemyContainer != null, "_enemyContainer is null");
			DebugLamplighter.Assert(_playerContainer != null, "_playerContainer is null");

			_characters = new List<TextureRect>();
		}

		/// <summary>
		/// Metodo per aggiungere il personaggio nella View dell'atb
		/// </summary>
		/// <param name="character"></param>
		public void AddCharacter(AtbCharacterProperties character)
		{
			DebugLamplighter.Assert(character != null, "character is null");

			Log.PrintMessage($"questo è null? {character}");
			TextureRect textureRect = new TextureRect();
			textureRect.SettingUp(avatarSize, avatarMinimumSize, character.Avatar, avatarExpandMode);
			textureRect.SetAnchors();

			switch (character.CharacterType)
			{
				case AtbCharacterType.ALLY:
					_playerContainer.AddChild(textureRect);
					break;
				case AtbCharacterType.ENEMY:
					_enemyContainer.AddChild(textureRect);
					break;
			}

			_characters.Add(textureRect);
		}

		/// <summary>
		/// Rimuovere un personaggio nella View dell'ATB
		/// </summary>
		public void RemoveCharacter()
		{

		}

		public void UpdatePositions(List<float> charactersPosition)
		{
			DebugLamplighter.Assert(_characters != null, "_characters is null");

			var minimumSize = _control.CustomMinimumSize;
			for (int i = 0; i < _characters.Count; i++)
			{
				float currentPosition = (minimumSize.X / _characters[i].Position.X + TEMP_DEFAULT_SIZE_AVATAR);
				_characters[i].Position = new Vector2(currentPosition, _characters[i].Position.Y);
			}
		}
		public void UpdatePosition(float position, int index)
		{
			DebugLamplighter.Assert(_characters != null, "_characters is null");
			DebugLamplighter.Assert(index < _characters.Count, "index goes overflow");

			var barWidth = _control.Size.X;

			float x = Mathf.Lerp(0, barWidth - avatarSize.X, position);
			_characters[index].Position = new Vector2(x, _characters[index].Position.Y);

			//float currentPosition = (minimumSize.X / _characters[index].Position.X + TEMP_DEFAULT_SIZE_AVATAR);
			//_characters[index].Position = new Vector2(currentPosition, _characters[index].Position.Y);
			//_characters[index].SetPosition(new Vector2(currentPosition, _characters[index].Position.Y));
			//Log.PrintMessage($"{index} - {currentPosition})");
		}

		public void ClearCharacters()
		{
			DebugLamplighter.Assert(_characters != null, "_characters is null");
			DebugLamplighter.Assert(_enemyContainer != null, "_enemyContainer is null");
			DebugLamplighter.Assert(_playerContainer != null, "_playerContainer is null");

			_characters.Clear();
			for (int i = 0; i < _enemyContainer.GetChildren().Count; i++)
			{
				_enemyContainer.RemoveChild(_enemyContainer.GetChildren()[i]);
			}
			for (int i = 0; i < _playerContainer.GetChildren().Count; i++)
			{
				_playerContainer.RemoveChild(_playerContainer.GetChildren()[i]);
			}
		}
	}
}
