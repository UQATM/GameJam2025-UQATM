using UnityEngine;

public class ChoisirCadrant : MonoBehaviour
{
    [SerializeField] Camera camTop;
    [SerializeField] GameObject[] cadrants;
    [SerializeField] GestionnaireJoueur playerManager;
    GameObject activeCam;
    GestionnaireJoueur.Keybinds keybinds;

    // Start is called before the first frame update
    void Start()
    {
        activeCam = camTop.gameObject;
        keybinds = playerManager.getKeybinds();
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
                                activeCam = camCadrant;
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
                    activeCam.SetActive(false);
                    camTop.gameObject.SetActive(true);
                    activeCam = camTop.gameObject;
                }
                break;
            case GestionnaireJoueur.State.tank:
                if (Input.GetKeyDown(keybinds.Reduire))
                {
                    playerManager.setActiveState(GestionnaireJoueur.State.towerDefence);
                    activeCam.SetActive(false);
                    camTop.gameObject.SetActive(true);
                    activeCam = camTop.gameObject;
                }
                break;
        }
    }
}
