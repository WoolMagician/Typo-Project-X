using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CurveAttribute))]
public class CurveDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        CurveAttribute curve = attribute as CurveAttribute;

        if (property.propertyType == SerializedPropertyType.AnimationCurve)
        {
            EditorGUI.LabelField(position, curve.label, EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(!curve.enabled);

            position.y += position.height;
            position.height = 100;

            property.animationCurveValue = EditorGUI.CurveField(position, "", property.animationCurveValue, curve.color, new Rect(curve.PosX, curve.PosY, curve.RangeX, curve.RangeY));
            EditorGUI.EndDisabledGroup();
            //EditorGUILayout.Space();
        }
    }    
}