using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Managers
{
    public class SkyBoxManager : MonoBehaviour
    {
        [SerializeField] private Material _skyBox;
        [SerializeField] private Volume _volume;
        [SerializeField] private ColorAdjustments _colorAdjustments;

        private void Start()
        {
            
            _volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
            
            DOTween.To(() => _colorAdjustments.colorFilter.value, x => _colorAdjustments.colorFilter.value = x, new Color32(255,110,110,0), 3f)
                .OnComplete(() =>
                {
                    ChangeSkybox();
                });
        }

        private void ChangeSkybox()
        {
            RenderSettings.skybox = _skyBox;
            DOTween.To(() => _colorAdjustments.colorFilter.value, x => _colorAdjustments.colorFilter.value = x,
                Color.white, 3f);
        }
    }
}
