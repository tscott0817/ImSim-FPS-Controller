using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ManageInputs
{
    // Defaults
    public static KeyCode moveForwardKey = KeyCode.W;
    public static KeyCode moveBackwardKey = KeyCode.S;
    public static KeyCode moveLeftKey = KeyCode.A;
    public static KeyCode moveRightKey = KeyCode.D;
    public static KeyCode leanLeftKey = KeyCode.Q;
    public static KeyCode leanRightKey = KeyCode.E;
    public static KeyCode freeLookKey = KeyCode.V;
    public static KeyCode sprintKey = KeyCode.LeftShift;
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode crouchKey = KeyCode.C;
    public static KeyCode interactionKey = KeyCode.F;
    public static KeyCode speedAdjustmentKey = KeyCode.LeftControl;
    public static KeyCode zoomKey = KeyCode.Mouse1;
    public static KeyCode flashlightKey = KeyCode.L;

    // For user reassignment
    // 'ref' is just a reference (&) yay!
    // Example Use:
    //  - InputManager.SetKeyBinding(ref InputManager.moveForwardKey, KeyCode.W);
    public static void SetKeyBinding(ref KeyCode keyBinding, KeyCode newKey)
    {
        keyBinding = newKey;

    }
}
