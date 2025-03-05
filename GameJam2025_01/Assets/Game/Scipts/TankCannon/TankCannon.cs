using System.Collections;
using UnityEngine;

public class TankCannon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletC;

    [SerializeField]
    private GameObject _bulletM;

    [SerializeField]
    private GameObject _cannonEnd;

    [SerializeField]
    private int cooldownTimeC = 10;

    [SerializeField]
    private int cooldownTimeM = 3;

    [SerializeField]
    private int ammoM = 50;

    [SerializeField] GestionnaireJoueur playerManager;

    public bool isCooldownC = false;

    public bool isCooldownM = false;

    private void Update()
    {
        if (playerManager.getActiveState() == GestionnaireJoueur.State.tank)
        {
            if (Input.GetKeyDown(playerManager.getKeybinds().TirSecondaireTank) && !isCooldownC)
            {
                GameObject b = Instantiate(_bulletC, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.up) * 25f, ForceMode.Impulse);
                isCooldownC = true;
                if (isCooldownC == true)
                {
                    StartCoroutine(CoolDownCannon());
                }
                Destroy(b, 10f);
            }

            if (Input.GetKeyDown(playerManager.getKeybinds().TirPrincipalTank) && !isCooldownM)
            {
                GameObject b = Instantiate(_bulletM, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.up) * 25f, ForceMode.Impulse);
                ammoM--;
                if (ammoM <= 0)
                {
                    isCooldownM = true;
                }
                if (isCooldownM == true)
                {
                    StartCoroutine(CoolDownMachineGun());
                }
                Destroy(b, 10f);
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

}
