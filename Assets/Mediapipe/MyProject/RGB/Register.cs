using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
[Serializable]
public class Register
{
	// Token: 0x06000188 RID: 392 RVA: 0x00007B62 File Offset: 0x00005D62
	public int GetCount()
	{
		return this.articulations.Count;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00007B6F File Offset: 0x00005D6F
	public Register()
	{
		this.articulations = new Dictionary<int, Vector3>();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00007B82 File Offset: 0x00005D82
	public Register(int[] articulationList)
	{
		this.articulations = new Dictionary<int, Vector3>();
		this.SetArticulations(articulationList);
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00007B9C File Offset: 0x00005D9C
	public void SetArticulations(int[] articulationList)
	{
		int i = 0;
		while (i < articulationList.Length)
		{
			if (1 <= articulationList[i] && articulationList[i] <= 20)
			{
				try
				{
					this.articulations.Add(articulationList[i], default(Vector3));
					goto IL_4C;
				}
				catch (ArgumentException)
				{
					throw new ArgumentException("Duplicate articulation in list");
				}
				goto IL_36;
				IL_4C:
				i++;
				continue;
			}
			IL_36:
			this.articulations.Clear();
			throw new IndexOutOfRangeException("Articulations can't be smaller than 1 or greater than 20");
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00007C10 File Offset: 0x00005E10
	public void SetArticulationCoordinates(int articulation, Vector3 rotations)
	{
		if (this.articulations.ContainsKey(articulation))
		{
			this.articulations[articulation] = rotations;
			return;
		}
		throw new KeyNotFoundException();
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00007C34 File Offset: 0x00005E34
	public Vector3 GetArticulationCoordinates(int articulation)
	{
		Vector3 result;
		try
		{
			result = this.articulations[articulation];
		}
		catch (KeyNotFoundException)
		{
			throw new KeyNotFoundException();
		}
		return result;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00007C68 File Offset: 0x00005E68
	public override string ToString()
	{
		string[] array = new string[this.articulations.Count];
		int num = 0;
		foreach (KeyValuePair<int, Vector3> keyValuePair in this.articulations)
		{
			array[num] = string.Concat(new string[]
			{
				Math.Round((double)keyValuePair.Value.x, 2).ToString().Replace(',', '.'),
				",",
				Math.Round((double)keyValuePair.Value.y, 2).ToString().Replace(',', '.'),
				",",
				Math.Round((double)keyValuePair.Value.z, 2).ToString().Replace(',', '.')
			});
			num++;
		}
		return string.Join(";", array);
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00007D74 File Offset: 0x00005F74
	public string GetArticulationIndexPattern()
	{
		string[] array = new string[this.articulations.Count];
		int num = 0;
		foreach (KeyValuePair<int, Vector3> keyValuePair in this.articulations)
		{
			array[num] = "a" + keyValuePair.Key.ToString();
			num++;
		}
		return string.Join(";", array);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00007E00 File Offset: 0x00006000
	public int[] GetArticulationList()
	{
		int[] array = new int[this.articulations.Count];
		int num = 0;
		foreach (KeyValuePair<int, Vector3> keyValuePair in this.articulations)
		{
			array[num] = keyValuePair.Key;
			num++;
		}
		return array;
	}

	// Token: 0x040001B2 RID: 434
	private Dictionary<int, Vector3> articulations;
}
