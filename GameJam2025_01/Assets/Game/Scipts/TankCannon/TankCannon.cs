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
    AudioClip gunsound;

    [SerializeField]
    AudioClip cannonSound;

    [SerializeField]
    AudioClip grenadeSound;

    private AudioSource _audio;

    public bool isCooldownC = false;

    public bool isCooldownM = false;

    public bool isCooldownG = false;

    private GameObject c;



    private void Awake()
    {
        //CreerReticule();
        _audio = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerManager.getActiveState() == GestionnaireJoueur.State.tank)
        {
            if (Input.GetKeyDown(playerManager.getKeybinds().TirSecondaireTank) && !isCooldownC)
            {
                GameObject b = Instantiate(_bulletC, cCannonEnd.transform.position, cCannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(cCannonEnd.transform.TransformDirection(Vector3.forward) * 30f, ForceMode.Impulse);
                _audio.PlayOneShot(cannonSound);
                isCooldownC = true;
                if (isCooldownC == true)
                {
                    StartCoroutine(CoolDownCannon());
                }
                Destroy(b, 5f);
            }

            if (Input.GetKeyDown(playerManager.getKeybinds().TirPrincipalTank) && !isCooldownM)
            {
                Vector3 MGCannon = new Vector3(mCannonEnd.transform.position.x, mCannonEnd.transform.position.y + 0.1f, mCannonEnd.transform.position.z + 0.04f);

                GameObject b = Instantiate(_bulletM, MGCannon, mCannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(mCannonEnd.transform.TransformDirection(Vector3.forward) * 50f, ForceMode.Impulse);
                _audio.PlayOneShot(gunsound);
                ammoM--;
                if (ammoM <= 0)
                {
                    isCooldownM = true;
                }
                if (isCooldownM == true)
                {
                    StartCoroutine(CoolDownMachineGun());
                }
                Destroy(b, 5f);
            }

            if (Input.GetKeyDown(playerManager.getKeybinds().ReparationTank) && !isCooldownG)
            {
                GameObject g = Instantiate(_bulletG, gCannonEnd.transform.position, gCannonEnd.transform.rotation);
                g.GetComponent<Rigidbody>().AddForce(gCannonEnd.transform.TransformDirection(Vector3.forward) * 30f, ForceMode.Impulse);
                _audio.PlayOneShot(grenadeSound);
                isCooldownG = true;
                if (isCooldownG == true)
                {
                    StartCoroutine(CoolDownGrenade());
                }
                Destroy(g, 5f);
            }

            //UpdatePositionReticule();
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
    }

    public IEnumerator CoolDownMachineGun()
    {
        yield return new WaitForSeconds(cooldownTimeM);
        ammoM = 50;
        isCooldownM = false;
    }

    public IEnumerator CoolDownGrenade()
    {
        yield return new WaitForSeconds(cooldownTimeG);
        isCooldownG = false;
    }

}
