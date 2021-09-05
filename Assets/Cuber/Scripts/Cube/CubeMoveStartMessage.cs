using System.Collections;
using System.Collections.Generic;
using TheCuber.Cube;
using UnityEngine;

public class CubeMoveStartMessage : Message
{
    #region Public
    public readonly CubeMover cubeMover;
    public readonly Vector3Int fromPosition;
    public readonly Vector3Int toPosition;
    #endregion

    #region Functions
    public CubeMoveStartMessage(CubeMover cubeMover, Vector3Int fromPosition, Vector3Int toPosition)
    {
        this.cubeMover = cubeMover;
        this.fromPosition = fromPosition;
        this.toPosition = toPosition;
    }
    #endregion
}
