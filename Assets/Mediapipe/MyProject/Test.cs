using System;
using System.Collections;
using System.Collections.Generic;

public class Test
{
  // Start is called before the first frame update
  static void Main(string[] args)
  {
    int[] a = { 93, 30, 55 };
    int[] b = { 1, 30, 5 };
    Console.WriteLine("ASD");
  }
  public int[] solution(int[] progresses, int[] speeds)
  {
    List<int> release = new List<int>();
    Queue<int> days = new Queue<int>();
    int day = 0;
    int nextDay = 0;
    for (int i = 0; i < progresses.Length; i++)
    {
      day = (100 - progresses[i]) / speeds[i];
      if ((100 - progresses[i]) % speeds[i] != 0) day++;
      days.Enqueue(day);
    }
    day = days.Dequeue();
    int dayCount = 1;
    while (days.Count != 0)
    {
      nextDay = days.Dequeue();
      if (day >= nextDay) dayCount++;
      else
      {
        day = nextDay;
        release.Add(dayCount);
        dayCount = 1;
      }
    }
    release.Add(dayCount);
    return release.ToArray();
  }
}
