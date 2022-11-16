using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class CallEffect : MonoBehaviour
{
    public VisualEffect myVFX;
    // Start is called before the first frame update
    void Start()
    {
        
        myVFX.gameObject.transform.DOMove(new Vector3(5, 5, 5), 2).OnComplete(() => { myVFX.Play(); });
    }
    
        
}
