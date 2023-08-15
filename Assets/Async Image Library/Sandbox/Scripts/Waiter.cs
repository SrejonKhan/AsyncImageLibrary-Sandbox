using AsyncImageLibrary;
using System;
using System.Collections;
using UnityEngine;

public static class Waiter
{
    public static void Wait(int frameToWait, Action action)
    {
        StaticCoroutine.StartCoroutine(ExecuteWait(frameToWait, action));
    }

    public static void WaitEndOfFrame(int frameToWait, Action action)
    {
        StaticCoroutine.StartCoroutine(ExecuteWaitEndOfFrame(frameToWait, action));
    }

    public static void Wait(float secondToWait, Action action)
    {
        StaticCoroutine.StartCoroutine(ExecuteWaitSecond(secondToWait, action));
    }

    public static void WaitUntil(Func<bool> predicate, Action action)
    {
        StaticCoroutine.StartCoroutine(ExecuteWaitUntil(predicate, action));
    }

    private static IEnumerator ExecuteWait(int frameToWait, Action action)
    {
        for (int i = 0; i < frameToWait; i++)
            yield return null;

        action?.Invoke();
    }

    private static IEnumerator ExecuteWaitEndOfFrame(int frameToWait, Action action)
    {
        for (int i = 0; i < frameToWait; i++)
            yield return new WaitForEndOfFrame();

        action?.Invoke();
    }

    private static IEnumerator ExecuteWaitSecond(float secondToWait, Action action)
    {
        yield return new WaitForSecondsRealtime(secondToWait);

        action?.Invoke();
    }

    private static IEnumerator ExecuteWaitUntil(Func<bool> predicate, Action action)
    {
        yield return new WaitUntil(predicate);

        action?.Invoke();
    }
}