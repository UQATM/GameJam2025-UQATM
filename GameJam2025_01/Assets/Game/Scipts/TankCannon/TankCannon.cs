using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TankCannon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletC;

    [SerializeField]
    private GameObject _bulletM;

    [SerializeField]
    private GameObject _bulletG;

    [SerializeField]
    private GameObject _cannonEnd;

    [SerializeField]
    private GameObject _reticle;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private int cooldownTimeC = 10;

    [SerializeField]
    private int cooldownTimeM = 3;

    [SerializeField]
    private int cooldownTimeG = 7;

    [SerializeField]
    private int ammoM = 50;

    public bool isCooldownC = false;

    public bool isCooldownM = false;

    public bool isCooldownG = false;

    private Vector3 pos;
    private GameObject r;
    public float offset = 3f;

    void Start()
    {
        r = Instantiate(_reticle, Vector3.zero, Quaternion.identity);
        Renderer renderer = r.GetComponent<Renderer>();
        renderer.material.color = UnityEngine.Color.red;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isCooldownM)
        {
            GameObject m = Instantiate(_bulletM, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            m.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
            ammoM--;
            if (ammoM <= 0)
            {
                isCooldownM = true;
            }
            if (isCooldownM == true)
            {
                StartCoroutine(CoolDownMachineGun());
            }
            Destroy(m, 10f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isCooldownC)
        {
            GameObject c = Instantiate(_bulletC, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            c.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
            isCooldownC = true;
            if (isCooldownC == true)
            {
                StartCoroutine(CoolDownCannon());
            }
            Destroy(c, 10f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && !isCooldownG)
        {
            GameObject g = Instantiate(_bulletG, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
            isCooldownG = true;
            if (isCooldownG == true)
            {
                StartCoroutine(CoolDownGrenade());
            }
            Destroy(g, 10f);
        }

        Vector3 mouse = Input.mousePosition;
        mouse.x = Mathf.Clamp(mouse.x, 0, Screen.width);
        mouse.y = Mathf.Clamp(mouse.y, 25, (Screen.height - 250));
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, _cannonEnd.transform.position.y + 90));

        transform.LookAt(mouseWorld);

        pos = Input.mousePosition;
        pos.z = offset;
        r.transform.position = cam.ScreenToWorldPoint(pos);
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
