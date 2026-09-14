using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JfranMora.Inspector;



public class ChangeTexture : MonoBehaviour
{

    /// <summary>
    /// 计时器
    /// </summary>
    //private float timer;
    /// <summary>
    /// 渲染网格
    /// </summary>
    public MeshRenderer meshRenderer;
    /// <summary>
    /// 贴图数组
    /// </summary>
    private Texture[] texture;
    /// <summary>
    /// Resource文件夹下的子文件夹名
    /// </summary>
    private string materialTexture = "MaterialTexture";
    /// <summary>
    /// 贴图数量
    /// </summary>
    private int textureCount = 5;
    /// <summary>
    /// 一开始循环的索引
    /// </summary>
    private int index = 0;
    /// <summary>
    /// 切换时间
    /// </summary>
    //private float changeTime = 0.5f;
    public List<Texture> textures = new List<Texture>(); //贴图 
    public Material M1, M2;



    private void Start()
    {
        //timer = 0;
        index = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        //定义获取贴图的数量
        texture = new Texture[textureCount];
        //动态加载贴图
        for (int i = 0; i < texture.Length; i++)
        {
            texture[i] = Resources.Load(materialTexture + "/page_" + (i + 1)) as Texture;
        }
    }


    private void Update()
    {

    }

    
    public void PagePlus()
    {
        meshRenderer.material = M2;
        //meshRenderer.material.SetTexture("_Emission", texture[index]);
        //index = (index + 1) % texture.Length;
    }

    public void PageMinus()
    {
        meshRenderer.material = M1;
        //meshRenderer.material.SetTexture("_Emission", texture[index]);
        //index = (index - 1) % texture.Length;
    }

    

}
