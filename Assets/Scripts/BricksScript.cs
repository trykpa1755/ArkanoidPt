using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksScript : MonoBehaviour
{
    //Zmienna prywatna, któr¹ mo¿na zmieniaæ w inspektorze
    //Iloœæ ¿ycia naszej cegie³ki
    
    public int life = 1;

    //Za ka¿dym razem jak ona siê zetknie z czymœ
    private void OnCollisionEnter(Collision collision)
    {
        //Je¿eli ma to tag Ball
        if(collision.gameObject.tag == "Ball")
        {
            //usuawamy jedno ¿ycie
            life--;
            //Je¿eli ¿ycia s¹ równe lub mniejsze od 0
            if(life <= 0)
            {
                GameManager.instance.bricks.Remove(this.gameObject);
                Destroy(gameObject); //Zniszcz obiekt
                GameManager.instance.UpdateUI();
            }
        }
    }

    public void SetBrick(int life)
    {
        this.life = life;
        if (life <= 0) Destroy(gameObject);
        BrickColor();
    }
    void BrickColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color[] colors = { Color.white, Color.blue, Color.red, Color.green };
        int colorIndex = Mathf.Clamp(life - 1, 0, colors.Length - 1);
        renderer.material.color = colors[colorIndex];
    }
}
