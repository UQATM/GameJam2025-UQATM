using UnityEngine;

public class ChoisirCadrant : MonoBehaviour
{
    [SerializeField] Camera camTop;
    [SerializeField] GameObject[] cadrants;
    [SerializeField] GestionnaireJoueur playerManager;
    [SerializeField] GameObject tank;

    GameObject camCadrantActif;
    GestionnaireJoueur.Keybinds keybinds;

    // Start is called before the first frame update
    void Start()
    {
        keybinds = playerManager.getKeybinds();
        tank.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerManager.getActiveState())
        {
            case GestionnaireJoueur.State.factory:
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
                                playerManager.setActiveState(GestionnaireJoueur.State.towerDefence);
                            }
                        }
                    }
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
}
