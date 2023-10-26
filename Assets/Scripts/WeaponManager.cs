using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private float timeSinceLastShot;

    private Camera cam;
    private EquipmentManager manager;
    private Animator anim;
    private Animator weaponAnim;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform armsPivot;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        manager = GetComponent<EquipmentManager>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, manager.currentWeapon.returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, manager.currentWeapon.snappiness * Time.deltaTime);
        cameraPivot.localRotation = Quaternion.Euler(currentRotation);
        armsPivot.localRotation = cameraPivot.localRotation;

        if (Input.GetKey(KeyCode.Mouse0) && manager.currentWeapon.currentAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && manager.currentWeapon.remainingAmmo != 0)
        {
            Reload();
        }
    }

    private void RaycastShoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit rayHit;

        float currentWeaponRange = manager.currentWeapon.range;

        if (Physics.Raycast(ray, out rayHit, currentWeaponRange))
        {
            Debug.Log(rayHit.transform.name);
        }
    }

    private void Shoot()
    {
        weaponAnim = manager.currentWeaponObject.GetComponent<Animator>();

        if (Time.time > timeSinceLastShot + 60 / manager.currentWeapon.rateOfFire)
        {
            timeSinceLastShot = Time.time;

            RaycastShoot();

            anim.SetTrigger("Shoot");
            weaponAnim.SetTrigger("Shoot");
            manager.currentWeapon.currentAmmo--;

            targetRotation += new Vector3(manager.currentWeapon.recoilX, manager.currentWeapon.recoilY, manager.currentWeapon.recoilZ);
        }
    }

    private void Reload()
    {
        weaponAnim = manager.currentWeaponObject.GetComponent<Animator>();

        if (manager.currentWeapon.currentAmmo != manager.currentWeapon.magazineSize)
        {
            if (manager.currentWeapon.currentAmmo != 0)
            {
                anim.SetTrigger("Tactical Reload");
                weaponAnim.SetTrigger("Tactical Reload");
            }
            else
            {
                anim.SetTrigger("Reload");
                weaponAnim.SetTrigger("Reload");
            }

            int reloadAmount = Mathf.Min(manager.currentWeapon.magazineSize - manager.currentWeapon.currentAmmo, manager.currentWeapon.remainingAmmo);

            manager.currentWeapon.currentAmmo += reloadAmount;
            manager.currentWeapon.remainingAmmo -= reloadAmount;
        }
    }
}
