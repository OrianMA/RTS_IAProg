using System.Collections.Generic;
using UnityEngine;

public class PlayerGroup : MonoBehaviour
{
    public List<PlayerController> soldierController;
    public Camera mainCamera;

    [Header("Detect Point")]
    public float radiusSphereDetection;
    public LayerMask layerMaskDetection;

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
            RaycastHit hit;

            // Vérifier si le ray touche un objet avec un Collider
            if (Physics.Raycast(ray, out hit))
            {
                // Afficher la position dans le monde
                //agent.nextPosition = hit.point;
                foreach (PlayerController controller in soldierController) {
                
                    controller.agent.SetDestination(hit.point);
                }
                DetectPoint(hit.point);
                //agent.Move(hit.point);
                //agent.Move(hit.point);
                //agent.SetPath();
                // Tu peux utiliser hit.point pour manipuler les données de la position sur la carte
            }
        }
    }


    public void DetectPoint(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.SphereCast(point, radiusSphereDetection, Vector3.forward, out hit, layerMaskDetection))
        {
            SpecialZone specialZone = hit.collider.GetComponent<SpecialZone>();
            for (int i = 0; i < specialZone.soldierPos.Count; i++) {
                if (i < soldierController.Count)
                {
                    soldierController[i].agent.SetDestination(specialZone.soldierPos[i].position);
                    specialZone.soldierAssign.Add(soldierController[i]);
                    specialZone.OnActive();
                }
            }
        }
    }
}
