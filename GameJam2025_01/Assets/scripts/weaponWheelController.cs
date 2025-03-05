using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weaponWheelController : MonoBehaviour
{
    [Header("Scripts attributing")]
    public turretBuildManager manager;

    [Header("Turret attribute")]
    public int Id;
    private Animator anim;
    public string itemName;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
    }

    public void turretSelection()
    {
        manager.buildTurret(Id);
    }

    public void closing()
    {
        manager.Deselected();
    }
}
