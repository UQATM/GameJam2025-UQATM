using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sockets : MonoBehaviour
{
    [Header("Scripts attributing")]
    public turretBuildManager turretBuildScript;

    [Header("Turret settings")]
    public bool alreadyBuilt;
    // Start is called before the first frame update
    void Start()
    {
        
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
