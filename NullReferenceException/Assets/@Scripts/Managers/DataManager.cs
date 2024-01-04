using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager {

    public Dictionary<string, CreatureData> Creatures = new();
    public Dictionary<string, ItemData> Items = new();
    public Dictionary<string, SkillData> Skils = new();
    public Dictionary<string, SkillData> PlayerSkils = new();

    public void Initialize() {
        Creatures = LoadJson<CreatureData>();
        Items = LoadJson<ItemData>();
        Skils = LoadJson<SkillData>();
    }

    private Dictionary<string, T> LoadJson<T>() where T : Data {
        return JsonConvert.DeserializeObject<List<T>>(Main.Resource.Load<TextAsset>($"{typeof(T).Name}").text).ToDictionary(data => data.Key);
    }
}