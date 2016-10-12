using UnityEngine;
using System.Collections;

public class RectGridPosition : MonoBehaviour {

    public float m_margin = 0.1f;
    public int m_columns, m_rows;

    public int m_positionOffset = 4;

    private int m_position = 0;

	public void set(int position)
    {
        float width = (1f - 2*m_margin) / m_columns;
        float height = (1f - 2 * m_margin) / m_rows;

        m_position = position + m_positionOffset;

        int row = m_position / m_columns;
        int col = m_position % m_columns;

        RectTransform rectTransform = (RectTransform)transform;
        rectTransform.anchorMin = new Vector2(m_margin + width * col, 1 - m_margin - height * (row + 1));
        rectTransform.anchorMax = rectTransform.anchorMin + new Vector2(width, height);
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }
}
