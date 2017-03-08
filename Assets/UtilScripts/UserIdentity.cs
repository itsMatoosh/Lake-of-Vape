using UnityEngine;
/// <summary>
/// Stores the user identity.
/// </summary>
public class UserIdentity
{
    /// <summary>
    /// Name of the user.
    /// </summary>
    public static string name;

    /// <summary>
    /// Saves the user identity.
    /// </summary>
    public static void Save()
    {
        PlayerPrefs.SetString("username", name);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// Loads the user identity.
    /// </summary>
    public static void Load()
    {
        name = PlayerPrefs.GetString("username", "Anonymous");
    }
}
