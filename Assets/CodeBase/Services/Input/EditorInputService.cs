using CodeBase.StaticData;

namespace CodeBase.Services.Input
{
    public class EditorInputService : IInputService
    {
        public float GetHorizontal() =>
            UnityEngine.Input.GetAxis(InputConstants.Horizontal);
    }
}
