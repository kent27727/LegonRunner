using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NPCAI : MonoBehaviour
{
	//optimize et
    Rigidbody rb;
	public string currentSurface;
	public float speed;
	public float upgradeSpeed;
	public float downgradeSpeed;
	public float suspensionHeightMax;
	public float suspensionHeightMin;
	public float upgradeDurationTime;
	public float downgradeDurationTime;
	public bool isUpgraded;
	public bool getDownGrade = false;
	private float staticSpeed;
	public int min, max;
	public bool isWon,isLose;
	public EffectManager FXMan;
	public GameObject[] TireChains;
	public GameObject[] EngineParts;
	public WheelCollider[] WheelColliders;
	public GameObject CarParts;
	public float yPosofCarParts;
	public CarController carController;
	public int levelIndex;
	public bool currentUpgrading;
	public int notDowngradeIndex;
	public int notDowngradeIndexTarget;
	public VisualEffect[] wheelEffects;
	public Color[] effectColours;

	private void Start()
	{
		carController = GetComponent<CarController>();
		yPosofCarParts = CarParts.transform.position.y;
		FXMan = GetComponent<EffectManager>();
		rb = GetComponent<Rigidbody>();
		staticSpeed = speed;
	}
	
	private void Update()
	{
		if (levelIndex == 1)
		{
			if (isLose)
			{
				rb.velocity = Vector3.zero;
				carController.isBreaking = true;
			}
			else if (!isWon)
			{
				rb.velocity = new Vector3(speed, -5, 0);
			}
			else if (isWon)
			{
				rb.velocity = Vector3.zero;
				carController.isBreaking = true;

			}
		}
		else
		{
			if (isLose)
			{
				rb.velocity = Vector3.zero;
				carController.isBreaking = true;
			}
			else if (isWon)
			{
				rb.velocity = Vector3.zero;
				carController.isBreaking = true;

			}
		}
		if (rb.velocity.magnitude > speed && !currentUpgrading)
		{
			rb.velocity = rb.velocity.normalized * speed;
		}

	}

	private float Randomizer(int x, int y)
	{
		return Random.Range(x, y);
	}
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
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
				break;
			case "Surface5":
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
				break;
			case "Surface6":
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
				break;
			case "Surface7":
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
				break;
			case "Surface8":
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMax, upgradeDurationTime);
				break;
			case "Surface9":
				FXMan.TweenDoTo(WheelColliders, suspensionHeightMin, upgradeDurationTime);
				break;
			default:
				break;
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		switch (collision.gameObject.name)
		{
			case "Surface1":
				currentSurface = collision.gameObject.name;
				break;
			case "Surface2":
				currentSurface = collision.gameObject.name;
				break;

			case "Surface3":
				currentSurface = collision.gameObject.name;
				break;

			case "Surface4":
				currentSurface = collision.gameObject.name;
				break;

			case "Surface5":
				currentSurface = collision.gameObject.name;
				break;
			case "Surface6":
				currentSurface = collision.gameObject.name;
				break;
			case "Surface7":
				currentSurface = collision.gameObject.name;
				break;
			case "Surface8":
				currentSurface = collision.gameObject.name;
				break;
			case "Surface9":
				currentSurface = collision.gameObject.name;
				break;
			case "FinishLine":
				currentSurface = collision.gameObject.name;
				isWon = true;
				break;
			case "Normal":
				currentSurface = null;
				break;



			default:
				currentSurface = null;
				break;
		}
		switch (collision.tag)
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
	private void OnTriggerExit(Collider collision)
	{
		if (collision.name != "Engebe")
		{
			ResetObjectETC();
			isUpgraded = false;
			if (currentSurface != null)
			{
				float timing = Randomizer(min, max) / 10;

				if (isGetUpgrade())
				{
					Invoke("GettingUpgrade", timing);
				}
				else
				{
					Invoke("downgradeTrigger", timing);
				}
				if (notDowngradeIndex != notDowngradeIndexTarget)
				{
					notDowngradeIndex += 1;
				}
			}
			
		}
		
	}
	public void downgradeTrigger()
	{
			getDownGrade = true;
			GettingDowngrade();
	}

	public void GettingDowngrade()
	{


		if (getDownGrade)
		{
			DOTween.To(() => speed, x => speed = x, downgradeSpeed, downgradeDurationTime).OnComplete(() => { });
			getDownGrade = false;

		}

	}

	private bool isGetUpgrade()
	{
		int i = Random.Range(0, 3);
		if (i == 0 ||i == 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void GettingUpgrade()
	{

		float lastSpeed = speed + upgradeSpeed;


		if (!isUpgraded)
		{
			Sequence mySequence = DOTween.Sequence();
			CurrentEvent();
			currentUpgrading = true;
			mySequence.Append(DOTween.To(() => speed, x => speed = x, lastSpeed, upgradeDurationTime));
			mySequence.Append(DOTween.To(() => speed, x => speed = x, staticSpeed, upgradeDurationTime).OnComplete(() =>
			{
				currentUpgrading = false;
				isUpgraded = true;
			}));
		}

	}
	public void ResetObjectETC()
	{
		FXMan.TweenScale(FXMan.Wheels, 1f);
		FXMan.TweenScale(TireChains, 0f);
		FXMan.TweenScale(EngineParts, 1f);
	}
	public void effectColourChange(int index)
	{
		foreach (VisualEffect item in wheelEffects)
		{
			item.SetVector4("color01", effectColours[index]);
		}
	}

}
