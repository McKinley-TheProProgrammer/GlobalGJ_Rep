using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public static class CamUtils
{
    public static Vector3 ScreenToWorldPosition(CinemachineBrain mainBrain, Vector3 targetPos)
    {
        targetPos.z = mainBrain.OutputCamera.nearClipPlane;

        return mainBrain.OutputCamera.ScreenToWorldPoint(targetPos);
    }
}
