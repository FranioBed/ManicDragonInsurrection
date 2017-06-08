using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsManager : MonoBehaviour
{
    private const string usableItemsJsonFile = "../ManicDragonInsurrection/Assets/Resources/JSON Files/Items/usable-items.json";
    private const string equippableItemsJsonFile = "../ManicDragonInsurrection/Assets/Resources/JSON Files/Items/equippable-items.json";
    private static List<UsableItem> usableItems = new List<UsableItem>();
    public List<EquippableItem> exuippableItems = new List<EquippableItem>();

    void Awake()
    {
        LoadUsableItemsListFromFile(usableItemsJsonFile);
        Debug.Log("Usable items count: " + usableItems.Count);
        LoadEquippableItemsListFromFile(equippableItemsJsonFile);
        //Debug.Log("Equippable items count: " + exuippableItems.Count);
    }

    private void LoadEquippableItemsListFromFile(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            JsonData data = JsonMapper.ToObject(json);
            foreach (JsonData elem in data["equippable"])
            {
                EquippableItem item = new EquippableItem(
                    elem["Name"].ToString(),
                    Int32.Parse(elem["UniqueId"].ToString()),
                    elem["Description"].ToString(),
                    (EquippableItem.EquippableType)(int)elem["Type"],
                    elem["SpritePath"].ToString());

                LoadFeatures(elem, item);
                exuippableItems.Add(item);
            }
        }
        catch (Exception e)
        {
            Debug.Log("LoadUsableItemsListFromFile exeption: " + e);
        }
    }

    private void LoadUsableItemsListFromFile(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            JsonData data = JsonMapper.ToObject(json);

            foreach (JsonData elem in data["usable"])
            {
                UsableItem item = new UsableItem(
                    elem["Name"].ToString(),
                    Int32.Parse(elem["UniqueId"].ToString()),
                    elem["Description"].ToString(),
                    elem["SpritePath"].ToString());
                
                LoadFeatures(elem, item);
                usableItems.Add(item);
            }
        }
        catch (Exception e)
        {
            Debug.Log("LoadUsableItemsListFromFile exeption: " + e);
        }
    }

    private void LoadFeatures(JsonData elem, Item item)
    {
        foreach (JsonData feature in elem["Features"])
        {
            if (feature["Type"].Equals((int)Feature.FeatureTypes.Instant))
            {
                item.features.Add(new InstantStatModFeature(
                    float.Parse(feature["Amount"].ToString()),
                    (StatTypes)(int)feature["Stat"],
                    Feature.FeatureTypes.Instant
                    ));
                //TODO: czemu to nie dziala? przez enumy?
                //item.features.Add(JsonMapper.ToObject<InstantStatModFeature>(feature.ToString()));
                //TODO: solution?:
                //JsonMapper.RegisterExporter<float>((obj, writer) => writer.Write(Convert.ToDouble(obj)));
                //JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
            }
            else if (feature["Type"].Equals((int)Feature.FeatureTypes.Continues))
            {
                item.features.Add(new ContinuesStatModFeature(
                    float.Parse(feature["Duration"].ToString()),
                    float.Parse(feature["Interval"].ToString()),
                    float.Parse(feature["Amount"].ToString()),
                    (StatTypes)(int)feature["Stat"],
                    Feature.FeatureTypes.Continues));
                //item.features.Add(JsonMapper.ToObject<ContinuesStatModFeature>(feature.ToString()));
            }
            else if (feature["Type"].Equals((int)Feature.FeatureTypes.Temp))
            {
                item.features.Add(new TempStatModFeature(
                    float.Parse(feature["Duration"].ToString()),
                    float.Parse(feature["Amount"].ToString()),
                    (StatTypes)(int)feature["Stat"],
                    Feature.FeatureTypes.Temp));
                //item.features.Add(JsonMapper.ToObject<TempStatModFeature>(feature.ToString()));
            }
        }
    }

    public static UsableItem GetRandomUsableItem()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        int rand = (int)Random.Range(0, usableItems.Count);
        return usableItems[rand];
    }
}
