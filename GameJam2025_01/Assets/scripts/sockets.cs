using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sockets : MonoBehaviour
{
    [Header("Scripts attributing")]
    public turretBuildManagers turretBuildScript;

    [Header("Turret settings")]
    public bool alreadyBuilt;

    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        turretBuildScript = gameManager.GetComponent<turretBuildManagers>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void detected()
    {
        turretBuildScript.selectedSocket = this.gameObject;
        turretBuildScript.Selected();
    }
}
