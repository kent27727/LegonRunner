using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    
    public GameObject TextObject;
    public GameObject FinishTextObject;
    public string[] satisfyTexts;
    public string[] wrongTexts;
    public string nowText;
    TextMeshProUGUI FinishText;
    TextMeshProUGUI textOBJtext;
    void Start()
    {
        int index = Random.Range(0, satisfyTexts.Length);
        nowText = satisfyTexts[index];
        textOBJtext = TextObject.GetComponent<TextMeshProUGUI>();
        textOBJtext.text = nowText;
        FinishText = FinishTextObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Celebrate()
	{
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(TextObject.transform.DOScale(Vector3.one, 1));
        mySequence.Append(TextObject.transform.DOScale(Vector3.zero, 1).OnComplete(() =>
        {
            int index = Random.Range(0, satisfyTexts.Length);
            nowText = satisfyTexts[index];
            textOBJtext.text = nowText;
        }));

        
        
    }

    public void FalseAction()
	{
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(TextObject.transform.DOScale(Vector3.one, 1));
        mySequence.Append(TextObject.transform.DOScale(Vector3.zero, 1).OnComplete(() =>
        {
            int index = Random.Range(0, wrongTexts.Length);
            nowText = wrongTexts[index];
            textOBJtext.text = nowText;
        }));
    }

    public void FinishCeleb()
    {
        Sequence finishSequence = DOTween.Sequence();
        finishSequence.Append(FinishTextObject.transform.DOScale(Vector3.one, 1));
    }
}
