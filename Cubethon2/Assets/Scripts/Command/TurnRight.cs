using UnityEngine;

public class TurnRight : Command
{
    
    public TurnRight(PlayerMovement controller)
    {
        this.controller = controller;
    }

    public override void Execute()
    {
        controller.Move(PlayerMovement.Direction.Right);
    }
}
