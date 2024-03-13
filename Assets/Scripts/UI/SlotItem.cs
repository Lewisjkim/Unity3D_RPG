using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SlotItem : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] Item item = null;

        private void Update()
        {
            if(item == null)
            {
                Color color = itemImage.color;
                color.a = 0;
                itemImage.color = color;
            }
            else
            {
                itemImage.sprite = item.ItemImage;
                Color color = itemImage.color;
                color.a = 1;
                itemImage.color = color;
            }
            
        }
        public void SetItem(Item m_item)
        {
            item = m_item;
        }
    }
}
