#version 330 core
out vec4 FragColor;

in vec3 Normal;       // �Ӷ�����ɫ������ķ�����
in vec3 FragPos;      // �Ӷ�����ɫ�������Ƭ��λ��
in vec2 TexCoords;    // ��������

uniform sampler2D texture_diffuse1; // ����������
uniform vec3 lightPos;// ��Դλ��
uniform vec3 viewPos; // ���λ��
uniform vec3 lightColor;
uniform vec3 objectColor;
// ����ǿ��
uniform float ambientStrength; 
uniform float diffuseStrength;
uniform float specularStrength; 
// ����ǿ��
uniform vec3 lightPositions[4];
uniform vec3 lightColors[4];
// ��Ҫ���Ĳ���
void main() {
    vec3 ambient = vec3(0.0);
    vec3 diffuse = vec3(0.0);
    vec3 specular = vec3(0.0);

    for (int i = 0; i < 4; ++i) {
        // ���������,���ܹ�Դλ�úͷ����Ӱ�죬ֻ�ܻ�������ǿ�Ȳ���Ӱ��
        ambient += ambientStrength * lightColors[i];
        vec3 norm = normalize(Normal);// ��׼��������
        vec3 viewDir = normalize(viewPos - FragPos);// ���߷���
        // ����������
        vec3 lightDir = normalize(lightPositions[i] - FragPos);// ���߷���
        float diff = max(dot(norm, lightDir), 0.0);// ������ǿ��
        diffuse += diffuseStrength * diff * lightColors[i];// ���������ɫ
        // ��������
        vec3 reflectDir = reflect(-lightDir, norm);// ���淴�䷽��
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);// ���淴��ǿ��
        specular += specularStrength * spec * lightColors[i];// ���淴�����ɫ
    }

    vec3 result = (ambient + diffuse + specular) ;
    //FragColor = texture(texture_diffuse1, TexCoords);
    //�ı���Ⱦ����ɫ
    FragColor = vec4(result, 1.0) * texture(texture_diffuse1, TexCoords);
}
