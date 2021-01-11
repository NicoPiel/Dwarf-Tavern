using UnityEngine;


public class GameGUI : MonoBehaviour
{
    public Rect windowRect = new Rect(20, 20, 450, 400);
    public bool doWindow0 = false;

    public void ToggleWindows0()
    {
        doWindow0 = !doWindow0;
    }

    void OnGUI()
    {
        // Register the window. Notice the 3rd parameter
        if (doWindow0)
        {
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
        }
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 400, 500), "Hello World"))
        {
            print("Got a click");
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
}