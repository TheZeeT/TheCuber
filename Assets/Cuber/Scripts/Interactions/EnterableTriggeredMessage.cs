using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterableTriggeredMessage : Message
{
    #region Inspector
    #endregion

    #region Private
    #endregion

    #region Public
    public readonly Enterable enterable;
    public readonly bool isEntered;
    #endregion

    #region Functions
    public EnterableTriggeredMessage(Enterable enterable, bool isEntered)
    {
        this.enterable = enterable;
        this.isEntered = isEntered;
    }
    #endregion

    #region Gizmos
    #endregion

    #region Classes
    #endregion
}
