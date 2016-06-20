using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button upgradesBtn;

	void Start ()
    {
	    Button[] _buttons = GetComponentsInChildren<Button>();

        IEnumerable<Button> filteringButtons =
           from button in _buttons
           where button.name == "StartBtn"
           select button;

        Button startBtn = filteringButtons.ElementAt(0);
        Text label = startBtn.GetComponentInChildren<Text>();
        label.text = (string)LocaleModel.Instance.GetString("PLAY");
        startBtn.onClick.AddListener(LoadGameScene);

        label = upgradesBtn.GetComponentInChildren<Text>();
        label.text = (string)LocaleModel.Instance.GetString("UPGRADES");
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(SceneId.GAME);
    }

    public void OnUpgradesClick()
    {
        Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.UpgradesWindow);
    }
}
