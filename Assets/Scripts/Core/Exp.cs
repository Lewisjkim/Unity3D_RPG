using System.Collections;
using UnityEngine;

namespace RPG.Core
{
    public class Exp : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] int playerLevel = 1;
        [SerializeField] Health health = null;
        [Range(0, 100)] [SerializeField] float exp = 0f;
        [SerializeField] GameObject expParticle = null;
        
        void Update()
        {
            //player
        }

        public void GetExp(float expAmount)
        {
            exp += expAmount;
            if(exp >= 100)
            {
                exp = 0;
                playerLevel += 1;
                health.SetHP(100f);
                
                GameObject particle = Instantiate(expParticle, gameObject.transform.position, Quaternion.identity);
                Destroy(particle, 2f);
            }
        }

        public float GetExpFraction()
        {
            return exp / 100f;
        }
    }
}