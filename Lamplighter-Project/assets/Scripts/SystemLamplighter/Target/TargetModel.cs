using System.Collections.Generic;
using System.Linq;
using Godot;
using MessagePipe;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;

/// <summary>
/// Model per la classe del targetizzazione
/// </summary>
public partial class TargetModel : AbstractModel
{
	[Export]
	private NodePath _cameraPath;
	[Export]
	private Godot.Collections.Array<TargetResolver> _targetResolvers;

	public List<TargetResolver> TargetResolvers => _targetResolvers.ToList();
	
	#nullable enable
	private  TargetResolver? _currentTargetResolver;
	public TargetResolver? CurrentTargetResolver {get => _currentTargetResolver; set => _currentTargetResolver = value; }
	#nullable disable

	private Camera3D _camera;
	public Camera3D Camera => _camera;

	public TargetResolver FindTargetResolver<T>() where T : TargetResolver
	{
		return TargetResolvers.Find(r => r is T);
	}

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