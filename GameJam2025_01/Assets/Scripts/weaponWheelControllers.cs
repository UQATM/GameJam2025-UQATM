using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class weaponWheelControllers : MonoBehaviour
{
    [Header("Scripts attributing")]
    public turretBuildManagers manager;

    [Header("Turret attribute")]
    public int Id;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI UIName;


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

    public void setName(string name)
    {
        UIName.text = name;
    }
    public void eraseName()
    {
        UIName.text = "No turret selected";
    }
}
