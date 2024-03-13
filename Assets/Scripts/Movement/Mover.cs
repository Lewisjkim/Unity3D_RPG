using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        
        NavMeshAgent navMeshAgent;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
            //Debug.DrawRay(ray.origin, ray.direction * 100);
        }

        public void StartMoveAction(Vector3 dest)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(dest);
        }

        public void MoveTo(Vector3 dest)
        {
            GetComponent<NavMeshAgent>().destination = dest;
            navMeshAgent.isStopped = false;
            
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);//월드 속도를 로컬 속도로 변환
            float speed = localVelocity.z;//foward speed
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}
