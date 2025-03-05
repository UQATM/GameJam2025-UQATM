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
    private Camera cam;

    [SerializeField]
    private int cooldownTimeC = 10;

    [SerializeField]
    private int cooldownTimeM = 3;

    [SerializeField]
    private int ammoM = 50;

    public bool isCooldownC = false;

    public bool isCooldownM = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isCooldownC)
        {
            GameObject b = Instantiate(_bulletC, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
            isCooldownC = true;
            if (isCooldownC == true)
            {
                StartCoroutine(CoolDownCannon());
            }
            Destroy(b, 10f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isCooldownM)
        {
            GameObject b = Instantiate(_bulletM, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
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

        Vector3 mouse = Input.mousePosition;
        mouse.x = Mathf.Clamp(mouse.x, 0, Screen.width);
        mouse.y = Mathf.Clamp(mouse.y, 25, (Screen.height - 250));
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, _cannonEnd.transform.position.y + 90));

        transform.LookAt(mouseWorld);
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
