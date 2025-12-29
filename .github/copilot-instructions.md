# Copilot / AI agent instructions — NID-DGD-GD2-PhysicsProject

This file gives focused, actionable guidance to AI coding assistants working on this Unity 2D physics project.

- **Project type & Unity version**: Unity project (Editor version found in ProjectSettings/ProjectVersion.txt: `m_EditorVersion: 6000.3.2f1`). Open and run changes in the Unity Editor (do not assume a headless CLI build).

- **Where to look first**: key folders
  - [Assets/Scripts](Assets/Scripts) — main gameplay C# scripts (primary entry points to understand flow).
  - [Assets/Scenes](Assets/Scenes) — scene compositions and GameObject references (inspect in Editor).
  - [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) — Unity editor version to match when running.

- **Big picture / architecture**
  - This is a simple Unity 2D physics game driven by spawn/level logic: `Spawner` creates objects (potatoes/rocks), `LevelManager` and `Deleter` handle lifecycle, and `ScoreManager`/`ScoreKeeper` update scoring. Start by reading [Assets/Scripts/Spawner.cs](Assets/Scripts/Spawner.cs), [Assets/Scripts/LevelManager.cs](Assets/Scripts/LevelManager.cs) and [Assets/Scripts/Deleter.cs](Assets/Scripts/Deleter.cs) to understand data flow.
  - Communication is mostly via direct scene references and runtime lookups using `FindAnyObjectByType<T>()` rather than event buses. Respect existing coupling when making minimal edits.

- **Common code patterns to follow**
  - Serialized configuration: most prefabs/values are exposed via `[SerializeField]` and set in the Editor. Avoid replacing serialized fields with hard-coded values.
  - Initialization: `Awake()` is used for `GetComponent`/type lookups; `Start()` is used to begin coroutines (see `Spawner.Start`). Follow this pattern when adding new components.
  - Coroutines: used for spawn loops and UI flashes (see `Spawner.SpawnLoop()` and `ScoreManager.ShowUpdate`). Use `StartCoroutine`/`StopCoroutine` consistent with existing code.

- **Style and naming notes (follow existing repo conventions)**
  - Method naming is inconsistent (example: `killPlayer()` vs `PlayerWin()`), but most public methods are lowerCamel or Pascal; preserve the project’s local style when making small fixes.
  - Use `FindAnyObjectByType<T>()` where other scripts do—not `FindObjectOfType<T>()`—to keep consistency with Unity version patterns used.

- **Files that demonstrate important patterns (read these for examples)**
  - [Assets/Scripts/Spawner.cs](Assets/Scripts/Spawner.cs) — spawning logic, use of `Coroutine`, `MassRandomizer`, prefab `Instantiate` and `LevelManager` checks.
  - [Assets/Scripts/ScoreManager.cs](Assets/Scripts/ScoreManager.cs) — UI updates with `TextMeshProUGUI`, temporary update coroutine, and interaction with `ScoreKeeper`.
  - [Assets/Scripts/PlayerMovement.cs](Assets/Scripts/PlayerMovement.cs) — shows how movement is handled; note there is an apparent bug (invalid `rb.linearVelocityX` usage) — be careful when editing physics code.

- **Common pitfalls & safety checks for PRs**
  - Many fields are set in the Editor — changing a serialized field name or type will break scene links. If renaming/typing, update scene/prefab references in Unity Editor.
  - Changes that affect prefabs, Rigidbody2D mass, or physics behavior must be validated by running the Scene in Editor and observing gameobjects (visual verification).
  - Avoid large refactors across many scripts in a single PR; prefer small, testable changes that can be verified inside the Editor.

- **Debugging & run workflow**
  - Run and test inside the Unity Editor (Play mode). Use Visual Studio / Rider attached to the Editor for breakpoints. Typical developer flow: open project in Unity Editor → open C# project in IDE (double-click any script or open `Physics Game.slnx`).
  - Use `Debug.Log` for quick runtime inspection; look at Console in Unity Editor.

- **Builds and CI**
  - There is no project-specific CLI build script in the repo. Use Unity Editor or Unity Hub to build. If asked to produce automated builds, request the target Unity version and CI runner details before adding automation.

- **When making edits**
  - Prefer minimal, local changes. Example safe edits: fix the movement assignment in `PlayerMovement.FixedUpdate()` to set `rb.velocity` rather than touching serialized prefab connections.
  - If adding or modifying public API between managers (e.g., `LevelManager`, `ScoreKeeper`), update all call sites and run the scene in Editor.

If anything here is unclear or you want me to expand a section (for example, add a short checklist for validating scene references after renames), tell me which part to improve and I will update this file.
