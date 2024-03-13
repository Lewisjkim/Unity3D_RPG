using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isPlayed = false;
        GameObject player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //Delegate를 통해 trigger 됐을 시 액션 조절
            GetComponent<PlayableDirector>().played += CancelPlayerAction;
            GetComponent<PlayableDirector>().stopped += EnablePlayerAction;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayed && other.gameObject.tag == "Player")
            {
                isPlayed = true;
                GetComponent<PlayableDirector>().Play();
                //CancelPlayerAction(other.GetComponent<PlayableDirector>());
            }
        }

        private void CancelPlayerAction(PlayableDirector obj)
        {
            //print("Cancel Player Action");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnablePlayerAction(PlayableDirector obj)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
