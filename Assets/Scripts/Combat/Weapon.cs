using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float damage = 1;
        [SerializeField] float weaponRange = 2;
        [SerializeField] float timeBetweenAttacks = 0;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] string weaponName = null;

        public void DestroyWeapon(Transform righthandPos, Transform lefthandPos)
        {
            Transform oldWeapon = righthandPos.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = lefthandPos.Find(weaponName);
                if (oldWeapon == null)
                    return;
            }
            Destroy(oldWeapon.gameObject);

        }
        public void Spawn(Transform righthandPos, Transform lefthandPos, Animator animator)
        {
            DestroyWeapon(righthandPos, lefthandPos);

            if (weaponPrefab != null)
            {
                Transform handTransform = GetHandPos(righthandPos, lefthandPos);

                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                
                weapon.name = weaponName;
            }
            if (weaponOverride != null)
            {
                animator.runtimeAnimatorController = weaponOverride;
            }
        }

        private Transform GetHandPos(Transform righthandPos, Transform lefthandPos)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = righthandPos;
            else handTransform = lefthandPos;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void FireProjectile(Transform righthand, Transform lefthand, Health target)
        {
            Projectile projectileInst = Instantiate(projectile, 
                GetHandPos(righthand, lefthand).position,Quaternion.identity);
            projectile.SetTarget(target, GetDamage());
            
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetTimeBetweenAttacks()
        {
            return timeBetweenAttacks;
        }

    }
}