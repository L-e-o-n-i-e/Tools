using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] swimSprites = new Sprite[6];
    #region Animation
    private SpriteRenderer spRr;
    private float animSpeed = .2f;
    private float timeToSwitchSprite;
    private int currentSpriteIndex = 0;
    #endregion
    void Start()
    {
        spRr = transform.GetComponent<SpriteRenderer>();
        timeToSwitchSprite = animSpeed + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (Time.time >= timeToSwitchSprite)
        {
            currentSpriteIndex++;
            currentSpriteIndex = currentSpriteIndex % 6;
            spRr.sprite = swimSprites[currentSpriteIndex];
            timeToSwitchSprite = animSpeed + Time.time;
        }
    }
}
