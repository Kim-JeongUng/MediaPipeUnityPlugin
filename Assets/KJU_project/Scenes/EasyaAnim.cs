using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyaAnim : MonoBehaviour
{
  public Transform Point;
  public Transform[] Anims;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    if (Point.childCount > 0)
    {
      Anims[0].localPosition = Point.GetChild(15).localPosition;

      Anims[1].localPosition = Point.GetChild(16).localPosition;
      Anims[2].localPosition = Point.GetChild(31).localPosition;
      Anims[3].localPosition = Point.GetChild(32).localPosition;
      
    }
  }
}
