using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	[SerializeField] private HomeMenuUI homeMenuUI;
	[SerializeField] private LevelManagerSO data;

	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		Setup();
	}

	private void Setup() {
		LoadingUI.Instance.Hide();
	}

	public void SetHomeUI(HomeMenuUI homeMenuUI) {
		this.homeMenuUI = homeMenuUI;
		homeMenuUI.OnSubmit += LoadSceneLevel;
	}

	public void ShowLevel() {
		homeMenuUI.LoadLevels(data);
	}

	public LevelSO CurrentLevelSelection { get; private set; }

	public void SetLevelData(LevelSO levelSO) {
		CurrentLevelSelection = levelSO;
	}

	public void LoadSceneLevel() {
		LoadingUI.Instance.Show();
		var scene_01 = SceneManager.LoadSceneAsync(SCENENAME_Per);
		var scene_02 = SceneManager.LoadSceneAsync(SCENENAME_Level1, LoadSceneMode.Additive);
		LoadScene(new AsyncOperation[] { scene_01, scene_02 });
	}

	public void LoadSceneMenu() {
		LoadingUI.Instance.Show();
		var scene = SceneManager.LoadSceneAsync(SCENENAME_Menu);
		LoadScene(new AsyncOperation[] { scene }, Setup);
	}

	public async void LoadScene(AsyncOperation[] scenes, Action OnFinish = null) {
		foreach (var scene in scenes) {
			scene.allowSceneActivation = false;
		}

		float value = 0;
		do {
			await Task.Delay(100);
			value = 0;
			foreach (var scene in scenes) {
				value += scene.progress;
			}

			LoadingUI.Instance.SetFill(value / scenes.Length);
		} while (value < 0.9f);

		await Task.Delay(2000);
		foreach (var scene in scenes) {
			scene.allowSceneActivation = true;
		}
		
		await Task.Delay(1000);
		LoadingUI.Instance.Hide();
		OnFinish?.Invoke();
	}

	private const string SCENENAME_Level1 = "Level1";
	private const string SCENENAME_Per = "PersistentScene";
	private const string SCENENAME_Menu = "MenuScene";
}