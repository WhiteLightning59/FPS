using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]

public class Weapon : ScriptableObject
{
    public string weaponName;

    public int currentAmmo;
    public int remainingAmmo;
    public int initialAmmo;
    public int magazineSize;
    public int maxAmmo;

    public float damage;
    public float range;
    public float rateOfFire;

    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float snappiness;
    public float returnSpeed;

    public WeaponType weaponType;
    public WeaponSlot weaponSlot;

    public GameObject prefab;
}

public enum WeaponType { Pistol, AR, SMG, MG, Shotgun }
public enum WeaponSlot { Primary, Secondary }