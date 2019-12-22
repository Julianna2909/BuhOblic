using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class PlayerPrefsExt
{
    static Hashtable _data = new Hashtable();
    static bool _loaded = false;
    static Action _onSave;

    const string FILE_PATH = "player_prefs.json";

    public static string DataToJSON { get { return _data.toJson(); } }
    public static void SetupData(Hashtable data)
    {
        _data = data;
    }

    public static void SetOnSave(Action onSave)
    {
        _onSave = onSave;
    }
    
    public static void Load()
    {
        if (_loaded) return;
        _loaded = true;

#if UNITY_EDITOR
        if (File.Exists(FILE_PATH)) _data = (Hashtable) MiniJSON.jsonDecode(File.ReadAllText(FILE_PATH));
#else
        if (PlayerPrefs.HasKey("Data")) _data = (Hashtable) MiniJSON.jsonDecode(PlayerPrefs.GetString("Data"));
#endif
    }

    public static void Save()
    {
        Load();        
        SetString("Date", DateTime.UtcNow.ToString(), false);

#if UNITY_EDITOR
        File.WriteAllText(FILE_PATH, _data.toJson());
#else
        PlayerPrefs.SetString("Data", _data.toJson());        
		PlayerPrefs.Save();
#endif
        if (_onSave != null)
        {
            _onSave();
        }
    }

    public static void SetInt(string key, int value, bool save = true)
    {
        Load();
        _data[key] = value;
        if (save) Save();
    }
    
    public static int GetInt(string key, int defaultValue = 0, bool setIfNull = false, bool save = true)
    {
        Load();

        if (_data.ContainsKey(key)) return _data.GetInt32(key);
        else
        {
            if (setIfNull) SetInt(key, defaultValue, save);
            return defaultValue;
        }
    }

    public static void SetFloat(string key, float value, bool save = true)
    {
        Load();
        _data[key] = value;
        if (save) Save();
    }

    public static float GetFloat(string key)
    {
        Load();
        return _data.GetFloat(key);
    }

    public static void SetString(string key, string value, bool save = true)
    {
        Load();
        _data[key] = value;
        if (save) Save();
    }

    public static string GetString(string key)
    {
        Load();
        return _data.GetString(key);
    }

    public static bool HasKey(string key)
    {
        Load();
        return _data.ContainsKey(key);
    }

    public static void DeleteKey(string key, bool save = true)
    {
        Load();
        _data.Remove(key);
        if (save) Save();
    }

    public static void DeleteAll(bool save = true)
    {
        Load();
        _data.Clear();
        if (save) Save();
    }
}
