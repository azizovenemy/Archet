using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;
using Newtonsoft.Json;
using System.Linq;

public class ExampleStorageService : MonoBehaviour
{
    private const string key = "EXAMPLE_KEY";
    private IStorageService storageService;

    private void Start()
    {
        storageService = new JsonToFileStorageService();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ExampleStorageItem e = new ExampleStorageItem();
            e.StringParameter = "example";
            e.DictionaryParametr = new Dictionary<string, int>
            {
                {"part1", 10},
                {"part2", 60},
                {"part3", 40}
            };

            storageService.Save(key, e);
            Debug.Log($"Data saved successfull");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            storageService.Load<ExampleStorageItem>(key, e =>
            {
                Debug.Log($"Loaded. string {e.StringParameter}, " +
                    $"dictionary[0]: {e.DictionaryParametr.Keys.ElementAt(0)}: {e.DictionaryParametr.Values.ElementAt(0)}" +
                    $"dictionary[1]: {e.DictionaryParametr.Keys.ElementAt(1)}: {e.DictionaryParametr.Values.ElementAt(1)}" +
                    $"dictionary[2]: {e.DictionaryParametr.Keys.ElementAt(2)}: {e.DictionaryParametr.Values.ElementAt(2)}");
            });
        }
    }
}

public class ExampleStorageItem
{
    [JsonProperty(PropertyName = "str")]
    public string StringParameter { get; set; }
    [JsonProperty(PropertyName = "dic")]
    public Dictionary<string, int> DictionaryParametr { get; set; }
}
