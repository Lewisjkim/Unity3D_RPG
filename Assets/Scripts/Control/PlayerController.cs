using System;
using System.Collections;
using RPG.Combat;
using RPG.Core;
using RPG.Gathering;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour 
    {
        bool itemisSet = false;
        GameObject itemTarget;
        [SerializeField] GameObject Inventory;
        bool inventoryActive = false;

        enum CursorType
        {
            None,
            Attack,
            Move,
            Gather
        }
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType   type;
            public Texture2D    texture;
            public Vector2      hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                if(!inventoryActive)
                {
                    Inventory.SetActive(true);
                    inventoryActive = true;
                }
                else
                {
                    Inventory.SetActive(false);
                    inventoryActive = false;
                }
            }

            if(InteractWithCombat()) return;

            if(InteractWithGathering())return;
            if (itemisSet)
            {
                if (Vector3.Distance(transform.position, itemTarget.transform.position) > 3f)
                {
                    Mover mover = GetComponent<Mover>();
                    GetComponent<ActionScheduler>().StartAction(mover);
                    GetComponent<NavMeshAgent>().destination = itemTarget.transform.position;
                    GetComponent<NavMeshAgent>().isStopped = false;
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    GetComponent<GatheringSystem>().StartToGather(itemTarget);
                }
            }

            if (InteractWithMovement())return;
            SetCursor(CursorType.None);
        }

         private bool InteractWithCombat()
         {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                SetCursor(CursorType.Attack);
                return true;
            }
            return false;
         }


        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                SetCursor(CursorType.Move);
                return true;
            }
            return false;
        }

     
        private bool InteractWithGathering()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                GatherItem gtarget = hit.transform.GetComponent<GatherItem>();
                if (gtarget == null) continue;

                if (!GetComponent<GatheringSystem>().CanGather(gtarget.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    SetAsItem(true);
                    itemTarget = gtarget.gameObject;
                }
                SetCursor(CursorType.Gather);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMappingList(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMappingList(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        public void SetAsItem(bool set)
        {
            itemisSet = set;
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
