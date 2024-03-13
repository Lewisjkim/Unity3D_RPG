
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] string ItemName;
    public Sprite ItemImage;
}
