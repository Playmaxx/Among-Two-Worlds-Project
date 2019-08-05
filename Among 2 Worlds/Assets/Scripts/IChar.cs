using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChar
{
    void movement();
    void jump();
    void dash();
    void glide();
    void wallaction();
}
