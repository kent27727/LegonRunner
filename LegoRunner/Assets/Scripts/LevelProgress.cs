using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    public UIAnimations FinishUI;
    [SerializeField] Transform Player; //player position
    [SerializeField] Transform EndLine; //endLine posiiton
    [SerializeField] Slider slider; //slider UI
    public float maxDistance; //distance between player and endLine
    public int fullSlider;
    public int increaseSlider;
    public int firstLevel;
    public int secondLevel;
    
    public Text levelText1;
    public Text levelText2;
    void Start()
    {

        maxDistance = getDistance();
        firstLevel = 1;
        secondLevel = 2;
        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        
        if ((slider.value == slider.maxValue || slider.value>=0.95 ) && Player.gameObject.GetComponent<MainController>().Winner)
        {
            FinishUI.FinishCeleb();
            firstLevel+=1;
            secondLevel+=1;
            levelText1.text = firstLevel.ToString();
            levelText2.text = secondLevel.ToString();
            slider.value = 0;
            
            
        }
        else
        {
            if (Player.position.x <= maxDistance && Player.position.x <= EndLine.position.x && !Player.gameObject.GetComponent<MainController>().Winner)
            {
                float distance = 1 - (getDistance() / maxDistance);
                setProgress(distance);
            }
        }
    }

    float getDistance()
    {
        return Vector2.Distance(Player.position, EndLine.position);
    }

    void setProgress(float p)
    {
        slider.value = p;
        
    }

    
}
