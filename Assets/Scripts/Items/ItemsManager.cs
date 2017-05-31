﻿using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private const string usableItemsJsonFile = "../ManicDragonInsurrection/Assets/Resources/JSON Files/Items/usable-items.json";
    private const string equippableItemsJsonFile = "../ManicDragonInsurrection/Assets/Resources/JSON Files/Items/equippable-items.json";
    private List<UsableItem> usableItems = new List<UsableItem>();
    private List<EquippableItem> exuippableItems = new List<EquippableItem>();

    void Awake()
    {
        LoadUsableItemsListFromFile(usableItemsJsonFile);
        //Debug.Log("Usable items count: " + usableItems.Count);
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
            //List<RankingRow> list = JsonMapper.ToObject<List<RankingRow>>(data["Ranking"].ToString());
            //BuildingsLevel levels = JsonMapper.ToObject<BuildingsLevel>(data["Buildings"].ToString());
            //data["album"]["name"]
            string json = File.ReadAllText(path);
            JsonData data = JsonMapper.ToObject(json);

            foreach (JsonData elem in data["usable"])
            {
                UsableItem item = new UsableItem(elem["Name"].ToString(), elem["Description"].ToString());
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
                usableItems.Add(item);
            }
        }
        catch (Exception e)
        {
            Debug.Log("LoadUsableItemsListFromFile exeption: " + e);
        }
    }
}
