using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChar
{
    void movement(Vector2 moveVector);
    void jump();
    void dash(Vector2 moveVector);
    void glide();
    void wallaction();
}
