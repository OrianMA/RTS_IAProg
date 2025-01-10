using System.Collections.Generic;
using UnityEngine;

public class RessourcesRecolter : SpecialZone
{
    public ItemType ItemType;
    public float ressourceDelay;
    public int ressourceNumber;

    float timePass;
    public override void OnActive()
    {
        base.OnActive();
        isActive = true;
    }


    private void Update()
    {
        if (isActive) {

            if (timePass >= ressourceDelay)
            {
                timePass = 0;
                List<PlayerController> currentSoldierAssign = new();
                currentSoldierAssign = soldierAssign;

                foreach(PlayerController soldier in currentSoldierAssign)
                {
                    soldier.AddInventory(ItemType, ressourceNumber);
                }

            } else
                timePass += Time.deltaTime;

            if (soldierAssign.Count <= 0)
            {
                isActive = false;
                timePass = 0;
            }
        }
    }


}
