##Vertex

#version 460 core

VertexInput {
    vec2 aPos : Position
    vec2 texCoord : TexCoord0
    vec4 color : Color
}

uniform mat3 objectToWorld;
uniform mat3 worldToView;
uniform mat3 viewToScreen;

out vec4 vertexColor;
out vec2 uv;

void main()
{
    vec3 worldPos = objectToWorld * vec3(aPos.xy, 1.0);
    vec3 viewPos = worldToView * vec3(worldPos.xy, 1.0);
    vec3 screenPos = viewToScreen * vec3(viewPos.xy, 1.0);

    gl_Position = vec4(screenPos.xy, 0.0, 1.0);
    vertexColor = color;
    uv = texCoord;
}

##Fragment

#version 460 core

in vec4 vertexColor;
in vec2 uv;

out vec4 FragColor;

uniform sampler2D mainTex;
uniform vec4 tint;

void main()
{
    FragColor = texture(mainTex, uv);// * tint * vertexColor;
}