using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Transform FindAllChildByName(this Transform parent, string name)
    {
        //
        Transform[] t = parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].gameObject.name.Equals(name))
                return t[i];
        }

        return null;
    }

    public static Transform[] FindAllChildsByName(this Transform parent, string name)
    {
        List<Transform> result = new List<Transform>();

        Transform[] t = parent.GetComponentsInChildren<Transform>();
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].gameObject.name.Equals(name))
                result.Add(t[i]);
        }
        return result.ToArray(); //List를 배열로 변환해줌 Tranform[] result
    }

    //이름으로 특정 클릭을 얻어오는 함수
    public static AnimationClip GetClip(this Animator animator, string name)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(name))
                return clip;
        }

        return null;
    }

    //클립이 가진 프레임 개수를 리턴
    public static uint GetFrameCount(this AnimationClip clip)
    {
        return (uint)(clip.frameRate * clip.length);
    }

    //클립의 마지막 프레임을 리턴
    public static uint GetLastFrame(this AnimationClip clip)
    {
        return clip.GetFrameCount() - 1;
    }

    //특정 프레임을 타임으로 변환

    public static float GetTime(this AnimationClip clip, float rate) 
    {
        return clip.length * rate;
    }

    //이벤트 추가
    public static void AddEvent(this AnimationClip clip, string funcName, float rate)
    {
        float execTime = clip.GetTime(rate);

        AnimationEvent e = new AnimationEvent()
        {
            time = execTime,
            functionName = funcName
        };
        clip.AddEvent(e);
    }

    public static void ReadFile(this Animator animator, string file)
    {
        string[] lines = System.IO.File.ReadAllLines("./Assets/Datas/" + file);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');
           
            string clipName = data[0].Trim();
            string funcName = data[1].Trim();
            string tempFrame = data[2].Trim();
            float rate = float.Parse(tempFrame);

            AnimationClip clip = animator.GetClip(clipName);

            clip.AddEvent(funcName, rate);

        }
    }

}
