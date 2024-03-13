using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class ExpBar : MonoBehaviour
    {
        [SerializeField] Exp m_exp = null;
        [SerializeField] RectTransform forward = null;

        void Update()
        {
            forward.localScale = new Vector3(m_exp.GetExpFraction(), 1, 1);
        }
    }
    
}
