using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    
    public static GameStateManager Instance { get; private set; }

    private Dictionary<string, bool> flags;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            flags = new Dictionary<string, bool>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFlag(string key, bool value)
    {
        flags[key] = value;
    }
    public bool GetFlag(string key)
    {
        return flags.ContainsKey(key) && flags[key];
    }
}
