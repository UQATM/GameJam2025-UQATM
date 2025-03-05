using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBuildManagers : MonoBehaviour
{
    [Header("Which socket will have turret spawn")]
    public GameObject selectedSocket;

    [Header("UI to activate")]
    public GameObject uiSelectionTool;

    [Header("Select which turret to activate")]
    public GameObject turret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildTurret(int turretID)
    {
        Debug.Log("meow" + (turretID - 1));
        Debug.Log(selectedSocket.transform.childCount);
        if (turretID > 0 && turretID <= selectedSocket.transform.childCount) // Ensure the turretID is valid
        {
            Debug.Log("meow2 : ");
            Transform turret = selectedSocket.transform.GetChild(turretID - 1); // Get the child (0-based index)
            turret.gameObject.SetActive(true);
        }

        selectedSocket.GetComponent<sockets>().alreadyBuilt = true;
        Deselected();
    }
    public void Selected()
    {
        uiSelectionTool.SetActive(true);
    }

    public void Deselected()
    {
        selectedSocket = null;
        uiSelectionTool.SetActive(false);
    }

}
