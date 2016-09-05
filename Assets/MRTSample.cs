using UnityEngine;
using System.Linq;

namespace XJ.Unity3D.ImageEffects
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    [AddComponentMenu("XJImageEffects/SobelFilter")]
    public class MRTSample : ImageEffect
    {
        #region Field

        private RenderTexture[] renderTextures;
        private RenderTexture renderTextureDepth;
        private new Camera camera;

        #endregion Field

        protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            base.OnRenderImage(source, destination);
        }

        protected override void Start()
        {
            // (1) RenderTexture を初期化します。
            int renderTargetSize = 3;

            this.renderTextures = new RenderTexture[renderTargetSize];

            for (int i = 0; i < renderTargetSize; i++)
            {
                this.renderTextures[i] = new RenderTexture
                    (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
            }

            this.renderTextureDepth = new RenderTexture
                (Screen.width, Screen.height, 24, RenderTextureFormat.Depth);

            // (2) カメラに複数の RenderTexture を設定します。

            this.camera = GetComponent<Camera>();
            this.camera.SetTargetBuffers
                (this.renderTextures.Select(tex => tex.colorBuffer).ToArray(),
                 this.renderTextureDepth.depthBuffer);

            // Graphics.SetRenderTarget なるものもある。
            // 
            //Graphics.SetRenderTarget
            //    (this.renderTextures.Select(tex => tex.colorBuffer).ToArray(),
            //     this.renderTextureDepth.depthBuffer);
        }

        void Update()
        {
            //Graphics.SetRenderTarget(null);
            this.camera.RenderWithShader(base.shader, null);
        }

        //void OnGUI()
        //{
        //    // テクスチャを描画
        //    GUI.DrawTexture(new Rect(0, 0, 100, 100), this.renderTextures[2]);
        //}
    }
}