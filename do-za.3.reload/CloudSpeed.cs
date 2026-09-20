using Godot;

public partial class CloudSpeed : Control
{
	[Export] public float Speed = 10.0f; // пикселей в секунду

	[Export] public NodePath Cloud1Path;
	[Export] public NodePath Cloud2Path;

	private TextureRect _cloud1;
	private TextureRect _cloud2;
	private float _cloudWidth;

	public override void _Ready()
	{
		_cloud1 = GetNode<TextureRect>(Cloud1Path);
		_cloud2 = GetNode<TextureRect>(Cloud2Path);
		_cloudWidth = _cloud1.Size.X;
	}

	public override void _Process(double delta)
	{
		_cloud1.Position -= new Vector2(Speed * (float)delta, 0);
		_cloud2.Position -= new Vector2(Speed * (float)delta, 0);

		if (_cloud1.Position.X <= -_cloudWidth)
			_cloud1.Position = new Vector2(_cloud2.Position.X + _cloudWidth, _cloud1.Position.Y);

		if (_cloud2.Position.X <= -_cloudWidth)
			_cloud2.Position = new Vector2(_cloud1.Position.X + _cloudWidth, _cloud2.Position.Y);
	}
}
