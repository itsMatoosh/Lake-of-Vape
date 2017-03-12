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
	/// <summary>
	/// The lan match button holder.
	/// </summary>
	public Transform lanMatchButtonHolder;
	/// <summary>
	/// Match list transform.
	/// </summary>
	public RectTransform matchList;
	/// <summary>
	/// The direct connect window.
	/// </summary>
	public RectTransform directConnect;
	/// <summary>
	/// The lan match list.
	/// </summary>
	public RectTransform lanMatchList;

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
	/// Refreshs the lan matches.
	/// </summary>
	public void RefreshLanMatches() {
		MatchMaker.instance.FindLanMatches ();
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
	/// Adds the lan match button.
	/// </summary>
	/// <param name="address">Address.</param>
	/// <param name="info">Info.</param>
	public void AddLanMatchButton (string address, string info) {
        if (lanMatchButtonHolder == null) return;
        foreach (Transform t in lanMatchButtonHolder) {
			if (t.GetComponent<MatchButton> ().address == address)
				return;
		}
		((GameObject)Instantiate (matchButtonPrefab, lanMatchButtonHolder)).GetComponent<MatchButton>().Setup(address, info);
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
	/// <summary>
	/// Clears the lan match list.
	/// </summary>
	internal void ClearLanMatchList() {
		foreach (Transform button in lanMatchButtonHolder) {
			Destroy (button.gameObject);
		}
	}

	public void ShowMatchList() {
		HideLanMatchList ();
		HideDirectConnection ();
		matchList.gameObject.SetActive (true);
	}
	public void ShowLanMatchList() {
		HideDirectConnection ();
		HideMatchList ();
		lanMatchList.gameObject.SetActive (true);
	}
	public void HideLanMatchList() {
		lanMatchList.gameObject.SetActive (false);
	}
	public void HideMatchList() {
		matchList.gameObject.SetActive (false);
	}
	public void ShowDirectConnection() {
		HideLanMatchList ();
		HideMatchList ();
		directConnect.gameObject.SetActive (true);
	}
	public void HideDirectConnection() {
		directConnect.gameObject.SetActive (false);
	}
}
