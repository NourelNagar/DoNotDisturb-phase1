# Prototype Implementation Report
## The Haunting — Feature Documentation

---

## Overview

This report documents every system and mechanic implemented in the prototype of *The Haunting*, a first-person survival horror game. For each feature, the report explains what was built, how it works technically, and — critically — how it serves the game's narrative and design concept. The game's central premise is that the player is trapped in a haunted house with a malevolent ghost and must survive by using the environment cleverly, staying hidden, and managing limited resources.

---

## PLAYER ACTIONS

---

### 1. Door Interaction (`DoorInteraction.cs`)

**What it does:**
When the player approaches a door and presses **E**, the door smoothly rotates open or closed on its Y-axis using Quaternion Slerp interpolation. A context-sensitive prompt — *"[E] Open"* or *"[E] Close"* — appears on screen when the player is within interaction range. The door's frame collider is disabled when open to allow passage and re-enabled when closed.

**Technical implementation:**
- Proximity detection using `Vector3.Distance` against the Player tag
- Smooth rotation via `Quaternion.Slerp` coroutine at configurable speed
- Collider state management prevents the player from being blocked mid-transition
- New Input System (`Keyboard.current.eKey.wasPressedThisFrame`)

**Narrative and design contribution:**
Doors are the primary spatial gating mechanism in *The Haunting*. Every room in the house is separated by a door, and the ability to open and close them is the player's most fundamental act of agency in the world. Closing a door behind you creates a brief sense of safety — if the ghost is somewhere else in the house, a closed door buys precious seconds before it reaches you. The smooth, deliberate animation of the door (rather than an instant teleport) reinforces the physical weight of the world and the feeling that this is a real space. The interactive door is the first thing the player encounters, and its simplicity teaches the core interaction model (proximity + E key) that all other interactions build upon.

---

### 2. Lamp Toggle (`LampToggle.cs`)

**What it does:**
When the player stands near a lamp and presses **E**, the lamp's Point Light turns on or off. A prompt (*"[E] Turn On"* / *"[E] Turn Off"*) appears when the player is within range. Multiple lamps across the house can each be independently toggled.

**Technical implementation:**
- Automatically finds the `LampLight` child component using `transform.Find("LampLight")`
- Falls back to `GetComponentInChildren<Light>()` if the named child is not found
- Null guards prevent crashes when the light reference is missing
- New Input System input detection consistent with all other interaction scripts

**Narrative and design contribution:**
Light and darkness are the core tension axes of *The Haunting*. The ghost is attracted to light sources — a design principle introduced via the bathroom mirror message (*"It sees the light. Darkness is safety"*). This creates a direct strategic dilemma every time the player encounters a lamp: turn it on for better visibility and navigational safety, or leave it off to remain hidden? A lamp left on in the bedroom while the player explores the master bedroom above acts as a passive lure, drawing the ghost downstairs and buying time. This mechanic transforms ordinary household objects into tactical tools, deepening the player's relationship with the environment and making every room feel like a puzzle rather than just a corridor.

---

### 3. Item Pickup (`ItemPickup.cs`)

**What it does:**
When the player approaches a collectible object and presses **E**, the object is removed from the world (deactivated), a pickup counter increments, and a log message confirms the collection. The item name is configurable per object, and the prompt shows the specific item name (*"[E] Pick up Key"*).

**Technical implementation:**
- Static `_itemsCollected` counter shared across all instances tracks total pickups
- Proximity detection using the Player tag with configurable `interactionDistance`
- `gameObject.SetActive(false)` removes the item from the scene on pickup
- `GetItemsCollected()` static method exposes the count for use by other systems

**Narrative and design contribution:**
The item pickup system is the backbone of *The Haunting*'s puzzle structure. The game's entire escape sequence — finding Key Fragment 1 in the bathroom, Key Fragment 2 in the master bedroom, combining them into the Master Key, and using it on the front door — is built on this single mechanic. Every collectible tells a story: the fragments suggest a door that was once deliberately sealed, the matchbox implies someone prepared this trap, the journal reveals the ghost's origin. The act of picking up an object transfers knowledge and power from the world to the player, making exploration feel meaningful rather than decorative. The visible disappearance of items from the environment also provides clear feedback that the player's actions have permanently altered the world state.

---

### 4. Camera Hide / Crouch (`CameraSwitcher.cs`)

**What it does:**
When the player presses **H**, the camera smoothly slides down to a low crouched height (from approximately eye level at ~1.375 m down to ~0.4 m off the ground). Pressing **H** again returns the camera to standing height. The transition is animated using `Mathf.Lerp` at a configurable speed (default 5 units/second), creating a fluid cinematic lowering effect.

**Technical implementation:**
- Modifies the `localPosition.y` of the `PlayerCameraRoot` transform each frame via Lerp
- On `Start()`, reads the initial camera root height from its current local position so it adapts to any player scale
- No Cinemachine camera switching required — operates directly on the camera root transform
- `_targetHeight` float toggles between `standHeight` and `crouchHeight` on H key press

**Narrative and design contribution:**
Hiding is the defining survival mechanic of *The Haunting*. The player has no weapon, no means of fighting back — their only defence is avoidance. The H-key hide mechanic physically expresses the act of crouching behind furniture: sofa backs, beds, and countertops all become potential shields. When the camera drops to 0.4 m — below the sight line of most furniture — the player is experiencing their character's fear made tangible. They are literally making themselves small. The smooth, cinematic Lerp animation (rather than an instant snap) is also a camera transition in itself: the world shifts perspective, shadows loom larger, and the ceiling disappears from view. This is not just a gameplay mechanic but a psychological one — crouching feels vulnerable and desperate in a way that standing never does, reinforcing the core horror of powerlessness.

---

### 5. Player Health & Ghost Damage (`PlayerHealth.cs`)

**What it does:**
A health bar is displayed in the top-left corner of the screen showing current HP (0–100). The bar changes colour from green to red as health decreases. When the ghost is within a configurable damage radius (default 6 world units), the player loses health at a rate of 20 HP per second. At 0 HP, the scene reloads, restarting the run.

**Technical implementation:**
- Distance-based damage detection using `Vector3.Distance` between player and ghost transforms
- Ghost is located at runtime using `GameObject.FindGameObjectWithTag("Ghost")` with a try-catch to handle missing tag definitions gracefully
- Health bar rendered via `OnGUI` using layered `GUI.Box` calls: black background, coloured fill, white label
- `Color.Lerp(Color.red, Color.green, ratio)` creates a smooth colour gradient across the health states
- `SceneManager.LoadScene` reloads the current scene on death

**Narrative and design contribution:**
The health system makes the ghost's presence tangible as a physical threat. Before this system, the ghost was a visual presence only — a wandering model that could be walked through without consequence. The health bar gives the encounter genuine stakes. Each second spent near the ghost is measurable, visible damage. The gradient colour shift from green to red mirrors the player's rising panic — as the bar shrinks and reddens, the psychological pressure to flee mounts. The instant scene reload on death is also a deliberate narrative choice: in *The Haunting*, there are no checkpoints, no respawns, no second chances. The house resets. The ghost forgets. The player must start again. This reinforces the horror concept that the house itself is inescapable — you can only leave by completing it correctly.

---

## LIGHTING BEHAVIORS

---

### 6. Static Directional Night Light

**What it does:**
A directional light positioned at a 50° pitch and -30° yaw with intensity 2.0 provides the base ambient illumination for the entire scene, simulating deep moonlight filtering into the house.

**Narrative and design contribution:**
The fixed, unvarying night establishes the game's temporal premise: this all happens in a single night, approximately 2:00 AM, and it will not end until the player escapes or dies. The cool, desaturated ambient colour (blue-grey sky tone) prevents total darkness in corridors while keeping rooms dim enough that the flashlight remains useful. It creates long, directional shadows from furniture — the bed casts a shadow across the bedroom floor, the wardrobe looms — that make the environment feel haunted even when the ghost is elsewhere.

---

### 7. Player-Controlled Flashlight (`FlashlightController.cs`)

**What it does:**
Pressing **F** toggles a Spot Light attached to the player's camera. The light is off by default. When on, it emits a directed cone of light in the player's facing direction. When off, the world returns to ambient moonlight only.

**Technical implementation:**
- References a `Light` component (Spot Light child of camera) via Inspector or auto-discovery via `GetComponentInChildren<Light>()`
- Null guards prevent crashes if no light is assigned
- `light.enabled = !light.enabled` on F key press using New Input System

**Narrative and design contribution:**
The flashlight is the player's most important and most dangerous tool simultaneously. It illuminates the unknown — dark rooms, hidden items, the ghost in the shadows — but it also broadcasts the player's position. The ghost moves toward light sources, making the flashlight both a necessity and a risk. The act of pressing F to turn it off is one of the most tense moments in the game: choosing to go blind in the hope of becoming invisible. This creates a rhythm of light and darkness that defines each exploration sequence. The player learns to use the flashlight in short bursts — scan a room, turn it off, listen, move — a survival behaviour that the game encourages through consequence rather than instruction.

---

### 8. Lamp Toggle as Event-Triggered Lighting (`LampToggle.cs`)

**What it does:**
Individual lamps throughout the house can be switched on or off by the player, changing the lighting state of their respective rooms.

**Narrative and design contribution:**
As described in the Player Actions section, the lamp toggle doubles as an event-triggered lighting change — pressing E near a lamp is a player-triggered event that alters the environment's lighting state. This satisfies the requirement for an environment-triggered lighting behavior while also serving the game's tactical light management design. See Player Action #2 above for full narrative context.

---

## CAMERA BEHAVIORS

---

### 9. First-Person Cinemachine Camera

**What it does:**
A Cinemachine Virtual Camera locked to the player's `PlayerCameraRoot` transform provides the primary first-person perspective. The camera follows the player's position and rotation, providing a direct eye-level view of the world.

**Narrative and design contribution:**
First-person perspective is the genre standard for immersive horror for a clear reason: it eliminates the psychological distance between the player and the character. There is no avatar on screen to observe from a safe external vantage point — the player *is* in the house. When the ghost appears at the edge of the flashlight beam, there is no camera cut, no cutscene, no narrative mediation — just the player, looking at it directly. The Cinemachine implementation provides smooth, professional camera movement without custom transform manipulation, ensuring the experience feels polished.

---

### 10. Hide Camera Transition (`CameraSwitcher.cs`)

**What it does:**
Pressing **H** triggers a smooth cinematic camera transition, lowering the view from standing height to a crouched perspective using a Lerp animation. This constitutes both a second distinct camera perspective (the crouched view) and a cinematic transition (the smooth animated movement between heights).

**Narrative and design contribution:**
As detailed in Player Action #4, the hide camera serves dual purposes: it is a gameplay crouch mechanic and a cinematic expression of fear. The smooth downward movement of the camera is the closest the game gets to a traditional cutscene — a deliberate, animated shift in perspective that communicates meaning. When the camera lowers and the player crouches behind the sofa as the ghost's footsteps approach, the restricted low-angle view creates a powerful sense of vulnerability. The player cannot see what is happening above the furniture line. They must trust that they are hidden. This uncertainty is the game's most effective horror beat.

---

## AI BEHAVIOR

---

### 11. Ghost Patrol System (`GhostWander.cs`)

**What it does:**
The ghost (DemonDoll model) navigates the house using Unity's NavMesh system. It randomly selects destinations within a configurable wander radius (35 m), moves to each destination using a NavMeshAgent, waits for a random interval (2–5 seconds), then selects a new destination. This creates unpredictable, organic patrol patterns that cover the entire house.

**Technical implementation:**
- `NavMeshAgent.SetDestination()` drives pathfinding across the baked NavMesh
- `NavMesh.SamplePosition()` validates random points against the NavMesh before setting them as targets (up to 10 attempts per cycle)
- `Random.insideUnitCircle` projects destinations onto a flat 2D plane to prevent the ghost from attempting to navigate to elevated positions
- Wait timer uses `Random.Range(minWaitTime, maxWaitTime)` to create irregular, unpredictable movement cadence

**Narrative and design contribution:**
The ghost's random wander pattern is the engine of *The Haunting*'s tension. Unlike scripted patrol routes (which players memorise and exploit), the NavMesh-based random wander means the ghost is genuinely unpredictable — it might be in the bedroom, the hallway, or the master bedroom at any given moment. The player can never be certain a room is safe without checking. This unpredictability is essential to the horror premise: the ghost is not a puzzle to be solved but a presence to be feared. The 2–5 second wait between movements mimics a living entity pausing to listen or look around, giving the ghost a quality of eerie intentionality even though its behaviour is procedurally random. Every exploration of the house becomes a negotiation with this unpredictability.

---

## SUMMARY TABLE

| Feature | Script | Input | Narrative Role |
|---------|--------|-------|---------------|
| Door Open/Close | `DoorInteraction.cs` | E key | Spatial gating, tactical ghost management |
| Lamp Toggle | `LampToggle.cs` | E key | Light as a weapon and lure |
| Item Pickup | `ItemPickup.cs` | E key | Puzzle progression, world interaction |
| Camera Hide/Crouch | `CameraSwitcher.cs` | H key | Core survival mechanic, cinematic fear |
| Player Health | `PlayerHealth.cs` | Passive | Stakes, ghost threat made tangible |
| Directional Night Light | Scene | None | Atmospheric base, temporal setting |
| Flashlight Toggle | `FlashlightController.cs` | F key | Risk/reward visibility management |
| Lamp Lighting Change | `LampToggle.cs` | E key | Event-triggered environmental change |
| FPS Camera | Cinemachine | Mouse | Immersion, direct player-world connection |
| Hide Camera Transition | `CameraSwitcher.cs` | H key | Cinematic fear expression |
| Ghost Patrol | `GhostWander.cs` | Passive AI | Unpredictable threat, tension engine |

---

*Report compiled for Assignment 2 — The Haunting | Unity 6 URP | NourelNagar*
