using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private Image uiImage;

    void Awake()
    {
        uiImage = GetComponent<Image>();
    }

    
    public void SetImage(string imageName)
    {
        if (string.IsNullOrEmpty(imageName))
        {
            uiImage.sprite = null;
        }
        else
        {
            imageName = "Assets/" + imageName + ".png";
            
            Sprite loadedSprite = Resources.Load<Sprite>(imageName);

            
            if (loadedSprite != null)
            {
                uiImage.sprite = loadedSprite;
            }
            else
            {
                Debug.LogError("Image not found: " + imageName);
                uiImage.sprite = null;
            }
        }
    }
}
