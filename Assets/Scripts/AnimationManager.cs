using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private EquipmentManager manager;
    private PlayerHUD hud;

    private void Start()
    {
        manager = GetComponentInParent<EquipmentManager>();
        hud = GetComponentInParent<PlayerHUD>();
    }

    public void InstantiateWeapon()
    {
        manager.currentWeaponObject = Instantiate(manager.currentWeapon.prefab, manager.WeaponHolderR);
    }

    public void DestroyWeapon()
    {
        Destroy(manager.currentWeaponObject);
    }

    public void UpdateWeaponState()
    {
        hud.UpdateWeaponState();
    }
}
