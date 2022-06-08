using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class LegTrigger : MonoBehaviour
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002AC4 File Offset: 0x00000CC4
	private void Start()
	{
		this.poseManager = GameObject.Find("PoseManager").GetComponent<PoseManager>();
		//this.controlObstacles = base.gameObject.GetComponent<ControlObstacles>();
		base.StartCoroutine(this.SetTrigger());
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002AFC File Offset: 0x00000CFC
	private void Update()
	{
		if (this.poseManager.hasPose())
		{
			global::Pose pose = this.poseManager.GetPose();
			float num;
			if (this.legSide == LegTrigger.Sides.L)
			{
				num = Vector3.Angle(pose.HipLeft - pose.KneeLeft, pose.AnkleLeft - pose.KneeLeft);
			}
			else
			{
				num = Vector3.Angle(pose.HipRight - pose.KneeRight, pose.AnkleRight - pose.KneeRight);
			}
			if (this.lastAngle > 110f && num < 110f && this.triggerReady)
			{
				this.triggerReady = false;
				//this.controlObstacles.ReceiveTrigger();
			}
			this.lastAngle = num;
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002BB5 File Offset: 0x00000DB5
	private IEnumerator SetTrigger()
	{
		yield return new WaitForSeconds(2f);
		this.triggerReady = true;
		yield break;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002BC4 File Offset: 0x00000DC4
	public void ReceiveWaitTrigger()
	{
		this.triggerReady = false;
		base.StartCoroutine(this.SetTrigger());
	}

	// Token: 0x0400002D RID: 45
	public LegTrigger.Sides legSide;

	// Token: 0x0400002E RID: 46
	private PoseManager poseManager;

	// Token: 0x0400002F RID: 47
	//private ControlObstacles controlObstacles;

	// Token: 0x04000030 RID: 48
	private bool triggerReady;

	// Token: 0x04000031 RID: 49
	private float lastAngle = 100f;

	// Token: 0x02000048 RID: 72
	public enum Sides
	{
		// Token: 0x04000209 RID: 521
		L,
		// Token: 0x0400020A RID: 522
		R
	}
}
