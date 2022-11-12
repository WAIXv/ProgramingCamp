using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAction : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }//“∆∂Ø ‰»Î
    public bool JumpInput { get; private set; }//Ã¯‘æ ‰»Î

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    int presstime = 0;
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        JumpInput = context.performed;
    }
}
