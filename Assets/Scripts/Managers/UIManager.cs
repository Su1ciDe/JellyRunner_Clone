public class UIManager : Singleton<UIManager>
{
	public MoneyUI MoneyUI => moneyUI ? moneyUI : moneyUI = GetComponentInChildren<MoneyUI>(true);
	private MoneyUI moneyUI;
	public TapToStart TapToStartScreen => tapToStartScreen ? tapToStartScreen : tapToStartScreen = GetComponentInChildren<TapToStart>(true);
	private TapToStart tapToStartScreen;
	public LevelSuccessScreen LevelSuccessScreen => levelSuccessScreen ? levelSuccessScreen : levelSuccessScreen = GetComponentInChildren<LevelSuccessScreen>(true);
	private LevelSuccessScreen levelSuccessScreen;
	public LevelFailScreen LevelFailScreen => levelFailScreen ? levelFailScreen : levelFailScreen = GetComponentInChildren<LevelFailScreen>(true);
	private LevelFailScreen levelFailScreen;

	private void Awake()
	{
		TapToStartScreen.SetActive(true);
	}

	private void OnEnable()
	{
		LevelManager.OnLevelStart += OnLevelStart;
		LevelManager.OnLevelSuccess += OnLevelSuccess;
		LevelManager.OnLevelFail += OnLevelFail;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelStart -= OnLevelStart;
		LevelManager.OnLevelSuccess -= OnLevelSuccess;
		LevelManager.OnLevelFail -= OnLevelFail;
	}

	private void OnLevelStart()
	{
	}

	private void OnLevelSuccess()
	{
		LevelSuccessScreen.SetActive(true);
	}

	private void OnLevelFail()
	{
		LevelFailScreen.SetActive(true);
	}
}