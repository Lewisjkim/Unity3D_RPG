using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistPrefab;

        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistObjects();

            hasSpawned = true;
        }

        private void SpawnPersistObjects()
        {
            GameObject persistentObject = Instantiate(persistPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
