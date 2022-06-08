using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000003 RID: 3
public class AngleRecording : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x00002270 File Offset: 0x00000470
	private void Start()
	{
		
		this.angles = new Dictionary<string, int>();
		this.angles.Add("LeftArmC", 0);
		this.angles.Add("LeftArmT", 0);
		this.angles.Add("RightArmC", 0);
		this.angles.Add("RightArmT", 0);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000022CC File Offset: 0x000004CC
	private void Update()
	{
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000022D0 File Offset: 0x000004D0
	public void StartRecording(Text buttonText)
	{
		if (buttonText.text.Equals("Pausar"))
		{
			base.CancelInvoke("writeOne");
			buttonText.text = "Continuar";
			return;
		}
		if (buttonText.text.Equals("Continuar"))
		{
			base.InvokeRepeating("writeOne", 0f, 0.1f);
			buttonText.text = "Pausar";
			return;
		}
		if (!this.isRecording)
		{
			//this.puzzle.SetActive(true);
			//this.puzzle.GetComponent<Jigsaw>().Start();
			//this.puzzle.GetComponent<Jigsaw>().MovePieces();
			GameObject.Find("HandButton").GetComponent<Button>().interactable = false;
			this.isRecording = true;
			buttonText.text = "Pausar";
			this.csv = new StringBuilder();
			if (this.usingRightHand)
			{
				this.csv.AppendLine("RightShoulderSagi,RightShoulderTransv");
			}
			else
			{
				this.csv.AppendLine("LeftShoulderSagi,LeftShoulderTransv");
			}
			base.InvokeRepeating("writeOne", 0f, 0.1f);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000023E4 File Offset: 0x000005E4
	public void FinishRecording()
	{
		GameObject.Find("HandButton").GetComponent<Button>().interactable = true;
		this.isRecording = false;
		GameObject.Find("RecordText").GetComponent<Text>().text = "Montagem";
		string str = DateTime.Now.ToString("-MM-dd-HH-mm");
		base.CancelInvoke("writeOne");
		string text = Directory.GetParent(Application.persistentDataPath).FullName;
		text = Path.Combine(text, "Reports");
		Directory.CreateDirectory(text);
		text = Path.Combine(text, "angles" + str + ".csv");
		File.WriteAllText(text, this.csv.ToString());
		Debug.Log("File Saved - " + text);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000249F File Offset: 0x0000069F
	public void SetAngles(string pointAngleStr, float angle)
	{
		this.angles[pointAngleStr] = (int)angle;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000024B0 File Offset: 0x000006B0
	private void writeOne()
	{ 
		if (this.usingRightHand)
		{
			this.csv.AppendLine(string.Format("{0},{1}", this.angles["RightArmC"], this.angles["RightArmT"]));
			return;
		}
		this.csv.AppendLine(string.Format("{0},{1}", this.angles["LeftArmC"], this.angles["LeftArmT"]));
	}

	// Token: 0x0400000C RID: 12
	private bool isRecording;

	// Token: 0x0400000D RID: 13
	private Dictionary<string, int> angles;

	// Token: 0x0400000E RID: 14
	private StringBuilder csv;

	// Token: 0x0400000F RID: 15
	public bool usingRightHand = true;

	// Token: 0x04000010 RID: 16
	//public GameObject puzzle;
}
