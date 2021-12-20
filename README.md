# 个人的小型 Unity 代码工具箱

[代码下载地址](https://github.com/JiepengTan/GamesTanTools)

### 1. 内容说明  
#### 1. EditorExt
1. **GamesTan.EditorExt** 这个dll 包含了一些对Unity Editor 接口的封装，  
让在编写代码的时候无需考虑是否会因为 包含了Editor 相关的接口导致编译报错  
让代码编写更加容易

使用 EditorExt 之前的代码
```cs
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class LUTGenerator : ScriptableObject {
    private void GenTexture() {
#if UNITY_EDITOR 
        var path = AssetDatabase.GetAssetPath(this);
#endif
     // ....其他代码
#if UNITY_EDITOR 
        AssetDatabase.ImportAsset(path);
#endif
    }
}
```


使用 EditorExt 之后的代码，不再需要条件编译，编写起来更加流畅
```cs
using GamesTan;
public class LUTGenerator : ScriptableObject {
    private void GenTexture() {
        var path = EditorExtUtil.GetAssetPath(this);
     	// ....其他代码
        EditorExtUtil.ImportAsset(path);
    }
}
```

#### 2. Util  

##### 1. PathUtil  
  封装一些路径相关的API ，比如绝对路径转Unity 相对路径， 目录遍历等常用功能
  

##### 2. LUTGenerator   
 根据Gradient 生成对应的LUT贴图 方便材质球调色




### 2. 依赖的库  
1. NaughtyAttributes-2.1.0  
[NaughtyAttributes 下载地址](https://github.com/dbrizov/NaughtyAttributes/archive/refs/tags/v2.1.0.zip)
