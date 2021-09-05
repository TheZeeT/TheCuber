using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class CubeMoveEndMessage : Message
{
    #region Public
    public readonly CubeMover cubeMover;
    public readonly Vector3Int fromPosition;
    public readonly Vector3Int endPosition;
    #endregion

    #region Functions
    public CubeMoveEndMessage(CubeMover cubeMover, Vector3Int fromPosition, Vector3Int endPosition)
    {
        this.cubeMover = cubeMover;
        this.fromPosition = fromPosition;
        this.endPosition = endPosition;
    }
    #endregion
}
