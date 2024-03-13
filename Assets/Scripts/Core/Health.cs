using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace RPG.Core
{
    public class Health : MonoBehaviour 
    {
        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {

        }

        [SerializeField] float m_healthpoint = 100f;
        [SerializeField] float m_healthpointMax = 100f;

        [SerializeField] TakeDamageEvent takeDamage;
        

        bool isDead = false;
        float time = 0f;

       

        public void Update()
        {
            if(isDead)
            {
                //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                time += Time.deltaTime;
                if(time > 2f)
                {
                    if (gameObject.tag != "Player")
                    {
                        Exp exp = GameObject.FindGameObjectWithTag("Player").GetComponent<Exp>();
                        exp.GetExp(50f);
                    }
                    Destroy(this.gameObject);
                }
            }
        }

        public float GetHP()
        {
            return m_healthpoint;
        }

        public void SetHP(float hpAmount)
        {
            m_healthpoint += hpAmount;
            if (m_healthpoint >= 100f)
                m_healthpoint = 100f;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            m_healthpoint = Mathf.Max(m_healthpoint - damage, 0);
            
            if (m_healthpoint == 0)
            {
                Die();
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return m_healthpoint / m_healthpointMax;
        }
    }
}