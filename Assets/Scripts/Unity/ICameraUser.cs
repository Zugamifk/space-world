using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraUser {
    void AcquireCamera(Camera camera);
    Camera Camera { get; }
}
