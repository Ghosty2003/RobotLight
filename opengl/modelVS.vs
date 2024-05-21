#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
out vec3 FragPos;  // ���ݸ�Ƭ����ɫ����Ƭ��λ��
out vec3 Normal;   // ���ݸ�Ƭ����ɫ���ķ���

// ��Ҫ���Ĳ���
void main()
{
    // ����任��Ķ���λ��
    FragPos = vec3(model * vec4(aPos, 1.0));
    Normal = mat3(transpose(inverse(model))) * aNormal; // ���߱任
    TexCoords = aTexCoords;    // �������������Ƭ����ɫ��
    gl_Position = projection * view * model * vec4(aPos, 1.0);// �������յĲü��ռ�����
}
