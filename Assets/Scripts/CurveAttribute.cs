using UnityEngine;

public class CurveAttribute : PropertyAttribute
{
    public enum CurveColor
    {
        Red,
        Cyan,
        Yellow,
        Green,
        Magenta
    }
    public float PosX, PosY;
    public float RangeX, RangeY;
    public int x;
    public bool enabled;
    public UnityEngine.Color color;
    public string label = "";

    public CurveAttribute(float PosX, float PosY, float RangeX, float RangeY,bool enabled)
    {
        this.PosX = PosX;
        this.PosY = PosY;
        this.RangeX = RangeX;
        this.RangeY = RangeY;
        this.enabled = enabled;
        this.color = Color.green;
    }

    public CurveAttribute(string label, float PosX, float PosY, float RangeX, float RangeY, bool enabled)
    {
        this.PosX = PosX;
        this.PosY = PosY;
        this.RangeX = RangeX;
        this.RangeY = RangeY;
        this.enabled = enabled;
        this.color = Color.green;
        this.label = label;
    }

    public CurveAttribute(float PosX, float PosY, float RangeX, float RangeY, bool enabled, CurveColor color)
    {
        this.PosX = PosX;
        this.PosY = PosY;
        this.RangeX = RangeX;
        this.RangeY = RangeY;
        this.enabled = enabled;
        this.color = color.ToUnityColor();
    }

    public CurveAttribute(string label, float PosX, float PosY, float RangeX, float RangeY, bool enabled, CurveColor color)
    {
        this.PosX = PosX;
        this.PosY = PosY;
        this.RangeX = RangeX;
        this.RangeY = RangeY;
        this.enabled = enabled;
        this.color = color.ToUnityColor();
        this.label = label;

    }
}
public static class CurveColorExtensions
{
    public static Color ToUnityColor(this CurveAttribute.CurveColor color)
    {
        switch (color)
        {
            case CurveAttribute.CurveColor.Red:
                return Color.red;

            case CurveAttribute.CurveColor.Cyan:
                return Color.cyan;

            case CurveAttribute.CurveColor.Yellow:
                return Color.yellow;

            case CurveAttribute.CurveColor.Green:
                return Color.green;

            case CurveAttribute.CurveColor.Magenta:
                return Color.magenta;

            default:
                return Color.green;

        }
    }
}