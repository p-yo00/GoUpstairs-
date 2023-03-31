using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameMenu : MonoBehaviour
{
    private int curNum = 0;
    public GameMenu next;
    public List<Texture2D> tuto_imgs;

    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void tutorial()
    {
        GameObject.Find("tuto_img").GetComponent<RawImage>().enabled = true;
        GameObject.Find("next").GetComponent<Image>().enabled = true;
        GameObject.Find("prev").GetComponent<Image>().enabled = true;
        GameObject.Find("x").GetComponent<Image>().enabled = true;
        GameObject.Find("tuto_img").GetComponent<RawImage>().texture = tuto_imgs[0];
    }

    public void nextButton()
    {
        RawImage page = GameObject.Find("tuto_img").GetComponent<RawImage>();
        next.curNum=(next.curNum+1)%6;
        page.texture = tuto_imgs[next.curNum];
    }
    public void prevButton()
    {
        RawImage page = GameObject.Find("tuto_img").GetComponent<RawImage>();
        if (next.curNum == 0)
            next.curNum = 5;
        else
            next.curNum = next.curNum - 1;
        page.texture = tuto_imgs[next.curNum];
    }
    public void tutorialExit()
    {
        next.curNum = 0;
        GameObject.Find("tuto_img").GetComponent<RawImage>().enabled = false;
        GameObject.Find("next").GetComponent<Image>().enabled = false;
        GameObject.Find("prev").GetComponent<Image>().enabled = false;
        GameObject.Find("x").GetComponent<Image>().enabled = false;
    }
}
