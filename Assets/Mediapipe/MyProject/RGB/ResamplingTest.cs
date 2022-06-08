using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class ResamplingTest : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x00007401 File Offset: 0x00005601
	private void Start()
	{
		ResamplingTest.Resample(new float[]
		{
			1f,
			2f,
			10f
		}, 10);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000741C File Offset: 0x0000561C
	public static float[] Resample(float[] source, int n)
	{
		float[] array = new float[n];
		array[0] = source[0];
		int destFrom = 0;
		for (int i = 1; i < source.Length; i++)
		{
			int num = i * (array.Length - 1) / (source.Length - 1);
			ResamplingTest.Interpolate(array, destFrom, num, source[i - 1], source[i]);
			destFrom = num;
		}
		return array;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00007468 File Offset: 0x00005668
	private static void Interpolate(float[] destination, int destFrom, int destTo, float valueFrom, float valueTo)
	{
		int num = destTo - destFrom;
		for (int i = 0; i <= num; i++)
		{
			destination[destFrom + i] = Mathf.LerpAngle(valueFrom, valueTo, (float)i / (float)num);
		}
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00007498 File Offset: 0x00005698
	public static Quaternion[] Resample(Quaternion[] source, int n)
	{
		Quaternion[] array = new Quaternion[n];
		array[0] = source[0];
		int destFrom = 0;
		for (int i = 1; i < source.Length; i++)
		{
			int num = i * (array.Length - 1) / (source.Length - 1);
			ResamplingTest.Interpolate(array, destFrom, num, source[i - 1], source[i]);
			destFrom = num;
		}
		return array;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000074F4 File Offset: 0x000056F4
	private static void Interpolate(Quaternion[] destination, int destFrom, int destTo, Quaternion valueFrom, Quaternion valueTo)
	{
		int num = destTo - destFrom;
		for (int i = 0; i <= num; i++)
		{
			destination[destFrom + i] = Quaternion.Lerp(valueFrom, valueTo, (float)i / (float)num);
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00007528 File Offset: 0x00005728
	public static List<Vector3> ResampleVec3_ver2(List<Vector3> vectors, int n)
	{
		List<Quaternion> list = new List<Quaternion>();
		foreach (Vector3 euler in vectors)
		{
			list.Add(Quaternion.Euler(euler));
		}
		Quaternion[] array = ResamplingTest.Resample(list.ToArray(), n);
		List<Vector3> list2 = new List<Vector3>();
		foreach (Quaternion quaternion in array)
		{
			list2.Add(quaternion.eulerAngles);
		}
		return list2;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000075C4 File Offset: 0x000057C4
	public static List<Vector3> ResampleVec3(List<Vector3> vectors, int n)
	{
		float[] array = new float[vectors.Count];
		float[] array2 = new float[vectors.Count];
		float[] array3 = new float[vectors.Count];
		int i = 0;
		foreach (Vector3 vector in vectors)
		{
			array[i] = vector.x;
			array2[i] = vector.y;
			array3[i] = vector.z;
			i++;
		}
		array = ResamplingTest.Resample(array, n);
		array2 = ResamplingTest.Resample(array2, n);
		array3 = ResamplingTest.Resample(array3, n);
		List<Vector3> list = new List<Vector3>();
		for (i = 0; i < n; i++)
		{
			list.Add(new Vector3(array[i], array2[i], array3[i]));
		}
		return list;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00007698 File Offset: 0x00005898
	public static List<Register> ResampleRegisterList(List<Register> registerList, int n)
	{
		int[] articulationList = registerList[0].GetArticulationList();
		List<Register> list = new List<Register>(n);
		List<Vector3> list2 = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			list.Add(new Register(articulationList));
		}
		for (int j = 0; j < articulationList.Length; j++)
		{
			list2 = new List<Vector3>();
			foreach (Register register in registerList)
			{
				list2.Add(register.GetArticulationCoordinates(articulationList[j]));
			}
			list2 = ResamplingTest.ResampleVec3_ver2(list2, n);
			for (int k = 0; k < n; k++)
			{
				list[k].SetArticulationCoordinates(articulationList[j], list2[k]);
			}
		}
		return list;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00007774 File Offset: 0x00005974
	private static float AngleFix(float number)
	{
		if (number == 0f)
		{
			return number;
		}
		number = Mathf.Abs(number) % 360f * (number / Mathf.Abs(number));
		if (number < 0f)
		{
			number = 360f + number;
		}
		return number;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000077A9 File Offset: 0x000059A9
	private void Update()
	{
	}
}
