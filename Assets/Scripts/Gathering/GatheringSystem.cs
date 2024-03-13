using RPG.Core;
using RPG.Movement;
using RPG.UI;
using System.Collections;
using UnityEngine;

namespace RPG.Gathering
{
    public class GatheringSystem : MonoBehaviour, IAction
    {

        GatherItem target;
        [SerializeField] Item item = null;
        private void Update()
        {

            
        }

        public bool CanGather(GameObject GatheringItem)
        {
            if (GatheringItem == null) return false;
            GatherItem target = GatheringItem.GetComponent<GatherItem>();
            return target != null && target.Gatherable();
        }

        void Shoot()
        {
            if (target == null) return;
            target.StartCollecting();

            
        }

        void End()
        {
            SlotItem slot = FindObjectOfType<SlotItem>();
            slot.SetItem(item);
        }

        public void StartToGather(GameObject GatheringItem)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = GatheringItem.GetComponent<GatherItem>();
            //transform.LookAt(target.transform);
            GetComponent<Animator>().SetTrigger("gathering");
            
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("gathering");
            GetComponent<Mover>().Cancel();
        }
    }

    
}