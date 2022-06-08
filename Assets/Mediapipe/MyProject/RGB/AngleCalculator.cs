using System;
using TMPro;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AngleCalculator : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMeshPro>();
		this.PointAngle = GameObject.Find(this.avatarName + this.PointAngleStr).transform;
		Debug.Log(this.PointAngle ? ("Find " + this.avatarName + this.PointAngleStr) : ("Find " + this.avatarName + this.PointAngleStr + " Error"));
		this.angleRecording = GameObject.Find("AngleRecorder").GetComponent<AngleRecording>();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020E4 File Offset: 0x000002E4
	private void Update()
	{
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
	public void UpdateCalculation()
	{
		float num = Mathf.Round(this.CalculateAngle(this.PointAngle) * this.precisionFactor) / this.precisionFactor;
		this.textMesh.text = string.Format("{0}", num);
		float num2 = num / (float)this.MaxBarAngle;
		float y = 5f + 10f * num2 / 2f;
		if (this.bar)
		{
			this.bar.transform.localScale = new Vector3(0.1f, 1f, num2);
			this.bar.transform.localPosition = new Vector3(this.bar.transform.localPosition.x, y, this.bar.transform.localPosition.z);
		}
		this.angleRecording.SetAngles((this.fixedX || this.fixedXRight) ? (this.PointAngleStr + "C") : (this.PointAngleStr + "T"), num);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000021FC File Offset: 0x000003FC
	private float CalculateAngle(Transform joint)
	{
		float result;
		if (this.fixedX)
		{
			result = Vector3.Angle(Vector3.up, joint.right);
		}
		else if (this.fixedXRight)
		{
			result = Vector3.Angle(Vector3.down, joint.right);
		}
		else
		{
			result = Vector3.Angle(Vector3.forward, joint.forward);
		}
		return result;
	}

	// Token: 0x04000001 RID: 1
	private Transform PointAngle;

	// Token: 0x04000002 RID: 2
	public string PointAngleStr;

	// Token: 0x04000003 RID: 3
	private TextMeshPro textMesh;

	// Token: 0x04000004 RID: 4
	public Transform bar;

	// Token: 0x04000005 RID: 5
	[Range(0f, 250f)]
	public int MaxBarAngle;

	// Token: 0x04000006 RID: 6
	public bool fixedX;

	// Token: 0x04000007 RID: 7
	public bool fixedXRight;

	// Token: 0x04000008 RID: 8
	public bool fixedZ;

	// Token: 0x04000009 RID: 9
	public string avatarName = "mixamorig:";

	// Token: 0x0400000A RID: 10
	public float precisionFactor = 1f;

	// Token: 0x0400000B RID: 11
	private AngleRecording angleRecording;
}
