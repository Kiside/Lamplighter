using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SystemLamplighter.Debug;
using SystemLamplighter.MVC;
using SystemLamplighter.Extensions;
using SystemLamplighter.Tool;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.ATB
{
	/// <summary>
	/// View dell'ATB
	/// </summary>
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
		//private List<TextureRect> _characters;

		private Dictionary<ICombatActor, TextureRect> _actorsView;

		private const float TEMP_DEFAULT_SIZE_AVATAR = 15;


		public override void Init()
		{
			DebugLamplighter.Assert(_enemyContainer != null, "_enemyContainer is null");
			DebugLamplighter.Assert(_playerContainer != null, "_playerContainer is null");

			_actorsView = new Dictionary<ICombatActor, TextureRect>();
		}

		/// <summary>
		/// Metodo per aggiungere il personaggio nella View dell'atb
		/// </summary>
		/// <param name="character"></param>
		public void AddCharacter(ICombatActor character)
		{
			DebugLamplighter.Assert(character != null, "character is null");

			TextureRect textureRect = new TextureRect();
			textureRect.SettingUp(avatarSize, avatarMinimumSize, character.AtbProperties.Avatar, avatarExpandMode);
			textureRect.SetAnchors();

			switch (character.AtbProperties.CharacterType)
			{
				case AtbCharacterType.ALLY:
					_playerContainer.AddChild(textureRect);
					break;
				case AtbCharacterType.ENEMY:
					_enemyContainer.AddChild(textureRect);
					break;
			}

			_actorsView.Add(character, textureRect);
		}

		/// <summary>
		/// Rimuovere un personaggio nella View dell'ATB
		/// </summary>
		public void RemoveCharacter()
		{

		}

		public void UpdatePositions()
		{
			var barWidth = _control.Size.X;
			var charactersRegistry = GameBootstrap.Services.GetRequiredService<ICombatActorProvider>();

			foreach(var actor in charactersRegistry.GetActors())
			{
				if(!_actorsView.TryGetValue(actor, out var view))
					continue;
				
				float x = Mathf.Lerp(0, barWidth - avatarSize.X, actor.AtbProperties.Position);	
				view.Position = new Vector2(x, view.Position.Y);
			}
		}

		// TODO: Capire se bisogna cancellare il metodo qui sotto
		public void UpdatePosition(float position, int index)
		{
			DebugLamplighter.Assert(_actorsView != null, "_characters is null");
			DebugLamplighter.Assert(index < _actorsView.Count, "index goes overflow");

			var barWidth = _control.Size.X;

			float x = Mathf.Lerp(0, barWidth - avatarSize.X, position);
			//_characters[index].Position = new Vector2(x, _characters[index].Position.Y);
		}

		public void ClearCharacters()
		{
			DebugLamplighter.Assert(_actorsView != null, "_characters is null");
			DebugLamplighter.Assert(_enemyContainer != null, "_enemyContainer is null");
			DebugLamplighter.Assert(_playerContainer != null, "_playerContainer is null");

			_actorsView.Clear();
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
