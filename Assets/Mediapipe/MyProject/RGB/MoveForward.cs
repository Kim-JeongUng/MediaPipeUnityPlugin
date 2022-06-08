using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class MoveForward : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x00006340 File Offset: 0x00004540
	public void startMoving()
	{
		this.startPosition = base.gameObject.transform.position;
		this.endPosition = new Vector3(this.startPosition.x, this.startPosition.y, this.startPosition.z + 10f);
		this.isMoving = true;
		this.lerpTime = 0f;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000063A7 File Offset: 0x000045A7
	private void Start()
	{
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000063A9 File Offset: 0x000045A9
	private void Update()
	{
		this.lerpTime += Time.deltaTime;
		if (this.isMoving)
		{
			base.transform.position = Vector3.Lerp(this.startPosition, this.endPosition, this.lerpTime);
		}
	}

	// Token: 0x04000135 RID: 309
	private Vector3 startPosition;

	// Token: 0x04000136 RID: 310
	private Vector3 endPosition;

	// Token: 0x04000137 RID: 311
	private bool isMoving;

	// Token: 0x04000138 RID: 312
	private float stopTime = 3f;

	// Token: 0x04000139 RID: 313
	private float lerpTime;
}
