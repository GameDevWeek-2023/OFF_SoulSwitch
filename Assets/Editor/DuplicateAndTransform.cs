using UnityEngine;
using UnityEditor;

public class DuplicateAndTransform : EditorWindow
{
    int numberOfDuplicates = 0;
    Vector3 additionalTransform;
    bool rotateDuplicates = true;
    bool groupDuplicates = true;
    float rotationVariation = 2.0f;

    [MenuItem("Window/Duplicate and Transform")]
    static void Init()
    {
        DuplicateAndTransform window = (DuplicateAndTransform)EditorWindow.GetWindow(typeof(DuplicateAndTransform));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Duplicate and Transform", EditorStyles.boldLabel);

        numberOfDuplicates = EditorGUILayout.IntField("Number of Duplicates:", numberOfDuplicates);
        additionalTransform = EditorGUILayout.Vector3Field("Additional Transform:", additionalTransform);
        rotateDuplicates = EditorGUILayout.Toggle("Rotate Duplicates", rotateDuplicates);
        groupDuplicates = EditorGUILayout.Toggle("Group Duplicates", groupDuplicates);
        rotationVariation = EditorGUILayout.FloatField("Rotation Variation:", rotationVariation);

        if (GUILayout.Button("Duplicate"))
        {
            DuplicateObjects(numberOfDuplicates, additionalTransform, rotateDuplicates, groupDuplicates, rotationVariation);
        }
    }

    void DuplicateObjects(int numberOfDuplicates, Vector3 additionalTransform, bool rotateDuplicates, bool groupDuplicates, float rotationVariation)
    {
        // Get the selected game object
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            // Create a new parent game object for the duplicates
            GameObject parentObject = null;

            if (groupDuplicates)
            {
                parentObject = new GameObject(selectedObject.name + " Duplicates");
            }

            for (int i = 0; i < numberOfDuplicates; i++)
            {
                // Duplicate the selected game object
                GameObject duplicateObject = Instantiate(selectedObject);

                // Rename the duplicated game object
                duplicateObject.name = selectedObject.name + " " + i;

                // Apply additional transform
                duplicateObject.transform.position += additionalTransform * (i + 1);

                // Add small variation to the rotation
                if (rotateDuplicates)
                {
                    float rotationY = 90f * (i + 1) + Random.Range(-rotationVariation, rotationVariation);
                    duplicateObject.transform.Rotate(new Vector3(0f, rotationY, 0f));
                }

                // Add the duplicated game object to the parent game object
                if (groupDuplicates)
                {
                    duplicateObject.transform.parent = parentObject.transform;
                }

                // Place the duplicated game object next to the original game object
                //duplicateObject.transform.position = selectedObject.transform.position + new Vector3(i + 1, 0, 0);
            }

            // Group the duplicates under the parent game object
            if (groupDuplicates)
            {
                Selection.activeGameObject = parentObject;
                EditorGUIUtility.PingObject(parentObject);
            }
        }
    }
}
