using System.Collections.Generic;
using System.Linq;
using Godot;
using MessagePipe;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;

namespace SystemLamplighter.Target;

/// <summary>
/// Model per la classe del targetizzazione
/// </summary>
public partial class TargetModel : AbstractModel
{
	[Export]
	private NodePath _cameraPath;
	
	#nullable enable
	private  ITargetResolver? _currentTargetResolver;
	public ITargetResolver? CurrentTargetResolver {get => _currentTargetResolver; set => _currentTargetResolver = value; }
	#nullable disable

	[Export]
	private Godot.Collections.Array<GroupsName> _groups;
	public List<string> Groups => _groups.Select(g => g.ToString()).ToList();

	private Camera3D _camera;
	public Camera3D Camera => _camera;

	public override void Init()
	{
		NodeChecking();	
	}

	private void NodeChecking()
	{
		//DebugLamplighter.Assert(_cameraPath != null, "camera path is null");

		//_camera = GetNode<Camera3D>(_cameraPath);
	}

	public override void _ExitTree()
	{
		base._ExitTree();
	}
}