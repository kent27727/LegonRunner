using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEditor;
using UnityEngine.VFX;
public class MainController : MonoBehaviour
{
	#region Variables
	//optimize et
	Rigidbody rb;
    
    public string currentSurface;

    public int levelIndex;
    [Header("Mechanical Values")]
    public float speed;
    public float upgradeSpeed;
    public float downgradeSpeed;
    public float suspensionHeightMax;
    public float suspensionHeightMin;


    [Header("Times Of Actions")]
    public float upgradeDurationTime;
    public float downgradeDurationTime;
    [Space(20)]

    public float yPosofCarParts;
    public float staticSpeed;

    bool isUpgraded = false;
    public bool currentUpgrading;
    
    public SceneManagement manager;
    public GameObject Enemy;
    public bool Winner;
    public NPCAI npcScript;
    CarController carController;
    
    public GameObject[] TireChains;
    public GameObject[] EngineParts;
    public WheelCollider[] WheelColliders;
    public GameObject CarParts;
    public bool getDownGrade = false;
    UIAnimations UIscript;
    EffectManager FXMan;
    public GameObject mainCamera;


    public GameObject winPanel;
    public GameObject losePanel;
    public ParticleSystem finishConfetti;

    public int notDowngradeIndex;
    public int notDowngradeIndexTarget;

    public Color[] effectColours;
    public VisualEffect[] wheelEffects;


    #endregion


    #region SystemComponents
    void Start()
    {
        npcScript.levelIndex = levelIndex;
        carController = GetComponent<CarController>();
        yPosofCarParts = CarParts.transform.position.y;
        rb = GetComponent<Rigidbody>();
        FXMan = GetComponent<EffectManager>();
        UIscript = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIAnimations>();
        staticSpeed = speed;
        npcScript = Enemy.GetComponent<NPCAI>();
        //Surfaces = GameObject.FindGameObjectsWithTag("Surface").OrderBy(go => go.name).ToArray();
    }

    void FixedUpdate()
    {
        switch (levelIndex)
        {
            case 1:
                giveRBComponent();
                break;
            case 2:

                EngineSystem();
                break;
            case 3:
                TractionSystem();
                break;
            case 5:
                EngineSystem();
                break;

            default:
                break;
        }
        if (rb.velocity.magnitude > speed && !currentUpgrading)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }


    }
    #endregion
    #region Systems
    void giveRBComponent()
	{
        if (npcScript.isWon)
        {
            carController.isBreaking = true;
            EndLevel();
        }
        else if (!Winner)
        {
            rb.velocity = new Vector3(speed, -5, 0);
        }

        else
        {
            carController.isBreaking = true;
            EndLevel();

        }
    }
	
	void SuspensionSystem()
	{

        if (npcScript.isWon)
        {
            rb.velocity = Vector3.zero;
            carController.isBreaking = true;
            EndLevel();
        }

        else
        {
            rb.velocity = Vector3.zero;


        }

    }
    void EngineSystem()
    {
        if (npcScript.isWon)
        {
            rb.velocity = Vector3.zero;
            carController.isBreaking = true;
            EndLevel();
        }
        else
        {
			//rb.velocity = Vector3.zero;
			//EndLevel();

		}
    }
    void TractionSystem()
    {
        if (npcScript.isWon)
        {
            rb.velocity = Vector3.zero;
            carController.isBreaking = true;

        }

        else
        {
            rb.velocity = Vector3.zero;

        }
    }
	#endregion


	#region TriggerControls
	private void OnTriggerEnter(Collider other)
	{
        switch (other.gameObject.name)
        {
            case "Surface1":
                currentSurface = other.gameObject.name;
                break;
            case "Surface2":
                currentSurface = other.gameObject.name;
                break;

            case "Surface3":
                currentSurface = other.gameObject.name;
                break;

            case "Surface4":
                currentSurface = other.gameObject.name;
                break;

            case "Surface5":
                currentSurface = other.gameObject.name;
                break;
            case "Surface6":
                currentSurface = other.gameObject.name;
                break;
            case "Surface7":
                currentSurface = other.gameObject.name;
                break;
            case "Surface8":
                currentSurface = other.gameObject.name;
                break;
            case "Surface9":
                currentSurface = other.gameObject.name;
                break;
            case "Surface10":
                currentSurface = other.gameObject.name;
                break;
            case "FinishLine":
                currentSurface = other.gameObject.name;
                Winner = true;
                EndLevel();
                break;
            case "StartNPC":
                StartNPC();
                break;
            case "Normal":
                currentSurface = null;
                break;
            default:
                currentSurface = null;
                break;
        }
		switch (other.tag)
		{
            case "Asphalt":
                effectColourChange(0);
                break;
            case "Sand":
                effectColourChange(1);
                break;
            case "Mud":
                effectColourChange(3);
                break;
            case "Snow":
                effectColourChange(2);
                break;
            case "BasicAsphalt":
                effectColourChange(4);
                break;


            default:
				break;
		}

	}
	
	private void OnTriggerExit(Collider other)
	{
        Debug.Log(other);
		if (other.name != "Engebe")
		{
            ResetObjectETC();
			if (currentSurface !=null)
			{
                manager.addButtons();
                
            }
            if (notDowngradeIndex != notDowngradeIndexTarget)
            {
                notDowngradeIndex += 1;
            }


        }
        
    }

	#endregion
	#region UpgradeSystem
	public void GettingUpgrade(bool surface)
	{
        getDownGrade = false;
        float lastSpeed = speed + upgradeSpeed;
        
        
		if (!isUpgraded && surface)
		{

            isUpgraded = true;
            Sequence mySequence = DOTween.Sequence();
            UIscript.Celebrate();
            CurrentEvent();

            currentUpgrading = true;
            mySequence.Append(DOTween.To(() => speed, x => speed = x, lastSpeed, upgradeDurationTime/2));
            mySequence.Append(DOTween.To(() => speed, x => speed = x, staticSpeed, upgradeDurationTime/2).OnComplete(() =>
            {
                currentUpgrading = false;
            }));
        }
        
    }
    public void GettingDowngrade()
    {


        if (getDownGrade)
        {

            
            DOTween.To(() => speed, x => speed = x, downgradeSpeed, downgradeDurationTime).OnComplete(()=> { });
            getDownGrade = false;

        }

    }
    #endregion

    void CurrentEvent()
    {
        switch (currentSurface)
        {
            case "Surface2":
                FXMan.TweenScale(FXMan.Wheels, 1.5f);
                FXMan.CallEffect();
                break;
            case "Surface3":
                FXMan.TweenScale(TireChains, 1f);
                FXMan.CallEffect();
                break;
            case "Surface4":
                //FXMan.TweenMoveSuspensionUp(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
                break;
            case "Surface5":
                //FXMan.TweenMoveSuspensionDown(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
                break;
            case "Surface6":
                //FXMan.TweenMoveSuspensionUp(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
                break;
            case "Surface7":
                //FXMan.TweenMoveSuspensionDown(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
                break;
            case "Surface8":
                //FXMan.TweenMoveSuspensionUp(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
                break;
            case "Surface9":
                //FXMan.TweenMoveSuspensionDown(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
                break;
            case "Surface10":
                //FXMan.TweenMoveSuspensionDown(CarParts);
                FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
                break;

            default:
                break;
        }
    }


    public void EndLevel()
	{
		if (!npcScript.isWon)
		{
            npcScript.isLose = true;
            winPanel.SetActive(true);
            finishConfetti.Play();
        }
		else
		{
            losePanel.SetActive(true);
        }

	}


    public void ResetObjectETC()
	{
        FXMan.TweenScale(FXMan.Wheels, 1f);
        FXMan.TweenScale(TireChains, 0f);
        FXMan.TweenScale(EngineParts, 1f);
        //FXMan.TweenResetSuspension(CarParts, yPosofCarParts);
    }


    public void FalseButton()
	{
        UIscript.FalseAction();
            isUpgraded = false;
            getDownGrade = true;
            GettingDowngrade();
    }
    
    public void StartGame()
	{
        TweenCameraPos(mainCamera, 2);


    }
    public void StartNPC()
	{
        if (levelIndex != 1)
        {
            
            Enemy.GetComponent<Rigidbody>().isKinematic = false;   
            npcScript.carController.enabled = true;
            
            foreach (VisualEffect item in npcScript.wheelEffects)
            {
                item.Play();
            }
            foreach (WheelCollider item in npcScript.WheelColliders)
            {
                item.enabled = true;
            }
            Enemy.GetComponent<Rigidbody>().velocity = rb.velocity;
        }
        else
        {
            foreach (VisualEffect item in npcScript.wheelEffects)
            {
                item.Play();
            }
            
            Enemy.GetComponent<Rigidbody>().isKinematic = false;
            npcScript.carController.enabled = true;
            Enemy.GetComponent<Rigidbody>().velocity = rb.velocity;
        }
    }

    private void TweenCameraPos(GameObject cameraToTween,float time)
    {
        cameraToTween.transform.DOLocalMove(Vector3.zero, time);
        cameraToTween.transform.DOLocalRotate(Vector3.zero, time).OnComplete(() => {
            if (levelIndex != 1)
            {
                rb.isKinematic = false;
                carController.enabled = true;
				foreach (VisualEffect item in wheelEffects)
				{
                    item.Play();
				}
                foreach (WheelCollider item in WheelColliders)
                {
                    item.enabled = true;
                }
            }
            else
            {
                rb.isKinematic = false;
                carController.enabled = true;
                foreach (VisualEffect item in wheelEffects)
                {
                    item.Play();
                }
            }
        });
    }

    public void effectColourChange(int index)
	{
		foreach (VisualEffect item in wheelEffects)
		{
            item.SetVector4("color01", effectColours[index]);
		}
	}


}
