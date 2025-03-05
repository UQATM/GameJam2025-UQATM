using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] Camera camTop;
    [SerializeField] GameObject[] cadrants;
    [SerializeField] GestionnaireJoueur playerManager;
    [SerializeField] GameObject tank;
    [SerializeField] Transform canonEnd;
    [SerializeField] GameObject p_Missile;
    public two twoScript;
    GameObject camCadrantActif;
    GestionnaireJoueur.Keybinds keybinds;

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

                        if (Input.GetKeyDown(keybinds.MortarCanon))
                        {
                            Vector3 positionMouse = Input.mousePosition;
                            positionMouse.z = camTop.transform.position.y;
                            positionMouse = camTop.ScreenToWorldPoint(positionMouse);
                            TirMortier(positionMouse.x, positionMouse.z);
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
        Debug.Log("X: " + _posX + "; Z: " + _posZ);
        Vector3 positionCible = new Vector3(_posX, 0, _posZ);
        Vector3 positionInitial = canonEnd.position;

        Vector3 direction = positionCible - positionInitial;
        float distance = direction.magnitude;

        float differentielY = positionCible.y - positionInitial.y;
        float angleTir = 45f * Mathf.Deg2Rad;

        float velociteInitial = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angleTir));

        float vx = velociteInitial * Mathf.Cos(angleTir);
        float vy = velociteInitial * Mathf.Sin(angleTir);
        float vz = velociteInitial * Mathf.Sin(angleTir);

        GameObject projectile = Instantiate(p_Missile, positionInitial, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(vx, vy, vz);
    }
}
