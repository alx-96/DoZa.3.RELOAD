using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/PlayButton").Pressed += OnPlayPressed;
		GetNode<Button>("VBoxContainer/QuitButton").Pressed += OnQuitPressed;
	}
private void OnPlayPressed()
{
	GetTree().ChangeSceneToFile("res://node_2d.tscn");
}

private void OnQuitPressed()
{
	GetTree().Quit();
}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
