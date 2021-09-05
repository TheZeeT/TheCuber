using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonPressedMessage : Message
{
    #region Public
    public readonly FloorButton button;
    public readonly bool isPressed;
    #endregion

    #region Functions
    public FloorButtonPressedMessage(FloorButton button, bool isPressed)
    {
        this.button = button;
        this.isPressed = isPressed;
    }
    #endregion
}
