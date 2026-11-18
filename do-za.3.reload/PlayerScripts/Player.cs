using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public AnimatedSprite2D animatedSprite;
	public float Speed = 100.0f;
	public const float JumpVelocity = -400.0f;
	private PlayerState state;

    PlayerState IsPlayerPressedButton;
    public override void _Ready()
    {
		state = PlayerState.Idle;
    }
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("kLeft", "kRight", "kUp", "kDown");
		//-------------------------------------------------------------------------
		Speed = CheckSpeed();
        CheckSide(direction);
		IsPlayerPressedButton = GetButton();
		if(IsPlayerPressedButton != 0)
		{
			state = IsPlayerPressedButton;
		}
		else if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			if(velocity.Y >= 0)
				state = PlayerState.Fall;
			else if(velocity.Y < 0)
				state = PlayerState.Jump;
		}
		else if (Input.IsActionPressed("ui_accept"))
		{
            velocity.Y = JumpVelocity;
		}
        else
        {
			state = GetGroundState(direction);
        }


        switch (state)
		{
			case PlayerState.Idle:
				animatedSprite.Play("PlayerStay");
                break;

			case PlayerState.Walk:
                animatedSprite.Play("PlayerWalk");
                break;

			case PlayerState.Run:
                animatedSprite.Play("PlayerRun");
                break;

			case PlayerState.Jump:
                animatedSprite.Play("PlayerJump");
                break;

			case PlayerState.Fall:
                animatedSprite.Play("PlayerFalling");
                break;

			case PlayerState.Throw:
				animatedSprite.Play("PlayerThrow");
				break;
		}
		
        velocity.X = direction.X * Speed;
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
	
	enum PlayerState
	{
		Idle,
		Walk,
		Run,
		Fall,
		Jump,
		Throw

	}

	private PlayerState GetGroundState(Vector2 direction)
	{
		if (direction.X == 0)
			return PlayerState.Idle;

		if(Input.IsKeyPressed(Key.Shift))
			return PlayerState.Run;

		return PlayerState.Walk;
	}

	private PlayerState GetButton()
	{
		if (Input.IsActionJustPressed("Throw"))
		{
			return PlayerState.Throw;
		}
		return 0;
	}

	private float CheckSpeed()
	{
		if(Input.IsKeyPressed(Key.Shift))
		{
			return 300.0f;
		}
		return 100.0f;
	}
}