using UnityEngine;

// holds all data related to items picked up.
// allows us to use prefabs for items instead of objects with broken
// references between scenes 

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    [TextArea]
    public string description;
}
