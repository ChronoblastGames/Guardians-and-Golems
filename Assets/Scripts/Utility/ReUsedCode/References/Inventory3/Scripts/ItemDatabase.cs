using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

//public class ItemDatabase : MonoBehaviour
//{
//    //private List<itemTest>database =new List<itemTest>();
//    private List<Item> database = new List<Item>();
//    private JsonData itemData;

//    void Start()
//    {
//        /*Item item = new Item(0, "Sword", 5);
//        database.Add(item);
//        Debug.Log(database[0].Title);*/
//        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));
//        ConstructItemDatabse();

//        Debug.Log(database[0].Title);
//        Debug.Log(FetchItemByID(0));
//        // Item item = new Item();
//    }

//    public Item FetchItemByID(int id)
//    {
//        for (int i = 0; i < database.Count; i++)

//            if (database[i].ID == id)
//                return database[i];

//        return null;

//    }

//    void ConstructItemDatabse()
//    {
//        for (int i = 0; i < itemData.Count; i++)
//        //for (int i = 0; i< 2; i++)
//        {
//            //database.Add(new itemTest())
//            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"], itemData[i]["slug"].ToString()));
//        }
//    }
//}


//public class Item
//{
//    public int ID { get; set; }
//    public string Title { get; set; }
//    public int Value { get; set; }
//    public string Slug { get; set; }
//    public Sprite Sprite { get; set; }
//    public string itemName { get; internal set; }

//    public Item(int id, string title, int value, string slug)
//    {
//        this.ID = id;
//        this.Title = title;
//        this.Value = value;
//        this.Slug = slug;
//        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
//    }

//    public Item()
//    {
//        this.ID = -1;
    

//    }
//}