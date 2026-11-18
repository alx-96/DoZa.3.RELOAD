using Godot;
using System.Threading.Tasks;

public partial class CloudSpeed : Control
{
	[Export] public float Speed { get; set; } = 20.0f; // Пикселей в секунду

	[Export] public NodePath Cloud1Path;
	[Export] public NodePath Cloud2Path;

	private TextureRect _cloud1;
	private TextureRect _cloud2;
	private float _cloudWidth;

	public override async void _Ready()
	{
		_cloud1 = GetNode<TextureRect>(Cloud1Path);
		_cloud2 = GetNode<TextureRect>(Cloud2Path);

		// Ждем 1 кадр асинхронно, чтобы Godot успел рассчитать размеры UI
		await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

		// Теперь размер гарантированно точный
		_cloudWidth = _cloud1.GetRect().Size.X;

		// Если ширина всё ещё 0 (например, не задана текстура), берем дефолтный Size
		if (_cloudWidth <= 0) 
			_cloudWidth = _cloud1.Size.X;

		// Расставляем начальные позиции
		_cloud1.Position = new Vector2(0, _cloud1.Position.Y);
		_cloud2.Position = new Vector2(_cloudWidth, _cloud2.Position.Y);
	}

	public override void _Process(double delta)
	{
		// Проверяем, что ширину уже успели посчитать в _Ready
		if (_cloudWidth <= 0) return;

		float moveAmount = Speed * (float)delta;

		_cloud1.Position -= new Vector2(moveAmount, 0);
		_cloud2.Position -= new Vector2(moveAmount, 0);

		// Зацикливание 1-го облака
		if (_cloud1.Position.X <= -_cloudWidth)
		{
			_cloud1.Position = new Vector2(_cloud2.Position.X + _cloudWidth, _cloud1.Position.Y);
		}

		// Зацикливание 2-го облака
		if (_cloud2.Position.X <= -_cloudWidth)
		{
			_cloud2.Position = new Vector2(_cloud1.Position.X + _cloudWidth, _cloud2.Position.Y);
		}
	}
}
