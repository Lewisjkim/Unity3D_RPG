using RPG.Core;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        
        [SerializeField] float speed = 2;
        [SerializeField] Health target = null;
        [SerializeField] float damage = 0;
        
        
        // Update is called once per frame
        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
                

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();

            if(targetCollider == null)
            {
                Destroy(gameObject);
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCollider.height * 2f / 3f;
        }

        public void SetTarget(Health m_target, float bowdamage)
        {
            this.target = m_target;
            this.damage = bowdamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}