using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Username input field.
    /// </summary>
    public InputField inputField;

    private void Start()
    {
        UserIdentity.Load();
        Debug.Log("Current username: " + UserIdentity.name);
        inputField.text = UserIdentity.name;
    }

    /// <summary>
    /// Gos to lobby.
    /// </summary>
    public void GoToLobby() {
		SceneManager.LoadSceneAsync ("Lobby");
	}
    /// <summary>
    /// Sets the user name.
    /// </summary>
    /// <param name="name"></param>
    public void SetUserName(string name)
    {
        UserIdentity.name = name;
        UserIdentity.Save();
    } 
}
