using System.Collections;
using UnityEngine;

public class TankCannon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletC;

    [SerializeField]
    private GameObject _bulletM;

    [SerializeField]
    private GameObject _bulletG;

    [SerializeField] GameObject cCannonEnd;
    [SerializeField] GameObject mCannonEnd;
    [SerializeField] GameObject gCannonEnd;

    [SerializeField]
    private int cooldownTimeC = 10;

    [SerializeField]
    private int cooldownTimeM = 3;

    [SerializeField]
    private int cooldownTimeG = 3;

    [SerializeField]
    private int ammoM = 50;

    [SerializeField]
    GestionnaireJoueur playerManager;

    [SerializeField]
    private GameObject _reticle;

    [SerializeField]
    private Camera cam;

    public bool isCooldownC = false;

    public bool isCooldownM = false;

    public bool isCooldownG = false;

    private GameObject r;
    private Vector3 posR;
    private GameObject c;
    private Vector3 posC;
    private GameObject m;
    private Vector3 posM;


    private void Awake()
    {
        CreerReticule();
    }

    private void Update()
    {
        if (playerManager.getActiveState() == GestionnaireJoueur.State.tank)
        {
            if (Input.GetKeyDown(playerManager.getKeybinds().TirSecondaireTank) && !isCooldownC)
            {
                GameObject b = Instantiate(_bulletC, cCannonEnd.transform.position, cCannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(cCannonEnd.transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
                isCooldownC = true;
                Renderer rendererC = c.GetComponent<Renderer>();
                rendererC.material.color = UnityEngine.Color.red;
                if (isCooldownC == true)
                {
                    StartCoroutine(CoolDownCannon());
                }
                Destroy(b, 10f);
            }

            if (Input.GetKeyDown(playerManager.getKeybinds().TirPrincipalTank) && !isCooldownM)
            {
                Vector3 MGCannon = new Vector3(mCannonEnd.transform.position.x, mCannonEnd.transform.position.y + 0.1f, mCannonEnd.transform.position.z + 0.04f);

                GameObject b = Instantiate(_bulletM, MGCannon, mCannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(mCannonEnd.transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
                ammoM--;
                if (ammoM <= 0)
                {
                    isCooldownM = true;
                    Renderer rendererM = m.GetComponent<Renderer>();
                    rendererM.material.color = UnityEngine.Color.red;
                }
                if (isCooldownM == true)
                {
                    StartCoroutine(CoolDownMachineGun());
                }
                Destroy(b, 10f);
            }

            if (Input.GetKeyDown(playerManager.getKeybinds().ReparationTank) && !isCooldownG)
            {
                GameObject g = Instantiate(_bulletG, gCannonEnd.transform.position, gCannonEnd.transform.rotation);
                g.GetComponent<Rigidbody>().AddForce(gCannonEnd.transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
                isCooldownG = true;
                Renderer renderer = r.GetComponent<Renderer>();
                renderer.material.color = UnityEngine.Color.red;
                if (isCooldownG == true)
                {
                    StartCoroutine(CoolDownGrenade());
                }
                Destroy(g, 10f);
            }

            UpdatePositionReticule();
        }
    }


    void CreerReticule()
    {
        c = Instantiate(_reticle, cCannonEnd.transform.position, Quaternion.identity);
        c.transform.parent = transform;
    }

    void UpdatePositionReticule()
    {
        RaycastHit hit;
        Ray ray = new Ray(cCannonEnd.transform.position, cCannonEnd.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            if(c == null)
            {
                return;
            }else
            {
                Vector3 newPosition = hit.point;
                c.transform.position = newPosition;
                c.transform.rotation = cCannonEnd.transform.rotation;
            }
        }
    }


    public IEnumerator CoolDownCannon()
    {
        yield return new WaitForSeconds(cooldownTimeC);
        isCooldownC = false;
        Renderer rendererC = c.GetComponent<Renderer>();
        rendererC.material.color = UnityEngine.Color.green;
    }

    public IEnumerator CoolDownMachineGun()
    {
        yield return new WaitForSeconds(cooldownTimeM);
        ammoM = 50;
        isCooldownM = false;
        Renderer rendererM = m.GetComponent<Renderer>();
        rendererM.material.color = UnityEngine.Color.green;
    }

    public IEnumerator CoolDownGrenade()
    {
        yield return new WaitForSeconds(cooldownTimeG);
        isCooldownG = false;
        Renderer renderer = r.GetComponent<Renderer>();
        renderer.material.color = UnityEngine.Color.green;
    }

}
