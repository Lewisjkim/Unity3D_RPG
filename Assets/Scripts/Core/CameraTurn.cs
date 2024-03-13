using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraTurn : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
