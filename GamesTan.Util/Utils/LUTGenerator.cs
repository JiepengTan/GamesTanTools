// author : https://github.com/JiepengTan

using System.IO;
using NaughtyAttributes;
using UnityEngine;

namespace GamesTan.Util {
    [CreateAssetMenu(menuName = "GamesTan/Config/LUTGenerator")]
    public class LUTGenerator : ScriptableObject {
        public bool IsAutoRefresh = true;
        [Space]
        [OnValueChanged("OnGradientChanged")]
        public int Width = 4;
        [OnValueChanged("OnGradientChanged")]
        public int Height = 1024;

        [OnValueChanged("OnGradientChanged")]
        public Gradient Gradient = new Gradient();

        private  void OnGradientChanged() {
            if(IsAutoRefresh)
                GenTexture();
        }

        [Button()]
        private void GenTexture() {
            var tex = new Texture2D(Width, Height);
            var colors = new Color[Height * Width];
            for (int i = 0; i < Height; i++) {
                var val = Gradient.Evaluate(i * 1.0f / Height);
                for (int j = 0; j < Width; j++) {
                    colors[i * Width + j] = val;
                }
            }

            tex.SetPixels(0, 0, Width, Height, colors);
            tex.Apply();
            var data = tex.EncodeToPNG();
            var path = EditorExtUtil.GetAssetPath(this);
            path = path.Replace(".asset", ".png");
            File.WriteAllBytes(PathUtil.GetFullPath(path), data);
            EditorExtUtil.ImportAsset(path);
        }
    }
}