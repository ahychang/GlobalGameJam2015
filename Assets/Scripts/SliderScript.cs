using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	Slider charge;
	public bool used;

	void Start()
	{
		used = false;
		charge = GetComponent<Slider> ();
		charge.value = charge.maxValue;
	}

	void Update()
	{
		if (charge.value < charge.maxValue && used)
			charge.value += (charge.maxValue - charge.minValue)/100;
		else if (used)
			used = false;
	}

	public bool Full()
	{
		if (charge.value == charge.maxValue) return true;
		else return false;
	}

	public void setZero()
	{
		charge.value = charge.minValue;
	}

}
