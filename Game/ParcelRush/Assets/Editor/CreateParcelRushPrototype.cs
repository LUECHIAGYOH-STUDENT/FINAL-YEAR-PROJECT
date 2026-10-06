using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CreateParcelRushPrototype
{
    [MenuItem("Parcel Rush/Create Main Character Prototype")]
    public static void CreatePrototype()
    {
        EnsureFolder("Assets/Scenes");
        EnsureFolder("Assets/Materials");

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        Material groundMaterial = CreateMaterial("Assets/Materials/Ground.mat", new Color(0.20f, 0.58f, 0.34f));
        Material courierMaterial = CreateMaterial("Assets/Materials/CourierOrange.mat", new Color(1.00f, 0.38f, 0.08f));
        Material parcelMaterial = CreateMaterial("Assets/Materials/ParcelBrown.mat", new Color(0.55f, 0.25f, 0.08f));
        Material buildingMaterial = CreateMaterial("Assets/Materials/BuildingBlue.mat", new Color(0.16f, 0.46f, 0.82f));

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0f, -0.5f, 0f);
        ground.transform.localScale = new Vector3(30f, 1f, 30f);
        ground.GetComponent<Renderer>().sharedMaterial = groundMaterial;

        CreateBuilding(new Vector3(8f, 1.5f, 7f), new Vector3(5f, 3f, 5f), buildingMaterial, "Building_A");
        CreateBuilding(new Vector3(-9f, 2f, 5f), new Vector3(6f, 4f, 4f), buildingMaterial, "Building_B");
        CreateBuilding(new Vector3(6f, 1f, -8f), new Vector3(4f, 2f, 6f), buildingMaterial, "Building_C");

        GameObject courier = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        courier.name = "Courier";
        courier.transform.position = new Vector3(0f, 1.1f, 0f);
        Object.DestroyImmediate(courier.GetComponent<CapsuleCollider>());
        courier.GetComponent<Renderer>().sharedMaterial = courierMaterial;

        CharacterController controller = courier.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.5f;
        controller.center = Vector3.zero;
        controller.stepOffset = 0.3f;

        GameObject parcel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        parcel.name = "ParcelBackpack";
        parcel.transform.SetParent(courier.transform);
        parcel.transform.localPosition = new Vector3(0f, 0.15f, -0.55f);
        parcel.transform.localRotation = Quaternion.identity;
        parcel.transform.localScale = new Vector3(0.8f, 0.75f, 0.45f);
        Object.DestroyImmediate(parcel.GetComponent<BoxCollider>());
        parcel.GetComponent<Renderer>().sharedMaterial = parcelMaterial;

        GameObject cameraObject = new GameObject("Main Camera");
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.tag = "MainCamera";
        cameraObject.AddComponent<AudioListener>();
        cameraObject.transform.position = new Vector3(0f, 4f, -6f);

        ThirdPersonCamera cameraController = cameraObject.AddComponent<ThirdPersonCamera>();
        cameraController.target = courier.transform;

        ThirdPersonCourierController courierController = courier.AddComponent<ThirdPersonCourierController>();
        courierController.cameraTransform = cameraObject.transform;

        GameObject lightObject = new GameObject("Directional Light");
        Light light = lightObject.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightObject.transform.rotation = Quaternion.Euler(45f, -30f, 0f);

        RenderSettings.ambientLight = new Color(0.55f, 0.62f, 0.70f);

        const string scenePath = "Assets/Scenes/MainCity.unity";
        EditorSceneManager.SaveScene(scene, scenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scenePath, true) };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Parcel Rush prototype created at " + scenePath);
    }

    private static void CreateBuilding(Vector3 position, Vector3 scale, Material material, string name)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = name;
        building.transform.position = position;
        building.transform.localScale = scale;
        building.GetComponent<Renderer>().sharedMaterial = material;
    }

    private static Material CreateMaterial(string path, Color color)
    {
        Material existing = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (existing != null)
        {
            return existing;
        }

        Shader shader = Shader.Find("Standard");
        Material material = new Material(shader) { color = color };
        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static void EnsureFolder(string path)
    {
        string[] parts = path.Split('/');
        string current = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = current + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, parts[i]);
            }
            current = next;
        }
    }
}
