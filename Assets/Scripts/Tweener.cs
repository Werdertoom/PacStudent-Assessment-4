using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    void Awake()
    {
        activeTweens = new List<Tween>();
    }
   // private Tween activeTween;
   private List<Tween> activeTweens;

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject))
        {
            return false;
        }
        else
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(newTween);
            return true;
        }
    //    if (activeTween == null)
//            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
  //      }
    }
    public bool TweenExists(Transform Target)
    {
        foreach (Tween tween in activeTweens)
        {
            if (tween.Target == Target)
            {
                return true;
            }
        }
        return false;
    }
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
       for (int i = 0; i < activeTweens.Count; i++)
        {
            Tween tween = activeTweens[i];
            float t = (Time.time - tween.StartTime) / tween.Duration;
           
            if (Vector3.Distance(tween.Target.position, tween.EndPos) > 0.1f)
            {
                tween.Target.position = Vector3.Lerp(tween.StartPos, tween.EndPos, t);
            }
            else
            {
                tween.Target.position = tween.EndPos;
                activeTweens.Remove(tween);
                i = -1;

            }
        }
    }
}
