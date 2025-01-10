using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public List<PlayerController> soldierController;
    public Camera mainCamera;

    [Header("Detect Point")]
    public float radiusSphereDetection;
    public LayerMask layerMaskDetection;

    SpecialZone specialZone;

    private void Start()
    {
        foreach (PlayerController controller in soldierController)
        {
            controller.state = PlayerGroupState.IDLE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie s'il y a une interaction tactile ou un clic de souris
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition;

            // Récupère la position de l'écran selon le type d'entrée
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else // Si clic de souris
            {
                touchPosition = Input.mousePosition;
            }

            // Lancer un ray depuis la position de l'écran
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);

            if (Physics.SphereCast(ray, radiusSphereDetection, out RaycastHit sphereHit, Mathf.Infinity))
            {

                foreach (PlayerController controller in soldierController)
                {
                    controller.agent.SetDestination(sphereHit.point);
                }


                if (sphereHit.collider.gameObject.layer == LayerMask.NameToLayer("SpecialZone"))
                {
                    SpecialZone newSpecialZone = sphereHit.collider.GetComponent<SpecialZone>();

                    newSpecialZone.soldierAssign.Clear();
                    if (specialZone != null)
                        specialZone.soldierAssign.Clear();

                    specialZone = newSpecialZone;

                    for (int i = 0; i < specialZone.soldierPos.Count; i++)
                    {
                        if (i < soldierController.Count)
                        {
                            soldierController[i].agent.SetDestination(specialZone.soldierPos[i].position);
                            specialZone.soldierAssign.Add(soldierController[i]);
                            specialZone.OnActive();
                        }
                    }
                } else
                {
                    specialZone.soldierAssign.Clear();
                    specialZone.isActive = false;
                }
            }
        }
    }
}
