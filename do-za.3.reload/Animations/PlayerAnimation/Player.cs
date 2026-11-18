using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private AnimatedSprite2D animatedSprite;
	public float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;

	private int _health = 100;
	public int Health 
	{
		get { return _health; }
	}
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("kLeft", "kRight", "kUp", "kDown");


		if (Input.IsKeyPressed(Key.Shift))
		{
			Speed = 300.0f;
		}
		else
		{
			Speed = 100.0f;
		}

		// ----------------------Moving---------------------------------------------------
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
		else //OnEarth
		{
			if (Input.IsActionPressed("ui_accept"))
			{
				velocity.Y = JumpVelocity;
				animatedSprite.Play("PlayerJump");
			} // Jump

			if (direction != Vector2.Zero) // if move (animation)
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
			else // if stay (animation)
			{
				animatedSprite.Play("PlayerStay");
			}

			
			if(Input.IsActionJustPressed("Throw"))
			{
				Throw();
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

	/*private void Respawn()
	{
		animatedSprite.Play("");
	}*/

	private void Throw()
	{
		animatedSprite.Play("PlayerThrow");
	}

	private void Attack()
	{
		animatedSprite.Play("PlayerAttack");
	}
}