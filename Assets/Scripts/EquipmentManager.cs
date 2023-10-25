using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Weapon[] weapons = new Weapon[2];

    [HideInInspector] public int currentWeaponIndex;
    [HideInInspector] public GameObject currentWeaponObject;
    [HideInInspector] public Weapon currentWeapon;

    public Transform WeaponHolderR;
    [HideInInspector] public Animator anim;

    private void Start()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.remainingAmmo = weapon.initialAmmo;
            weapon.currentAmmo = weapon.magazineSize;
        }

        anim = GetComponentInChildren<Animator>();
        EquipWeapon(weapons[0]);
        currentWeapon = weapons[0];
    }

    private void Update()
    {
        currentWeapon = weapons[currentWeaponIndex];

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponIndex != 0)
        {
            UnequipWeapon();
            EquipWeapon(weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponIndex != 1)
        {
            UnequipWeapon();
            EquipWeapon(weapons[1]);
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeaponIndex = (int)weapon.weaponSlot;
        anim.SetInteger("Weapon Slot", currentWeaponIndex);
    }

    public void UnequipWeapon()
    {
        anim.SetTrigger("Unequip");
    }
}
