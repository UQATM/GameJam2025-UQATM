using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class turretBuildManagers : MonoBehaviour
{
    [Header("Which socket will have turret spawn")]
    public GameObject selectedSocket;

    [Header("UI to activate")]
    public GameObject uiSelectionTool;

    [Header("Select which turret to activate")]
    public GameObject turret;

    [Header("Scripts attributing")]
    public economyBase economyScript;

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
        if (turretID > 0 && turretID <= selectedSocket.transform.childCount) // Ensure the turretID is valid
        {
            if(turretID == 4)
            {
                if(!economyScript.checkPrice(40))
                {
                    return;
                }
            }
            else
            {
                if (!economyScript.checkPrice(selectedSocket.transform.GetChild(turretID - 1).GetComponent<turretScript>().price))
                {
                    return;
                }
            }
            Transform turret = selectedSocket.transform.GetChild(turretID - 1); // Get the child (0-based index)
            turret.gameObject.SetActive(true);
            if(turretID == 4)
            {
                economyScript.numberOfTower++;
            }
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
