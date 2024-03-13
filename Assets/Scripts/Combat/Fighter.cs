using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        
        [SerializeField] Transform righthandTransform = null;
        [SerializeField] Transform leftthandTransform = null;
        [SerializeField] Weapon weapon = null;
        [SerializeField] Weapon weapon2 = null;
        [SerializeField] Weapon weapon3 = null;
        Weapon currentWeapon = null;
        int currentWeaponIndex = 1;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            EquipWeapon(weapon);
        }


        private void Update()
        {
            timeSinceLastAttack+= Time.deltaTime;
            
            //weaponSwap
            if (gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    if(currentWeaponIndex ==1)
                    {
                        weapon.DestroyWeapon(righthandTransform, leftthandTransform);
                        EquipWeapon(weapon2);
                        currentWeaponIndex++;
                    }
                    else if(currentWeaponIndex == 2)
                    {
                        weapon2.DestroyWeapon(righthandTransform, leftthandTransform);
                        EquipWeapon(weapon3);
                        currentWeaponIndex++;
                    }
                    else
                    {
                        weapon3.DestroyWeapon(righthandTransform, leftthandTransform);
                        EquipWeapon(weapon);
                        currentWeaponIndex = 1;
                    }
                }
            }

            if (target == null) return;

            if(target.IsDead())
            {
                return;
            }

            if(!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }


        public void EquipWeapon(Weapon m_weapon)
        {
            if (m_weapon == null) return;
            Animator animator = GetComponent<Animator>();
            m_weapon.Spawn(righthandTransform, leftthandTransform, animator);
            currentWeapon = m_weapon;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > currentWeapon.GetTimeBetweenAttacks())
            {
                TriggerAttack();
                ResetAttackTime();
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }


        void Hit() // Animation Event
        {
            if(target == null) return;
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.FireProjectile(righthandTransform, leftthandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        void Shoot()
        {
            Hit();
        }

        private void ResetAttackTime()
        {
            timeSinceLastAttack = 0f;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combattarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combattarget.GetComponent<Health>();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        
    }
}