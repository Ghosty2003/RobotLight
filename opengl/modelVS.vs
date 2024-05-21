#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
out vec3 FragPos;  // 传递给片段着色器的片段位置
out vec3 Normal;   // 传递给片段着色器的法线

// 主要更改部分
void main()
{
    // 计算变换后的顶点位置
    FragPos = vec3(model * vec4(aPos, 1.0));
    Normal = mat3(transpose(inverse(model))) * aNormal; // 法线变换
    TexCoords = aTexCoords;    // 传递纹理坐标给片段着色器
    gl_Position = projection * view * model * vec4(aPos, 1.0);// 计算最终的裁剪空间坐标
}
