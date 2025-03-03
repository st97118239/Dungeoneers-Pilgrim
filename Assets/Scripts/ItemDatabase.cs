using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>(){
            new Item(0, "A", "Adesc",
            new Dictionary<string, int>
            {
                {"Atooltip1", 5},
                {"Atooltip2", 7}
            }),
            new Item(1, "B", "Bdesc",
            new Dictionary<string, int>
            {
                {"Btooltip1", 1},
                {"Btooltip2", 9}
            }),
            new Item(2, "C", "Cdesc",
            new Dictionary<string, int>
            {
                {"Ctooltip1", 2},
                {"Ctooltip2", 5}
            })
        };
    }
}
