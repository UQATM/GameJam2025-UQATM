using System.Collections.Generic;
using UnityEngine;

public class two : MonoBehaviour
{
    public Camera mainCamera;
    public List<GameObject> objects;

    private GameObject cameraQuadrant;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject clickedObject = GetClickedObject(Input.mousePosition);

            if (clickedObject != null && clickedObject.GetComponent<sockets>().alreadyBuilt == false)//si il detecte un picking
            {
                clickedObject.GetComponent<sockets>().detected(); // hit selection        
            }
        }
    }

    GameObject GetClickedObject(Vector3 mousePosition)
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in objects)
        {
            if (IsMouseOverObject(obj, mousePosition, out float distanceToCamera))
            {
                if (distanceToCamera < closestDistance) // prend l'objet le plus proche de la camera
                {
                    closestDistance = distanceToCamera;
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }

    bool IsMouseOverObject(GameObject obj, Vector3 mousePosition, out float distanceToCamera)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        // bounds de l'objet
        Bounds bounds = renderer.bounds;

        // prend en compte les rotations et translation
        //calcule chaque bord du rectangle
        Vector3[] bords = new Vector3[8];
        bords[0] = bounds.min;
        bords[1] = bounds.max;
        bords[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        bords[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        bords[4] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        bords[5] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        bords[6] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        bords[7] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);



        Vector2 screenMin = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 screenMax = new Vector2(float.MinValue, float.MinValue);

        foreach (Vector3 bord in bords)
        {
            Vector3 screenPos =  OctoToScreenPos(bord);//transforme les world positions des corner en pos screen
                //s'assure d'avoir le coin minimum et maximum to rectangle en comparant les position           
                screenMin = Vector2.Min(screenMin, new Vector2(screenPos.x, screenPos.y));
                screenMax = Vector2.Max(screenMax, new Vector2(screenPos.x, screenPos.y));
        }

        // rectangle 2D sur l'ecran
        Rect screenRect = new Rect(screenMin, screenMax - screenMin);

        if (screenRect.Contains(mousePosition))// si le rectangle de l'objet contient les pos du click
        {
            distanceToCamera = Vector3.Distance(mainCamera.transform.position, obj.transform.position);
            return true;
        }
        distanceToCamera = float.MaxValue;
        return false;
    }
    Vector3 OctoToScreenPos(Vector3 worldPosition)
    {
        //convert the world position to camera space
        Vector3 cameraSpacePosition = mainCamera.worldToCameraMatrix.MultiplyPoint(worldPosition);
        //find the frustum position with the camera matrix
        Vector3 frustumPos = mainCamera.projectionMatrix.MultiplyPoint(cameraSpacePosition);
        //find the screen position
        Vector3 screenPosition = new Vector3((frustumPos.x + 1.0f) * 0.5f * Screen.width,
                                             (frustumPos.y + 1.0f) * 0.5f * Screen.height,
                                              frustumPos.z
        );
        return screenPosition;
    }

    public void SetQuadrantCamera(GameObject camera)
    {
        mainCamera =camera.GetComponent<Camera>();
    }
}


