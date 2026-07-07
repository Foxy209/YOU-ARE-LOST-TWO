using UnityEngine;
using UnityEngine.EventSystems;
public class ES : StandaloneInputModule
{
    public override void Process()
    {
        // Временно возвращаем timeScale для обработки ввода
        float oldTimeScale = Time.timeScale;
        Time.timeScale = 1f;
        base.Process();
        Time.timeScale = oldTimeScale;
    }
}
