using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectFlip : MonoBehaviour
{
    [Range(0, 5)] public float Speed;
    public SpriteRenderer SpriteRenderer;
    public AnimationCurve AnimationCurve;

    private bool isFlipping;
    private float flipValue = 0;
    private Sprite toSprite;

    public void Init()
    {
        isFlipping = false;
    }

    public void Flip(Sprite _fromSprite, Sprite _toSprite)
	{
        flipValue   = 0;
		transform.localEulerAngles = new Vector3(0, 0, 0);
        isFlipping = true;

        SpriteRenderer.sprite = _fromSprite;
        toSprite = _toSprite;
    }

    public void StopFliping()
    {
        flipValue = 0;
        isFlipping  = false;
        SpriteRenderer.sprite = toSprite;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void LateUpdate () 
	{
		if(isFlipping)
		{
			flipValue += (Time.deltaTime * Speed);
            flipValue  = Math.Max(flipValue, 0.0f);
            flipValue  = Math.Min(flipValue,1.0f);

			if(flipValue >= 1.0f)
			{
                StopFliping();
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, AnimationCurve.Evaluate(flipValue) * 90, 0);

                if (flipValue >= 0.5f)
                    SpriteRenderer.sprite = toSprite;
            }
        }
 	}
}
