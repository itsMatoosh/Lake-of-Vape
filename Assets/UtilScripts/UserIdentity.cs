using UnityEngine;
/// <summary>
/// Stores the user identity.
/// </summary>
public class UserIdentity
{
    private static string _name;
    /// <summary>
    /// Name of the user.
    /// </summary>
    public static string name
    {
        get
        {
            if(_name == null)
            {
                Load();
            }
            return _name;
        }
        set
        {
            _name = name;
            Save();
        }
    }

    /// <summary>
    /// Saves the user identity.
    /// </summary>
    public static void Save()
    {
        PlayerPrefs.SetString("username", _name);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// Loads the user identity.
    /// </summary>
    public static void Load()
    {
        _name = PlayerPrefs.GetString("username", "Anonymous");
    }
}
