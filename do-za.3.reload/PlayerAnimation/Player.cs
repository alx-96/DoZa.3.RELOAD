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
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if(Input.IsKeyPressed(Key.Shift))
        {
            Speed = 300.0f;
        }
        else
        {
            Speed = 100.0f;
        }

		// -------------------------------------------------------------------------
		if(direction.X != 0)
		{
            velocity.X = direction.X * Speed;
        }
        else
		{
			velocity.X = 0;
		}
        //-------------------------------------------------------------------------
		CheckSide(direction);
        // In Falling
        if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			animatedSprite.Play("PlayerFalling");
		}
		else
		{
			if (Input.IsActionJustPressed("ui_accept"))
			{
				velocity.Y = JumpVelocity;
				animatedSprite.Play("PlayerJump");
			}

			else if (direction != Vector2.Zero)
			{
				if (Input.IsKeyPressed(Key.Shift))
				{
					animatedSprite.Play("PlayerRun");
				}
				else
				{
					animatedSprite.Play("PlayerWalk");
				}
			}
			else
			{
				//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				animatedSprite.Play("PlayerStay");
			}
		}
		

		Velocity = velocity;
		MoveAndSlide();
	}


	private void CheckSide(Vector2 directionCS)
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