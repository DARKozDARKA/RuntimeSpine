using UnityEngine;

namespace CodeBase.Logic.Cube
{
    public class MaterialChanger : MonoBehaviour
    {
        [SerializeField]
        private Material _mainMaterial;

        [SerializeField]
        private Material _secondaryMaterial;

        [SerializeField]
        private Renderer _renderer;

        [ContextMenu("Set Main")]
        public void SetMainMaterial() =>
            _renderer.material = _mainMaterial;

        [ContextMenu("Set Secondary")]
        public void SetSecondaryMateirial() =>
            _renderer.material = _secondaryMaterial;


    }
}
