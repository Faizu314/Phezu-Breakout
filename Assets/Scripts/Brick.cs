using PhezuEngine;

namespace Game
{
    public struct BrickData
    {
        public Vector2 Position;
        public Color[] Colors;
        public int Health;
    }
    
    public class Brick : BehaviourComponent
    {
        private Transform m_Transform;
        private Renderer m_Renderer;
        private Material m_Material;
        private IBrickListener m_Listener;

        private Color[] m_Colors;
        private int m_TotalHealth;
        private int m_CurrentHealth;
        private bool m_IsUnbreakable;
        
        public bool IsUnbreakable => m_IsUnbreakable;

        private void OnCreate()
        {
            m_Transform = Entity.GetComponent<Transform>();
            m_Renderer = Entity.GetComponent<Renderer>();
            Material material = m_Renderer.Material;
            m_Material = Material.Create(material);
            m_Renderer.Material = m_Material;
        }
        
        public void Initialize(IBrickListener listener, BrickData brickData)
        {
            m_Listener = listener;
            m_Transform.Position = brickData.Position;
            m_IsUnbreakable = brickData.Health <= 0;
            m_TotalHealth = brickData.Health;
            m_CurrentHealth = m_TotalHealth;
            m_Colors = brickData.Colors.Clone() as Color[];

            if (m_IsUnbreakable)
                m_Material.SetColor("tint", m_Colors[0]);
            else
                m_Material.SetColor("tint", m_Colors[m_CurrentHealth - 1]);
        }

        public void OnHit() {
            if (m_IsUnbreakable)
                return;
            
            m_CurrentHealth--;

            if (m_CurrentHealth <= 0)
            {
                m_Listener.OnBrickDestroyed(this);
                Entity.Destroy();
                return;
            }

            m_Material.SetColor("tint", m_Colors[m_CurrentHealth - 1]);
        }
    }
}