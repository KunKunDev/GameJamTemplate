using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Button m_playButton;
    public Button m_creditsButton;

	// Use this for initialization
	void Awake () {
        m_playButton.onClick.AddListener(delegate { ChangeScene.Instance.NextScene("Level1"); });
        m_creditsButton.onClick.AddListener(delegate { ChangeScene.Instance.NextScene("Credits"); });
    }
}
