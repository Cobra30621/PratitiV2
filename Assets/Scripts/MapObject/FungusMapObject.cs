using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusMapObject : MonoBehaviour
{

    public GameObject blur;
    public GameObject storyImage;
    public GameObject black;

    private GameObject[] Images;

    public void SetBlur(bool boolen)
    {
        storyImage.SetActive(boolen);
        //CloseAllImage(); 
        blur.SetActive(boolen);
    }

    public void SetBlack(bool boolen)
    {
        storyImage.SetActive(boolen);
        //CloseAllImage();
        black.SetActive(boolen);
    }


    public void CloseAllImage()
    {
        Images = GameObject.FindGameObjectsWithTag("StoryPicture");

        foreach (GameObject image in Images)
        {
            image.SetActive(false);
        }
    }
}


