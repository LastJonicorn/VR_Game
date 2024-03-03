using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider SnapTurn;
    public ActionBasedContinuousTurnProvider ContinuousTurn;

    public void SetTypeFromIndex(int index)
    {
        if(index == 0)
        {
            SnapTurn.enabled = false;
            ContinuousTurn.enabled = true;
        }
        if (index == 1)
        {
            SnapTurn.enabled = true;
            ContinuousTurn.enabled = false;
        }

    }
}
