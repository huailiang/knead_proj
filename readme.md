
## 游戏中捏脸的实现


本项目关于捏脸的实现主要参考了《楚留香》和《完美世界》手游， 目前主流的游戏关于捏脸的实现主要集中在两个方面：

1. 更改脸部骨骼，捏出不同脸的外形

2. 妆容，在基础的脸型上叠加不同的效果。


## Version

项目使用的unity版本是2018.2.1f， 打开unity工程，运行Example这个scene。


## 编辑器工具



Hierarchy窗口中选中Nose这根骨骼，随便调整Transform一个属性，比如说Position-X 拖拽到骨骼调动的最小值，然后在Inspector的BonesControl对应的面板点击
对应的左边按钮，然后拖拽Position-X到调动的最大值，点击对应属性右边的按钮。点击按钮点击之后，会把记录的值序列化到磁盘当中。
拖拽进度条，就可以预览效果。

Init按钮是保存fbx骨骼参数的初始值，Reset按钮是丢弃捏脸数据，恢复到初始值。

<br><img src='image/control.jpg'><br>

Hierarchy窗口中选中Fbx，即脸部的根节点。设置好每个部位对应的贴图和贴图在MainTexture中的偏移。运行Unity,调整Inspector中TextureToRT脚本里的参数（饱和度、
色相、浓淡），即可预览到对应的部位变化。

<br><img src='image/texture.jpg'><br>

### 骨骼

事先需要做好脸部骨骼的划分，哪些是由蒙皮动画控制的，哪些由程序调整的，然后需要把所有的骨骼都需要绑定到SkinedMeshRender上。

而捏脸控制的就是后者，这些骨骼不能再由Animation来改动。这里我们调整的骨骼参数是位置、旋转、缩放，其中旋转需要指定旋转的轴向。


<br><img src='image/bone.gif'><br>


### 妆容

事先把脸划分五个部位，分别是眉妆、眼影、瞳色、唇齿、面纹。 妆容的实现是由一张基础的脸型，然后在之基础上堆叠上部位的贴图。而调整的参数
就是色相、饱和度、浓淡三个参数。 调整好参数之后使用FaceMakeupShader，后处理生成一张RenderTexture, 然后把这张RT（RenderTexture）
再赋给之前的材质。


<br><img src='image/paint.gif'><br>


## 说明

本项目旨在展示捏脸的实现原理，在实际的项目开发过程中，其复杂度远远不止于此。 我们需要将预处理好编辑过的骨骼，导出编辑过的数据到二进制文件，
也不会直接在gameobject上挂载脚本。关于如何组织，就看读者的code 能力了。再比如说，你需要写一个工具，能自动裁剪妆容所在的区域，并导出偏移数
据。


## 联系方式

  <div class="text-gray-light mt-2 d-flex flex-items-center">
    <svg style="width: 16px;" class="octicon octicon-location" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M6 0C2.69 0 0 2.5 0 5.5 0 10.02 6 16 6 16s6-5.98 6-10.5C12 2.5 9.31 0 6 0zm0 14.55C4.14 12.52 1 8.44 1 5.5 1 3.02 3.25 1 6 1c1.34 0 2.61.48 3.56 1.36.92.86 1.44 1.97 1.44 3.14 0 2.94-3.14 7.02-5 9.05zM8 5.5c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
    <input class="ml-2 form-control flex-auto input-sm" placeholder="Location" aria-label="Location" name="user[profile_location]" value="Shanghai, China">
  </div>

    <div class="text-gray-light mt-2 d-flex flex-items-center">
      <svg style="width: 16px;" class="octicon octicon-mail" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M0 4v8c0 .55.45 1 1 1h12c.55 0 1-.45 1-1V4c0-.55-.45-1-1-1H1c-.55 0-1 .45-1 1zm13 0L7 9 1 4h12zM1 5.5l4 3-4 3v-6zM2 12l3.5-3L7 10.5 8.5 9l3.5 3H2zm11-.5l-4-3 4-3v6z"/></svg>
      <select name="user[profile_email]" id="user_profile_email" class="form-select form-control ml-2 flex-auto select-sm"><option value=""></option>
		<option selected="selected" value="peng_huailiang@qq.com">peng_huailiang@qq.com</option>
	  </select>
    </div>

  <div class="text-gray-light mt-2 d-flex flex-items-center">
    <svg style="width: 16px;" class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"/></svg>
    <input class="ml-2 form-control flex-auto input-sm" placeholder="Website" aria-label="Website" name="user[profile_blog]" value="https://huailiang.github.io">
  </div>