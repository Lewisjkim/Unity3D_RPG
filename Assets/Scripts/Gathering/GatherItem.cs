using RPG.Control;
using System.Collections;
using UnityEngine;

namespace RPG.Gathering
{
    public class GatherItem : MonoBehaviour
    {
        [SerializeField] float gatheringTime = 1f;
        bool ItemGatherable = true;
        bool GatheringStarted = false;

        void Update()
        {
            if(GatheringStarted)
            {
                gatheringTime -= Time.deltaTime;

            }

            if(gatheringTime <= 0)
            {
                PlayerController controller = FindObjectOfType<PlayerController>();
                controller.SetAsItem(false);
                Destroy(this.gameObject);
            }
        }

        public bool Gatherable()
        {
            return ItemGatherable;
        }

        public void StartCollecting()
        {
            print("StartCollecting");
            GatheringStarted = true;
        }
    }
}