using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private AnimatedSprite2D animatedSprite;
	public float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
            velocity.Y = JumpVelocity;
		}

		if (!IsOnFloor())
		{
			animatedSprite.Play("PlayerJump");
		}
		else
		{
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
			{
				if (Input.IsKeyPressed(Key.Shift))
				{
					animatedSprite.Play("PlayerRun");
					Speed = 300.0f;
				}
				else
				{
					animatedSprite.Play("PlayerWalk");
					Speed = 100.0f;
				}
				CheckSide(velocity, direction);
				velocity.X = direction.X * Speed;
			}
			else
			{
				animatedSprite.Play("PlayerStay");
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}
		}


		Velocity = velocity;
		MoveAndSlide();
	}


	private void CheckSide(Vector2 velocityCS, Vector2 directionCS)
	{
		if(directionCS.X > 0)
		{
			animatedSprite.FlipH = false;
		}
		else if(directionCS.X < 0)
		{
			animatedSprite.FlipH = true;
		}
	}
}
