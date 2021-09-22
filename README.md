# ObjectsPreWarmer
Distribute scene objects' initialization process (i.e. `Awake()` calls) evenly throughout multiple frames, easing scene loading processes.
## How to Use
1. Put `ObjectsPreWarmer.cs` on a scene-level GameObject component.
2. Select the GameObject, and press the lock icon on the inspector to lock the inspector view to the GameObject.
3. Use shift-click to select multiple objects that you wish to be prewarmed into the field `Pre Warm Objects`.
4. (Optional) You can change wheter you want to pre-warm during awake, the interval between awake processes (default interval is `Time.deltaTime`), and how many GameObjects you wish to prewarm during each operation.
5. Enter Play Mode to see the pre-warm results.
## How Does it Work
- When a scene with a lot of GameObjects are loaded, the initialization calls can sometimes slow down the game at the beginning.
- With `ObjectsPreWarmer`, you can even the initialization calls across multiple frames, optimizing scene loading processes.
- Make sure this script runs first in each scene by changing the Script Execution Order, this is done with `[DefaultExecutionOrder(-15)]` in the script.
