using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuParent = null;
    [SerializeField] private GameObject startMenu = null;

    private GameObject activeMenu;

    private void Awake()
    {
        startMenu.SetActive(true);
        activeMenu = startMenu.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuParent.SetActive(!menuParent.activeSelf);
            Cursor.visible = menuParent.activeSelf;
            if (menuParent.activeSelf)
                OpenMenu(startMenu);
        }
    }

    public void Play() => StartCoroutine(StartGame());

    public IEnumerator StartGame()
    {
        yield return SceneManager.UnloadSceneAsync("Game");
        yield return SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

        menuParent.SetActive(false);
    }

    public void OpenMenu(GameObject menu)
    {
        activeMenu.SetActive(false);
        menu.SetActive(true);
        activeMenu = menu;
    }

    public void CloseMenu()
    {
        menuParent.SetActive(false);
        Cursor.visible = false;
    }

    public void Back()
    {
        if (activeMenu == startMenu)
        {
            menuParent.SetActive(false);
            Cursor.visible = false;
        }
        else
            OpenMenu(startMenu);
    }
}
