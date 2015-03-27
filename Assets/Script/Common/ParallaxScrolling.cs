using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour 
{
	//每个层的Transform
	public Transform[] Layers;
	//对应每个层的最大位移
	public float[] Offsets;
	int count;
	//定义归一化位置
	float location;

	// Use this for initialization
	void Start () 
	{
		count = Layers.Length;
	}

	//设置归一化位置
	public void SetPositon(float position)
	{
		//当location为0时，显示各个层最左边的内容，为1时显示各个层最右边的内容
		location = Mathf.Clamp01 (position);

		for (int i =0; i < count; i++) 
		{
			Layers[i].localPosition = new Vector3(Offsets[i]*location,0,0);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
