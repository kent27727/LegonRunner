using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class EffectManager : MonoBehaviour
{
    public VisualEffect[] TireVFX;
    [SerializeField]
    public Transform cameraToTween;
    

    public float cameraY;
    public float cameraX;
    public float cameraDuration;
    public float changeZ;
    public float targetDamp;
    public float targetDown;



    [SerializeField]
    public GameObject[] Wheels;
    public float duration;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEffect()
	{
		foreach (VisualEffect effect in TireVFX)
		{
            effect.Play();
		}
        
    }

   
    private void TweenCameraRot(GameObject cameraToTween)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(cameraToTween.transform.DOMoveZ(this.transform.position.z - changeZ, cameraDuration));
        mySequence.Append(cameraToTween.transform.DOMoveX(cameraX, cameraDuration));
        mySequence.Append(cameraToTween.transform.DOMoveY(cameraY, cameraDuration).OnComplete(() => { cameraToTween.transform.DOMoveZ(this.transform.position.z + changeZ, cameraDuration); }));
    }

    public void TweenScale(GameObject[] Wheels,float targetScale)
	{
		foreach (GameObject targetwheel in Wheels)
		{
            TweenParams tParms = new TweenParams().SetLoops(0).SetEase(Ease.OutElastic);
            targetwheel.transform.DOScale(targetScale, duration).SetAs(tParms);
        }
        
    }
    public void TweenMoveSuspensionUp(GameObject CarParts)
	{
        TweenParams tParms = new TweenParams().SetLoops(0).SetEase(Ease.OutElastic);
        CarParts.transform.DOMoveY(CarParts.transform.position.y + targetDamp,duration).SetAs(tParms);
	}
    public void TweenMoveSuspensionDown(GameObject CarParts)
    {
        TweenParams tParms = new TweenParams().SetLoops(0).SetEase(Ease.OutElastic);
        CarParts.transform.DOMoveY(CarParts.transform.position.y - targetDown, duration).SetAs(tParms);
    }

    public void TweenResetSuspension(GameObject CarParts,float yPos)
	{
        TweenParams tParms = new TweenParams().SetLoops(0).SetEase(Ease.OutElastic);
        CarParts.transform.DOMoveY(yPos, duration).SetAs(tParms);
    }
    public void TweenDoTo(WheelCollider[] wheelColliderList,float targetFloat,float durationTime)
	{
        foreach (WheelCollider item in wheelColliderList)
        {
            DOTween.To(() => item.suspensionDistance, x => item.suspensionDistance = x, targetFloat, durationTime);
        }


    }
}
