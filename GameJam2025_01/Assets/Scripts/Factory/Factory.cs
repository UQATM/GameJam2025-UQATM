using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] Camera camTop;
    [SerializeField] GameObject[] cadrants;
    [SerializeField] GestionnaireJoueur playerManager;
    [SerializeField] GameObject tank;
    [SerializeField] GameObject p_Missile;
    [SerializeField] GameObject canon;
    [SerializeField] float vitesseMortier;
    [SerializeField] float cooldownMortier;
    [SerializeField] two twoScript;
    GameObject camCadrantActif;
    GestionnaireJoueur.Keybinds keybinds;
    GameObject camMissile;
    bool mortierReady = true;
    bool mortierShot = false;

    enum FactoryMode
    {
        choixCadrant,
        mortarCanon
    }

    FactoryMode activeMode;

    // Start is called before the first frame update
    void Start()
    {
        keybinds = playerManager.getKeybinds();
        tank.SetActive(false);
        activeMode = FactoryMode.choixCadrant;
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerManager.getActiveState())
        {
            case GestionnaireJoueur.State.factory:
                switch (activeMode)
                {
                    case FactoryMode.choixCadrant:
                        if (Input.GetKeyDown(keybinds.SwitchFactoryMode))
                        {
                            activeMode = FactoryMode.mortarCanon;
                        }

                        if (Input.GetKeyDown(keybinds.ChoisirCadran))
                        {
                            Ray ray = camTop.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit))
                            {
                                GameObject hitObject = hit.transform.gameObject;

                                foreach (GameObject cadrant in cadrants)
                                {
                                    GameObject camCadrant = cadrant.GetComponentInChildren<Camera>(true).gameObject;

                                    if (hitObject == cadrant && camCadrant)
                                    {
                                        camTop.gameObject.SetActive(false);
                                        camCadrant.SetActive(true);
                                        camCadrantActif = camCadrant;
                                        twoScript.SetQuadrantCamera(camCadrant);
                                        playerManager.setActiveState(GestionnaireJoueur.State.towerDefence);

                                    }
                                }
                            }
                        }
                        break;
                    case FactoryMode.mortarCanon:
                        if (Input.GetKeyDown(keybinds.SwitchFactoryMode))
                        {
                            activeMode = FactoryMode.choixCadrant;
                        }

                        if (Input.GetKeyDown(keybinds.MortarCanon) && mortierReady)
                        {
                            Vector3 positionMouse = Input.mousePosition;
                            positionMouse.z = camTop.transform.position.y;
                            positionMouse = camTop.ScreenToWorldPoint(positionMouse);
                            TirMortier(positionMouse.x, positionMouse.z);
                            mortierReady = false;
                            StartCoroutine(CooldownMortier());
                        }

                        if (Input.GetKeyDown(keybinds.SwitchCamMortier) && mortierShot && camMissile)
                        {
                            camMissile.SetActive(!camMissile.activeSelf);
                        }
                        break;
                }
                break;
            case GestionnaireJoueur.State.towerDefence:
                if (Input.GetKeyDown(keybinds.Reduire))
                {
                    playerManager.setActiveState(GestionnaireJoueur.State.factory);
                    camCadrantActif.SetActive(false);
                    camTop.gameObject.SetActive(true);
                }

                if (Input.GetKeyDown(keybinds.Agrandir))
                {
                    playerManager.setActiveState(GestionnaireJoueur.State.tank);
                    tank.SetActive(true);
                    camCadrantActif.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case GestionnaireJoueur.State.tank:
                if (Input.GetKeyDown(keybinds.Reduire))
                {
                    playerManager.setActiveState(GestionnaireJoueur.State.towerDefence);
                    camCadrantActif.SetActive(true);
                    tank.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
        }
    }

    void TirMortier(float _posX, float _posZ)
    {
        Vector3 positionCible = new Vector3(_posX, 0f, _posZ);
        Vector3 positionInitial = p_Missile.transform.position;
        float distance = Vector3.Distance(positionInitial, positionCible);

        float differentielY = positionCible.y - positionInitial.y;
        float angleTir = 45f * Mathf.Deg2Rad;

        float velocityInitial = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angleTir));
        float vx = velocityInitial * Mathf.Cos(angleTir);
        float vy = velocityInitial * Mathf.Sin(angleTir);

        GameObject missile = Instantiate(p_Missile, positionInitial, p_Missile.transform.rotation);
        missile.transform.localScale = new Vector3(100f, 100f, 100f);
        missile.SetActive(true);
        Rigidbody rb = missile.GetComponent<Rigidbody>();

        Vector3 direction = (positionCible - positionInitial).normalized;
        rb.velocity = new Vector3(vx * direction.x, vy, vx * direction.z);
        camMissile = missile.GetComponentInChildren<Camera>(true).gameObject;
        mortierShot = true;
    }

    IEnumerator CooldownMortier()
    {
        yield return new WaitForSeconds(cooldownMortier);
        mortierReady = true;
    }
}
