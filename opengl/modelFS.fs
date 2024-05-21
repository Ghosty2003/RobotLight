#version 330 core
out vec4 FragColor;

in vec3 Normal;       // 从顶点着色器传入的法向量
in vec3 FragPos;      // 从顶点着色器传入的片段位置
in vec2 TexCoords;    // 纹理坐标

uniform sampler2D texture_diffuse1; // 漫反射纹理
uniform vec3 lightPos;// 光源位置
uniform vec3 viewPos; // 相机位置
uniform vec3 lightColor;
uniform vec3 objectColor;
// 光照强度
uniform float ambientStrength; 
uniform float diffuseStrength;
uniform float specularStrength; 
// 光照强度
uniform vec3 lightPositions[4];
uniform vec3 lightColors[4];
// 主要更改部分
void main() {
    vec3 ambient = vec3(0.0);
    vec3 diffuse = vec3(0.0);
    vec3 specular = vec3(0.0);

    for (int i = 0; i < 4; ++i) {
        // 环境光计算,不受光源位置和方向的影响，只受环境光照强度参数影响
        ambient += ambientStrength * lightColors[i];
        vec3 norm = normalize(Normal);// 标准化法向量
        vec3 viewDir = normalize(viewPos - FragPos);// 视线方向
        // 漫反射光计算
        vec3 lightDir = normalize(lightPositions[i] - FragPos);// 光线方向
        float diff = max(dot(norm, lightDir), 0.0);// 漫反射强度
        diffuse += diffuseStrength * diff * lightColors[i];// 漫反射光颜色
        // 镜面光计算
        vec3 reflectDir = reflect(-lightDir, norm);// 镜面反射方向
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);// 镜面反射强度
        specular += specularStrength * spec * lightColors[i];// 镜面反射光颜色
    }

    vec3 result = (ambient + diffuse + specular) ;
    //FragColor = texture(texture_diffuse1, TexCoords);
    //改变渲染的颜色
    FragColor = vec4(result, 1.0) * texture(texture_diffuse1, TexCoords);
}
