using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum Destination
        {
            None = 0,
            Forest,
            ForestToMine,
            BossToMine,
            Boss
        }

        [SerializeField] int DestSceneIndex = 0;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destination DestID;
        [SerializeField] int portalNum;
        [SerializeField] int DestportalNum;
        [SerializeField] float FadeOutTime = 1f;
        [SerializeField] float FadeInTime = 1f;
        [SerializeField] float FadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            //Scene currentScene = SceneManager.GetActiveScene();
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(FadeOutTime);

            yield return SceneManager.LoadSceneAsync(DestSceneIndex);
            
            Portal spawnPortal = GetSpawnPortal();
            UpdatePlayer(spawnPortal);

            yield return new WaitForSeconds(FadeWaitTime);

            yield return fader.FadeIn(FadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal spawnPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPortal.spawnPoint.position;
            player.transform.rotation = spawnPortal.spawnPoint.rotation;
        }

        private Portal GetSpawnPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.DestID == this.DestID) continue;
                if (portal.portalNum != this.DestportalNum) continue;
                return portal;
            }
            return null;
        }
    }
}
