using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mapSelectionPanel;
    public GameObject PlayMenuPanel;
        void Start()
        {
            mapSelectionPanel.SetActive(false); // Ensure map selection is hidden at start
        }
    
        public void OnPlayButtonClicked()
        {
            PlayMenuPanel.SetActive(false); // Hide play menu
            mapSelectionPanel.SetActive(true); // Show map selection
        }
    
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName); // Load the specified scene
        }
}
