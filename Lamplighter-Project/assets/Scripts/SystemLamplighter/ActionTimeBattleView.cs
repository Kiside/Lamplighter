using Godot;
using System;
using System.Collections.Generic;

namespace SystemLamplighter
{
	public partial class ActionTimeBattleView : AbstractView
	{
		[Export]
		private Control _playerContainer;
		[Export]
		private Control _enemyContainer;

		private List<float> _charactersPosition;

		private const float TEMP_DEFAULT_SIZE_AVATAR = 15;


		public override void Init()
		{
			if (_playerContainer is null)
				Log.PrintWarning("There is no PlayerContainer");
			if (_enemyContainer is null)
				Log.PrintWarning("There is no EnemyContainer");

			_charactersPosition = new List<float>();
		}

		public void AddCharacter(AtbCharacter character)
		{

			if (character is null)
				Log.PrintMessage("No AtbCharacter");

			TextureRect textureRect = new TextureRect();
			textureRect.Texture = ImageTexture.CreateFromImage(character.Avatar);

			switch (character.CharacterType)
			{
				case AtbCharacterType.ALLY:
					_playerContainer.AddChild(textureRect);
					break;
				case AtbCharacterType.ENEMY:
					_enemyContainer.AddChild(textureRect);
					break;
			}

			_charactersPosition.Add(0f);
		}

		public void UpdatePositions(List<float> charactersPosition)
		{
			var minimumSize = this.GetMinimumSize();
			for (int i = 0; i < _charactersPosition.Count; i++)
			{
				float currentPosition = (minimumSize.X / _charactersPosition[i] + TEMP_DEFAULT_SIZE_AVATAR);
				_charactersPosition[i] = currentPosition;
			}
		}
		public void UpdatePosition(float position, int index)
		{
			var minimumSize = this.GetMinimumSize();
			float currentPosition = (minimumSize.X / _charactersPosition[index] + TEMP_DEFAULT_SIZE_AVATAR);
			_charactersPosition[index] = currentPosition;
		}

		public void ClearCharacters()
		{
			_charactersPosition.Clear();
		}
	}
}
