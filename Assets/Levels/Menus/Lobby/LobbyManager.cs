using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.Match;

public class LobbyManager : MonoBehaviour {
	/// <summary>
	/// The instance.
	/// </summary>
	public static LobbyManager instance;

	/// <summary>
	/// The match button prefab.
	/// </summary>
	public UnityEngine.Object matchButtonPrefab;
	/// <summary>
	/// The match button holder.
	/// </summary>
	public Transform matchButtonHolder;

	void Start() {
		//Listing all the active games.
		//TODO: Game name filtering.
		RefreshMatches();
	}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Refreshs the matches.
    /// </summary>
    public void RefreshMatches() {
		MatchMaker.instance.FindInternetMatch("");
	}
	/// <summary>
	/// Changes to the create match menu.
	/// </summary>
	public void CreateMatch() {
		SceneManager.LoadScene ("CreateMatch");
	}
	/// <summary>
	/// Adds the match button.
	/// </summary>
	/// <param name="info">Info.</param>
	public void AddMatchButton(MatchInfoSnapshot info) {
		((GameObject)Instantiate (matchButtonPrefab, matchButtonHolder)).GetComponent<MatchButton>().Setup(info);
	}

    /// <summary>
    /// Clears the match list.
    /// </summary>
    internal void ClearMatchList()
    {
        foreach(Transform button in matchButtonHolder)
        {
            Destroy(button.gameObject);
        }
    }
}
