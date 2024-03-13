using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] Health m_hp = null;
        [SerializeField] RectTransform forward = null;
        void Update()
        {
            forward.localScale = new Vector3(m_hp.GetFraction(), 1, 1);
        }
    }
}
