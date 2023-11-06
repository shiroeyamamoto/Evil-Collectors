using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private const string PlayerPrefs_Coin = "coin_data";
	
	public static GameManager Instance;

	[SerializeField] private HomeMenuUI homeMenuUI;
	[SerializeField] private LevelManagerSO data;


	public int Coin { get; private set; }
	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		GetDataSaved();
		Setup();
	}

	private void GetDataSaved()
	{
		Coin = 0;
		if (PlayerPrefs.HasKey(PlayerPrefs_Coin))
		{
			Coin = PlayerPrefs.GetInt(PlayerPrefs_Coin);
		}
	}
	
	public void UseCoin(int dataPrice)
	{
		Coin -= dataPrice;
		Save();
	}

	private void Save()
	{
		PlayerPrefs.SetInt(PlayerPrefs_Coin, Coin);
	}

	[ContextMenu("Debug_Add100Coin")]
	public void Debug_Add100Coin()
	{
		PlayerPrefs.SetInt(PlayerPrefs_Coin, 100);
	}
	
	

	private void Setup() {
		LoadingUI.Instance.Hide();
		this.homeMenuUI = FindObjectOfType<HomeMenuUI>();
		homeMenuUI.TurnOnSubmit();
	}

	public void ShowLevel() {
		homeMenuUI.LoadLevels(data);
	}

	public LevelSO CurrentLevelSelection { get; private set; }
	public List<ItemBase> CurrentItemsInventory { get; private set; }
	
	public void SetLevelData(LevelSO levelSO) {
		CurrentLevelSelection = levelSO;
	}
	public void SetItemsInventory(List<ItemBase> currentItemsInventory) {
		CurrentItemsInventory = currentItemsInventory;
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