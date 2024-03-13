using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        Transform m_Target = null;

        void LateUpdate() 
        {
            transform.position = m_Target.position;
        }
    }
}

