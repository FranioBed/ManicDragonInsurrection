using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private const string usableItemsJsonFile = "/Resources/JSON Files/Items/usable-items.json";
    private const string equippableItemsJsonFile = "/Resources/JSON Files/Items/equippable-items.json";
    private List<UsableItem> usableItems;
    private List<EquippableItem> exuippableItems;

    void Awake()
    {
        LoadUsableItemsListFromFile(usableItemsJsonFile);
        LoadEquippableItemsListFromFile(equippableItemsJsonFile);
    }

    private void LoadEquippableItemsListFromFile(string path)
    {
        throw new System.NotImplementedException();
    }

    private void LoadUsableItemsListFromFile(string path)
    {
        try
        {
            //{set; get;}; konstruktor, nazwy te same
            //List<RankingRow> list = JsonMapper.ToObject<List<RankingRow>>(data["Ranking"].ToString());
            //BuildingsLevel levels = JsonMapper.ToObject<BuildingsLevel>(data["Buildings"].ToString());
            //data["album"]["name"]
            string json = File.ReadAllText(path);
            JsonData data = JsonMapper.ToObject(json);
            data = JsonMapper.ToObject(data["usable"].ToString());

            foreach (JsonData elem in data)
            {
                UsableItem item = new UsableItem(elem["name"].ToString(), elem["description"].ToString());
                foreach (JsonData feature in data["features"])
                {
                    if (feature["type"].Equals((int)Feature.FeatureTypes.Instant))
                    {
                        item.features.Add(JsonMapper.ToObject<InstantStatModFeature>(feature.ToString()));
                    }
                    else if (feature["type"].Equals((int)Feature.FeatureTypes.Continues))
                    {
                        item.features.Add(JsonMapper.ToObject<ContinuesStatModFeature>(feature.ToString()));
                    }
                    else if (feature["type"].Equals((int)Feature.FeatureTypes.Temp))
                    {
                        item.features.Add(JsonMapper.ToObject<TempStatModFeature>(feature.ToString()));
                    }
                }
                usableItems.Add(item);
            }
        }
        catch (Exception e)
        {
            Debug.Log("LoadUsableItemsListFromFile exeption: " + e);
        }
    }
}
