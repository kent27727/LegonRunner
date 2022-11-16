using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneManagement : MonoBehaviour
{
    public GameObject ButtonList;
    public GameObject ButtonPrefab;
    public GameObject ButtonPrefabEngineLevel;
    public MainController character;
    public List<GameObject> ButtonObjectList;
    public List<Image> buttonImages;
    public int imageIndex = 0;
    public GameObject tapToStartText;
    private string ButtonPath = "ICON/activeButton";

    void Start()
    {
        if (character.levelIndex == 5)
        {
            ButtonPrefab = ButtonPrefabEngineLevel;
            ButtonPath = "ICON/activeButtonEngine";
        }
        if (tapToStartText.activeInHierarchy)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(tapToStartText.transform.DOScale(Vector3.one * 5, 1));
            mySequence.Append(tapToStartText.transform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), 1)).SetLoops(-1,LoopType.Restart);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		
    }
    public void addButtons()
	{
		if (!ButtonList.active)
		{
            ButtonList.active = true;
		}
		if (ButtonObjectList.Count >0)
		{
			for (int i = ButtonObjectList.Count-1; i >= 0; i--)
			{
                Destroy(ButtonObjectList[i]);
                ButtonObjectList.RemoveAt(i);
            }
        }
        int x = Random.Range(0,3);

		for (int i = 0; i < 3; i++)
		{
			if (i == x)
			{
                GameObject button = Instantiate(ButtonPrefab);
                button.transform.SetParent(ButtonList.transform);
                button.transform.localScale = Vector3.one*3.5f;
                button.GetComponent<Button>().onClick.AddListener(() => { 
                    character.GettingUpgrade(true); 
                    foreach (GameObject Button in ButtonObjectList)
                    {
                        
                        Button.GetComponent<Button>().enabled = false;
                        Button.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("ICON/normalButton");
                        button.GetComponent<Button>().image.sprite = Resources.Load<Sprite>(ButtonPath);
                    }
                });
                //button.GetComponent<Button>().image.color = Color.green;
                ButtonImageChanger(button.transform.GetChild(0).GetComponent<Image>(),true,false);
                ButtonObjectList.Add(button);
            }
			else
			{
                
                GameObject button = Instantiate(ButtonPrefab);
                button.transform.SetParent(ButtonList.transform);
                button.transform.localScale = Vector3.one * 3.5f;
                button.GetComponent<Button>().onClick.AddListener(() =>
				{
                    
                    character.FalseButton();
                    foreach (GameObject Button in ButtonObjectList)
					{
                        Button.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("ICON/normalButton");
					}
                    button.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("ICON/activeButton");
                });
				if (imageIndex == 0)
				{
                    ButtonImageChanger(button.transform.GetChild(0).GetComponent<Image>(), false, true);
                }
				else if (imageIndex == 1)
				{
                    ButtonImageChanger(button.transform.GetChild(0).GetComponent<Image>(), false, false);
                }
                
                ButtonObjectList.Add(button);
                imageIndex += 1;
            }
		}
        imageIndex = 0;
	}
    public void ButtonImageChanger(Image imageButton,bool isMainImage,bool isSecondImage)
	{
		if (isMainImage)
		{
            switch (character.currentSurface)
            {
                case "Surface1":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/001-chassis");
                    break;
                case "Surface2":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/biggerWheel");
                    break;
                case "Surface3":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/tiredWheel");
                    break;
                case "Surface4":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV6");
                    break;
                case "Surface5":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV8");
                    break;
                case "Surface6":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeSus");
                    break;
                case "Surface7":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/DowngradeSus");
                    break;
                case "Surface8":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/FrontWheel");
                    break;
                case "Surface9":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/BackWheel");
                    break;
                case "Surface10":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV12");
                    break;
                default:
                    break;
            }
        }
        else if (!isMainImage && isSecondImage)
        {
            switch (character.currentSurface)
            {
                case "Surface1":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/001-chassis");
                    break;
                case "Surface2":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/tiredWheel");
                    break;
                case "Surface3":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/biggerWheel");
                    break;
                case "Surface4":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV8");
                    break;
                case "Surface5":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV6");
                    break;
                case "Surface6":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/DowngradeSus");
                    break;
                case "Surface7":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeSus");
                    break;
                case "Surface8":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/BackWheel");
                    break;
                case "Surface9":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/FrontWheel");
                    break;
                case "Surface10":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV6");
                    break;
                default:
                    break;
            }
        }
        else if (!isMainImage && !isSecondImage)
        {
            switch (character.currentSurface)
            {
                case "Surface1":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/001-chassis");
                    break;
                case "Surface2":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/normalWheel");
                    break;
                case "Surface3":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/normalWheel");
                    break;
                case "Surface4":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV12");
                    break;
                case "Surface5":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV12");
                    break;
                case "Surface6":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/DowngradeSus");
                    break;
                case "Surface7":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeSus");
                    break;
                case "Surface8":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/BackWheel");
                    break;
                case "Surface9":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/FrontWheel");
                    break;
                case "Surface10":
                    imageButton.sprite = Resources.Load<Sprite>("ICON/UpgradeV8");
                    break;
                default:
                    break;
            }
        }



    }
    public void NextLevel(int LevelIndex)
	{
        SceneManager.LoadScene(LevelIndex);
	}
}
