using UnityEngine;

namespace Assets.Code.Common.Utils.Extensions
{
    public static class CurvesExtension
    {
        public static float GetAverageValue(this AnimationCurve animationCurve, int segmentsCount=20)
        {
            float sum = 0;

            for (int i = 0; i < segmentsCount; i++)
            {
                float step = i / (segmentsCount - 1f);
                sum += animationCurve.Evaluate(step);
            }
            return sum/segmentsCount;
        }
        
        public static AnimationCurve GetNormalisedCurve(this AnimationCurve animationCurve)
        {
            float average = animationCurve.GetAverageValue();
            AnimationCurve newCurve = new AnimationCurve();

            foreach (Keyframe keyframe in animationCurve.keys)
            {
                Keyframe newKeyframe = new Keyframe(keyframe.time, keyframe.value /average, keyframe.inTangent, keyframe.outTangent);
                newCurve.AddKey(newKeyframe);
            }

            return newCurve;
        }
    }

    public static class CreateCurve
    {
        public static AnimationCurve Sin=>            new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.25f, 0.5f, 0f, 0f),
            new Keyframe(0.5f, 0f, 0f, 0f), new Keyframe(0.75f, -0.5f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f));

        public static AnimationCurve InOut=>AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    }
}