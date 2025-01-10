using System.Collections.Generic;
using UnityEngine;

public class SpecialZone : MonoBehaviour
{
    public List<Transform> soldierPos;
    public List<PlayerController> soldierAssign;
    public bool isActive;

    public virtual void OnActive()
    {
        isActive = true;
    }
}
