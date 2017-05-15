using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.SceneCreator
{
    //Factory might be a little overkill given amount of prefabs to configure
    public abstract class LevelAbstractSpawner<InputType,ConfigType>
    {
        [Inject]
        SettingsInstaller.PrefabsConfig _prefabConfig;
        [Inject]
        protected ConfigType _configOfType;

        public IDictionary<InputType, GameObject> mapping = null;

        public List<GameObject> spawn(InputType[,] items)
        {
            if (mapping == null)
                mapping = populateMapping();
            List<GameObject> tilesList = new List<GameObject>();
            for (int m = 0; m < items.GetLength(0); m++)
            {
                for (int n = 0; n < items.GetLength(1); n++)
                {
                    GameObject instance = createNewTile(items, m, n);
                    if (instance != null)
                        tilesList.Add(instance);
                }
            }
            return tilesList;
        }

        private GameObject createNewTile(InputType[,] items, int m, int n)
        {
            GameObject prefab;
            try
            {
                prefab = mapping[items[m, n]];
            } catch (KeyNotFoundException)
            {
                Debug.LogWarning("Could not find mapping for " + items[m, n].ToString() + ". Skipping...");
                return null;
            }
            GameObject tileInstance = GameObject.Instantiate(prefab);
            //FIXME: Use GameObject reference instead of reflection
            tileInstance.transform.parent = GameObject.Find("TilesContainer").transform;
            tileInstance.transform.Translate(new Vector3(_prefabConfig.tileSpan * m, _prefabConfig.tileSpan * -n));
            return tileInstance;
        }

        protected abstract Dictionary<InputType, GameObject> populateMapping();


    }
}
