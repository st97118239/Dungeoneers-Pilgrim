using UnityEngine;

public class Use : MonoBehaviour
{
    public float hp = 3;
    public GameObject selectedItem;
    public string itemTag;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (itemTag == "Weapon")
            {
                print("attack");
            }
        }

        if (hp <= 0)
        {
            print("dead");
        }
    }

    public void newItem(GameObject item)
    {
        selectedItem = item;
        itemTag = item.tag;
    }

    public void removeItem()
    {
        selectedItem = null;
        itemTag = null;
    }
}
