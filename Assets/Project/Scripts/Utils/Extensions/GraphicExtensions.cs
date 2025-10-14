namespace Project.Scripts.Utils.Extensions
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this UnityEngine.UI.Graphic graphic, float alpha)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}