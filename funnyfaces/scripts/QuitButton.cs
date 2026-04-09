using Godot;
using System;

public partial class QuitButton : Node
{
    public override void _Ready()
    {
        DontDestroyOnLoad(this);
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent)
        {
            if (!keyEvent.Pressed) { return; }
            if (keyEvent.Keycode == Key.Escape)
            {
                GetTree().Quit();
            }
        }
    }
    private SceneTree GetSceneTree()
    {
        return (SceneTree)Engine.GetMainLoop();
    }
    public async void DontDestroyOnLoad(Node node)
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        node.GetParent()?.RemoveChild(node);
        GetSceneTree().Root.AddChild(node);
    }
}
