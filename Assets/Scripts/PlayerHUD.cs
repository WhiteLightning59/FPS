using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponNameText;

    private EquipmentManager manager;

    private void Start()
    {
        manager = GetComponent<EquipmentManager>();
    }

    public void UpdateWeaponState()
    {
        ammoText.text = manager.currentWeapon.currentAmmo.ToString() + " / " + manager.currentWeapon.remainingAmmo.ToString();
        weaponNameText.text = manager.currentWeapon.weaponName;
    }
}
