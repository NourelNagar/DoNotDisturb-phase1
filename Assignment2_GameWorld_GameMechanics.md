# Assignment 2: Game World and Game Mechanics

**Game Title:** *The Haunting*
**Genre:** First-Person Survival Horror
**Platform:** PC
**Engine:** Unity 6 (Universal Render Pipeline)
**Date:** October 18, 2026

---

## Game Overview

*The Haunting* is a first-person survival horror game in which the player finds themselves inexplicably trapped inside a large, seemingly ordinary residential house — one that has become home to a malevolent supernatural entity. The objective is to explore the house, collect key items scattered across its rooms, and unlock the front door to escape before the ghost hunts the player down. Every creak of a floorboard, every flicker of a light, and every closed door is a potential threat.

The game draws on classic horror game design principles: resource scarcity, atmospheric dread, limited player power, and a persistent enemy that cannot be defeated — only avoided. The player's primary tools are a flashlight, their own movement, and their ability to use the environment to hide and mislead the ghost.

---

# PART 1: GAME WORLD

---

## 1. Locations

The game world consists of a single, seamless two-floor residential house surrounded by an impenetrable fog-shrouded property. The interior is divided into six distinct locations, each with its own visual identity, function, and gameplay significance. There are no loading screens — the player navigates the full house continuously, which reinforces the sense of spatial presence and genuine threat from the roaming ghost.

---

### 1.1 The Bedroom

**Description:**
The bedroom is the first area the player awakens in at the start of the game. It measures approximately 6 m × 5 m with a ceiling height of 2.8 m. The room features worn hardwood flooring, faded floral wallpaper, and a single window sealed shut with nails — communicating immediately that escape is not a simple matter of opening a window. Dim moonlight filters through cracked curtains, casting long, distorted shadows across the furniture.

**Terrain & Landscape:**
The floor is flat and largely clear of obstructions except for furniture. A bed sits against the far wall; a wardrobe lines the right wall; a vanity mirror sits in the corner near the door. A bedside table with a toggleable lamp occupies the space between the bed and the wall. The furniture is arranged to force the player to move in predictable arcs, making navigation under pressure (when the ghost is nearby) genuinely stressful.

**Environmental Characteristics:**
- Ambient lighting is extremely low, immediately teaching the player that the flashlight is essential.
- Dust particles float in the air via a URP particle system, visible in the flashlight beam.
- The vanity mirror occasionally reflects the ghost behind the player — a passive warning mechanic that rewards attentive players.
- A post-processing breath-fog effect reinforces the supernatural cold atmosphere.
- The wallpaper has patches of dark staining that suggest something violent happened in this room before the game begins.

**Connections:**
The bedroom connects to the central hallway through a single interactive door (the *HouseDoor* prefab currently implemented in the prototype). The door swings inward on the Y-axis. It is the first door the player interacts with, teaching them the E-key interaction system.

---

### 1.2 The Kitchen

**Description:**
A utilitarian space measuring approximately 5 m × 4 m with cold tile flooring, wooden cabinets lining two walls, a refrigerator, stove, sink, and a central counter island. The kitchen is the most resource-rich area of the house — the player can find food items (consumables), a flashlight battery, a matchbox, and a drawer key here. This encourages thorough exploration despite the inherent risk of spending time in any one area.

**Terrain & Landscape:**
The counter island creates natural chokepoints that the player must navigate around. Cabinets open via the E-key interaction, revealing items or empty interiors. The refrigerator's contents are visible when the door is pulled open — a small detail that rewards curiosity. The island also provides partial cover when the ghost enters the kitchen.

**Environmental Characteristics:**
- An overhead fluorescent light flickers intermittently, creating an unsettling strobe effect that also makes it harder to see clearly.
- A window above the sink shows the foggy exterior, with rain visually animated on the glass and audible as background audio.
- The stove has a glowing burner providing minor ambient warm light even when no other light source is active.
- Cabinets can be opened and left open, creating permanent visual changes in the room that reflect player exploration.

**Connections:**
The kitchen connects to the hallway via an open archway (no door, allowing the ghost to enter freely). It also has a locked back door requiring the *Back Door Key* found in the master bedroom — this creates a delayed reward loop that encourages returning to locations.

---

### 1.3 The Bathroom

**Description:**
A narrow, claustrophobic space measuring 3 m × 2.5 m with cold tile floors, a pedestal sink, a bathtub with a shower curtain, and a toilet. The bathroom is a critical location because *Master Key Fragment 1* is hidden behind the medicine cabinet mirror. It is also the site of the game's most effective jump scare — the shower curtain reveal.

**Terrain & Landscape:**
The confined dimensions severely limit navigation. The shower curtain spans the full width of the bathtub alcove and can be pulled open. The medicine cabinet is mounted above the sink and opens outward. The narrow floor space means there is effectively no room to maneuver if the ghost enters — escaping requires getting past it in the doorway.

**Environmental Characteristics:**
- A single bare bulb hanging from the ceiling provides cool, clinical light — the only room with reliable built-in illumination that the player doesn't control.
- The mirror above the sink is fogged with moisture. Interacting with it wipes it clean, revealing a message scrawled in the condensation: *"It sees the light. Darkness is safety."* — a direct gameplay hint delivered through environmental storytelling.
- The bathtub is filled with dark, murky water. Its surface occasionally ripples for no apparent reason.
- Tile grout lines and water stains create a general sense of decay.

**Connections:**
The bathroom connects to the hallway via a swinging door.

---

### 1.4 The Hallway

**Description:**
The central spine of the house, the hallway runs approximately 12 m in length and 2 m in width, connecting all ground-floor rooms and containing the staircase to the upper floor. The walls are lined with framed family photographs — some of which have been defaced with a dark substance. A runner rug muffles footstep sounds when traversed (a design detail that rewards players who notice: footstep audio volume is reduced while on the rug).

**Terrain & Landscape:**
The narrow width makes the hallway the most dangerous traversal zone when the ghost is active — there is minimal room to dodge or hide. A broken floor lamp lies near the staircase base, creating a minor navigation obstacle. The staircase rises at a 35-degree incline along the right wall, with a wooden banister the player can lean past to see the upper landing.

**Environmental Characteristics:**
- Gas-style wall sconces line both sides of the hallway. They are all activated simultaneously by a single switch near the front door — the first non-door interactive object the player encounters. When active, the hallway is well-lit, but this also makes the player more visible to the ghost.
- The hallway is the only area from which all other rooms are visible simultaneously, making it the highest-risk location during ghost encounters.
- The grandfather clock at the far end of the hallway (near the living room passage) serves as the game's ambient sound anchor, its ticking audible from any location in the house.

**Connections:**
The hallway connects to: the Bedroom (door), the Kitchen (open archway), the Bathroom (door), the Living Room (open passage), and the Upper Floor (staircase).

---

### 1.5 The Living Room

**Description:**
The largest room in the house at 8 m × 7 m with a 3.2 m ceiling. It features a stone fireplace, a large sofa and armchairs, a media console with a television set, floor-to-ceiling bookshelves, and a central coffee table. The living room contains one of the game's most important puzzles: lighting the fireplace to melt the wax seal on a locked box. It is also where the player first experiences the television's ghost proximity warning.

**Terrain & Landscape:**
Furniture arrangement creates multiple cover zones — the player can hide behind the sofa or between the bookshelf and the wall. The fireplace occupies the entire south wall. The bookshelves are interactive — pulling the correctly colored book (red spine) in sequence reveals a hidden compartment. The coffee table holds the wax-sealed box as a prominent visual focal point, directing player attention.

**Environmental Characteristics:**
- Without the fireplace lit, this is the darkest room in the house — heavy curtains completely block any exterior light.
- When the fireplace is active, it casts dynamic, flickering warm-toned light across the room, dramatically changing the atmosphere from oppressive to (temporarily) almost inviting.
- The television turns on by itself when the ghost is within 8 m, displaying static with a buzzing audio cue — a proximity warning system.
- The grandfather clock's ticking is loudest here. Its chimes (every 15 in-game minutes) mask footstep audio, providing brief safe movement windows.

**Connections:**
The living room connects to the hallway via an open passage.

---

### 1.6 The Upper Floor / Master Bedroom

**Description:**
Accessed via the hallway staircase. The upper floor consists of a short landing (3 m × 2 m) and the master bedroom (7 m × 6 m). The master bedroom is the ghost's primary patrol zone — it is the most dangerous location in the house. It contains *Master Key Fragment 2* (inside a locked box on the dresser), the *Back Door Key*, and a journal that explains the narrative backstory of the haunting. The risk-reward calculation here is the most demanding in the game.

**Terrain & Landscape:**
The master bedroom has a sloped ceiling on the window side, following the roofline. A king-size bed dominates the center of the room. A large wardrobe, a writing desk (with lamp and journal), and a dresser fill the remaining space. The wardrobe is fully enterable — pressing E while facing it places the player inside, crouched in darkness. While hidden in the wardrobe, the ghost cannot detect the player for up to 10 seconds, but if the player stays longer or exits at the wrong moment, detection is likely.

**Environmental Characteristics:**
- A skylight in the sloped ceiling provides faint, cold moonlight — enough to navigate without the flashlight if the player moves slowly.
- The ghost's wandering radius (35 m in the prototype GhostWander script) is centered on this floor, meaning it will frequently return here.
- The writing desk lamp can be toggled by the player. Turning it on illuminates the desk items but also functions as a lure — the ghost will navigate toward the light source.
- The dresser is locked and requires the *Wax Box Key* to open.

**Connections:**
The master bedroom connects to the landing, which connects back to the hallway staircase.

---

### World Connectivity Summary

All six locations form a single continuous, seamless space. The ghost can traverse the same doors and passages as the player (it uses the same NavMesh baked across the house). This means every room the player enters carries genuine risk, and the player must always be aware of the ghost's last known position relative to their own. The connected layout encourages players to develop spatial memory of the house — knowing which rooms have cover, which have resources, and which routes offer the fastest traversal is a core skill.

---

## 2. Objects

The game features 33 distinct interactive, collectible, and environmental objects. These span multiple categories: weapons (the flashlight as a defensive tool), tools, resources, interactive devices, quest items, and environmental props.

---

| # | Object | Type | Description & Gameplay Role |
|---|--------|------|------------------------------|
| 1 | **Flashlight** | Tool / Light Source | The player's primary tool. Emits a 35° cone of directional light up to 15 m. Battery-powered — drains at 1% per 3 seconds while active. Low battery causes visible flickering. Pressing F toggles it on/off. Turning it off is the primary stealth mechanic, since the ghost is attracted to light. |
| 2 | **Flashlight Battery** | Consumable | Found in the kitchen drawer. Instantly restores the flashlight battery to 100%. Single-use. The only battery in the game, making it a high-value decision: use it now or save it? |
| 3 | **Bedroom Key** | Key / Quest Item | Unlocks the bedroom door from the hallway side. Found on the bedside table in plain view — the first item the player collects. Designed to teach the pickup interaction and inventory system without pressure. |
| 4 | **Master Key Fragment 1** | Key / Quest Item | Hidden behind the bathroom medicine cabinet. Combined with Fragment 2 in the inventory to craft the *Master Key*. Neither fragment is usable alone. |
| 5 | **Master Key Fragment 2** | Key / Quest Item | Located in the locked box on the master bedroom dresser. Requires the *Wax Box Key* to access. The final piece of the escape puzzle. |
| 6 | **Back Door Key** | Key / Quest Item | On the master bedroom writing desk. Unlocks the kitchen back door, providing an alternate ground-floor exit route (to the enclosed porch) useful for fleeing the ghost. |
| 7 | **Wax Seal Box** | Container / Puzzle Object | Sits on the living room coffee table. Sealed with wax that physically melts (shader animation) when the fireplace is lit. The visual change rewards players who make the connection between the fireplace puzzle and the box. Contains the *Wax Box Key*. |
| 8 | **Wax Box Key** | Key / Quest Item | Inside the wax seal box. Opens the locked dresser in the master bedroom. One link in the game's primary key chain. |
| 9 | **Matchbox** | Tool / Quest Item | Found in the kitchen on the counter next to the stove. Used once to light the living room fireplace. Consumed on use — cannot be un-lit. |
| 10 | **Fireplace** | Interactive Environment | In the living room. Activated by using the matchbox while facing it. Emits dynamic warm light (a Unity Point Light with flickering intensity), crackling audio, and melts the wax seal on the box nearby. Remains lit for the rest of the game once activated. |
| 11 | **Bedroom Lamp** | Toggleable Light | On the bedside table. Pressing E while facing it toggles it on/off. When on, emits a warm point light (range 4 m) that attracts the ghost toward the bedroom — a risk/reward decision for players who want to use it to lure the ghost away from a critical area. |
| 12 | **Master Bedroom Desk Lamp** | Toggleable Light | In the master bedroom. Same toggle mechanic as the bedroom lamp. Particularly useful for reading the journal but especially dangerous given the ghost's patrol pattern on that floor. |
| 13 | **Medicine Cabinet** | Interactive Container | In the bathroom above the sink. Opens outward when the player presses E while facing it. Reveals *Key Fragment 1* and a *First Aid Kit*. The mirror door wipes clean on interaction, revealing the navigation hint. |
| 14 | **First Aid Kit** | Consumable | Inside the medicine cabinet. Restores 50% of player health upon use. Single-use. The most powerful healing item in the game — its location in the bathroom requires crossing the ghost's patrol path to reach it. |
| 15 | **Journal / Diary** | Narrative Item | On the master bedroom desk. Interacting opens a full-screen text overlay styled as a handwritten journal, revealing the backstory of the house's former occupant and explaining the ghost's origin. Reading it does not pause the game. |
| 16 | **Framed Photographs** | Environmental Props | Six photographs mounted in the hallway. Each can be examined (E key) to display a close-up view with a narrative caption. Collectively, they tell the ghost's backstory through environmental storytelling rather than cutscenes. |
| 17 | **Enterable Wardrobe** | Interactive Hiding Spot | In the master bedroom. Pressing E places the player inside the wardrobe in a crouched, immobile state. The ghost cannot detect the player for up to 10 seconds. A timer or heartbeat audio cue signals when detection becomes likely. The only hiding mechanic in the game. |
| 18 | **Bookshelf (Puzzle)** | Interactive Puzzle Object | In the living room. Interacting with books along the shelf in a specific order (red book last) triggers a click and slides a hidden panel open in the bottom section, revealing a compartment. An auditory cue (grinding stone) signals when the sequence is wrong. |
| 19 | **Hidden Compartment Note** | Narrative Item | Inside the bookshelf compartment. A paper note with a numeric sequence (the padlock combination) and additional lore. Also hints at the ghost's weakness. |
| 20 | **Kitchen Refrigerator** | Interactive Container | Opens outward on E press (door-swing animation). Contains *Canned Goods* and the *Flashlight Battery*. Produces a low electrical hum, which can mask the player's footstep audio when they are standing near it. |
| 21 | **Canned Goods** | Consumable | Inside the refrigerator. Restores 25% of player health. Weaker than the first aid kit but more accessible and not requiring a dangerous detour to the upper floor. |
| 22 | **Hallway Light Switch** | Interactive Device | Mounted on the wall near the front door. Pressing E activates all hallway wall sconces simultaneously. The bright, fully illuminated hallway is faster to navigate but makes the player more visible to the ghost. A direct tradeoff decision. |
| 23 | **Television Set** | Environmental Prop / Proximity Warning | In the living room media console. Activates automatically (static image, buzzing audio) when the ghost entity is within 8 m. Cannot be controlled by the player. Functions as the game's most reliable ghost detection system in the living room area. |
| 24 | **Grandfather Clock** | Environmental Prop / Pacing Mechanic | In the living room corner. Ticks continuously as the primary ambient sound. Chimes every 15 real-world minutes, each chime signaling an increase in the ghost's movement speed. The audible heartbeat of the game's escalation system. |
| 25 | **Shower Curtain** | Interactive Environmental Prop | In the bathroom. Pulling it open with E plays a curtain-slide animation and either reveals an empty bathtub or, in one scripted instance, triggers a ghost jump scare (the ghost vanishes immediately after). The signature horror moment of the game. |
| 26 | **Bedroom Window** | Environmental Prop | Nailed shut from outside. Interacting with it displays the message: *"The nails have been hammered in from the outside."* This single line of text reframes the threat — the player was trapped intentionally. |
| 27 | **Kitchen Sink** | Environmental Prop | Produces a constant dripping sound when the player is within 2 m. Non-interactable beyond flavor. Used to help orient the player in the kitchen layout. |
| 28 | **Stove** | Environmental Prop / Light Source | One burner glows a faint orange regardless of player actions. Non-interactable. Provides the only passive light source in the kitchen that is not player-controlled. |
| 29 | **Sofa** | Environmental Prop / Cover Object | In the living room. The player can crouch (Ctrl) behind it to reduce the ghost's line-of-sight detection chance. Not a perfect hiding spot — the ghost can still detect the player via proximity if it wanders close enough. |
| 30 | **Bathtub** | Environmental Prop | In the bathroom. Filled with dark, murky water whose surface ripples at random intervals. Cannot be interacted with directly. Its primary role is atmospheric — reinforcing the bathroom as the most unsettling room in the house. |
| 31 | **Defaced Painting** | Environmental Prop | On the bedroom wall. A family portrait in an ornate frame, with each face scratched out with something sharp. Examining it triggers a subtle audio sting (a low, discordant violin note). |
| 32 | **Front Door** | Interactive Door / Win Condition | In the hallway entrance at the front of the house. Locked at game start. Using the completed *Master Key* on this door while standing in front of it triggers the win condition — the screen fades to white and the ending text overlay appears. |
| 33 | **Coded Padlock** *(Planned — Not Yet Implemented)* | Puzzle Device | A padlock on a secondary locked box in the study (a planned seventh location). Requires the numeric combination found in the hidden bookshelf compartment. This mechanic is designed but not present in the current prototype. |

---

## 3. Scale

**Playable Area:**
The total playable floor space of the house is approximately **200 m²**, distributed across two floors. The overall house bounding volume is roughly **15 m × 13 m × 6 m** (width × depth × total height including roof peak).

**Room Dimensions (approximate):**

| Room | Width | Depth | Ceiling Height |
|------|-------|-------|----------------|
| Bedroom | 6 m | 5 m | 2.8 m |
| Kitchen | 5 m | 4 m | 2.8 m |
| Bathroom | 3 m | 2.5 m | 2.8 m |
| Hallway | 2 m | 12 m | 3.0 m |
| Living Room | 8 m | 7 m | 3.2 m |
| Upper Landing | 3 m | 2 m | 2.8 m |
| Master Bedroom | 7 m | 6 m | 2.8–3.8 m (sloped) |

**Character & Object Scale:**

- **Player character:** ~1.75 m tall (CharacterController capsule height), shoulder width ~0.5 m. This matches standard first-person game conventions and creates the correct sense of scale against door frames and furniture.
- **Ghost enemy:** ~1.9 m tall (NavMeshAgent height 2 m, radius 0.5 m) — slightly larger than the player to convey physical threat.
- **Doors:** 2.1 m tall × 0.9 m wide, standard residential proportions. The HouseDoor prefab opens at -180° on the Y-axis.
- **Furniture:** All assets from the nappin HouseInteriorPack are designed at real-world scale (1 Unity unit = 1 metre). Beds are ~2 m × 1.5 m, wardrobes are ~2 m tall, the sofa is ~0.9 m high.
- **Flashlight beam:** 35° spot angle, 15 m range, intensity 2.0 in URP units.

**Scale Consistency:**
The game uses the Unity convention of **1 Unity unit = 1 real-world metre** throughout all assets, physics, the NavMesh bake, and Cinemachine camera calculations. This ensures that movement speeds, jump heights, and interaction distances all feel physically plausible, which is critical for immersive horror — environments that feel wrongly scaled immediately break tension.

---

## 4. Spatial Boundaries

**Exterior Limits:**
The house is fully enclosed by solid mesh colliders on all exterior wall surfaces. The player physically cannot walk through them. A secondary invisible trigger volume is placed approximately 3 m beyond the exterior perimeter; if the player somehow exits through a geometry gap, they are silently teleported back to the hallway spawn point (a failsafe, not the primary boundary mechanism).

**Window Constraints:**
All windows use non-interactive, sealed mesh colliders. They are visually present (providing exterior light reference and a view of the fog) but cannot be opened, broken, or climbed through. The bedroom window's examination text ("*The nails have been hammered in from the outside*") converts this technical limitation into a piece of narrative world-building.

**Interior Constraints:**
Room boundaries are formed by their wall colliders. All furniture larger than a chair (wardrobe, refrigerator, beds, the sofa) uses static mesh colliders, preventing the player from walking through them. This also serves a gameplay purpose: furniture forces predictable movement paths that both the player and the ghost must navigate around.

**Back Porch:**
When the back door is unlocked (using the *Back Door Key*), it opens onto a small porch bounded by a 2.5 m wooden fence with no gate. This gives the illusion of an accessible exterior world while still confining the player — reinforcing the horror that even the "outside" is just another trap.

**Vertical Constraints:**
The upper floor is accessible only via the staircase (a ramp collider at 35°). Ceilings are solid colliders. The player's maximum jump height (1.2 m, per the FirstPersonController settings) is insufficient to bypass vertical boundaries. There is no crouch-jump bypass due to the CharacterController implementation.

**Design Philosophy:**
The spatial confinement is deliberately narrative rather than invisible. Horror games that hide their boundaries with fog or invisible walls break immersion when the player encounters them. In *The Haunting*, every boundary is either a locked door (creating a goal: find the key), a nailed window (creating dread: the world does not want you to leave), or a solid wall (architecturally expected). The boundary system is part of the story.

---

## 5. Temporal Dimension

**Time Setting:**
*The Haunting* takes place during a single, unbroken night, permanently set at approximately **2:00 AM**. The game does not use a real-time day/night cycle. Instead, time is tracked through a subtle escalation system anchored to the grandfather clock, making the passage of time felt rather than displayed.

**Temporal Mechanics:**

**1. Grandfather Clock Escalation**
The grandfather clock in the living room chimes every 15 real-world minutes. Each chime sequence increases the ghost's movement speed by 5% and its detection radius by 0.5 m. After four chimes (60 minutes of real play), the ghost moves noticeably faster and can detect the player from a slightly greater distance. This creates a soft pressure curve — players who explore efficiently are rewarded with a more manageable ghost, while players who take too long face escalating difficulty.

**2. Flashlight Battery as a Time Sub-Clock**
The flashlight battery drains at approximately 1% every 3 seconds while active, giving a fully charged battery approximately 5 minutes of continuous use. Players must make constant micro-decisions: explore thoroughly (battery draining, becoming more vulnerable) or move quickly (more risk of missing items). The battery level is visible via a subtle HUD indicator.

**3. Ghost Aggression Escalation**
Independently of the clock system, every 5 real-world minutes, the ghost gains a small passive speed boost and slightly increases its wander radius. This means even a player who ignores the clock encounters gradual escalation. The game does not display a visible timer — players must infer the passing of time from the ghost's behavior.

**4. Fixed Night Atmosphere**
The exterior lighting never changes — the world remains in deep moonlit night (the directional light at 50° pitch, -30° yaw, intensity 2.0). Interior lighting is entirely player- and environment-controlled. The decision to use a fixed night rather than a mechanical day/night cycle was made deliberately: a slow-moving day/night cycle would dilute the tension, while a static deep-night atmosphere maintains consistent dread. The only "time" the player perceives is the clock, the draining battery, and the worsening ghost.

> ⚠️ **Missing Implementation Note:** The clock-based ghost escalation, flashlight battery drain system, and independent ghost aggression escalation are **not yet implemented** in the current prototype. The directional light is currently static with no time-based modification, and the GhostWander.cs script uses fixed wander parameters without escalation. These mechanics are fully designed and will require additional scripting in future iterations.

---

# PART 2: GAME MECHANICS

---

## 1. Mechanics Count — 20 Distinct Game Mechanics

The following 20 mechanics define the player's interactive relationship with the world of *The Haunting*:

1. **WASD Locomotion** — Standard first-person ground movement using the CharacterController component.
2. **Sprint** — Holding Shift temporarily doubles movement speed (4 m/s → 6 m/s) at the cost of producing more noise.
3. **Jump** — Pressing Space launches the player upward (1.2 m height) with simulated gravity (-15 m/s²).
4. **Crouch** — Pressing Ctrl reduces the player's capsule height, lowering the camera and reducing movement speed. Used to hide behind furniture.
5. **Flashlight Toggle** — Pressing F switches the flashlight on/off. This is both a navigation tool and a stealth mechanic (off = less detectable by ghost).
6. **Object Pickup** — Pressing E while looking at a collectible item adds it to the player's inventory and displays a brief HUD notification.
7. **Door Interaction** — Pressing E while within range of a door opens or closes it. Uses smooth Slerp rotation (Y-axis, -180°). Proximity detection at 20 m radius using tagged player detection.
8. **Item Use** — Pressing Q while an item is selected in the inventory uses it (consumables are consumed, keys are applied to nearby locked objects).
9. **Inventory Navigation** — Pressing Tab opens the inventory HUD, displaying collected items in a grid. Scrolling through items selects the active item.
10. **Lamp Toggle** — Pressing E while looking at a toggleable lamp switches it on/off. When on, emits warm point light that attracts the ghost.
11. **Fireplace Activation** — Using the Matchbox item while looking at the fireplace triggers a one-time ignition animation and permanently illuminates the living room.
12. **Furniture Examination** — Pressing E on narrative objects (photographs, paintings, the journal) opens a close-up view with descriptive text. Time does not pause during examination.
13. **Wardrobe Hiding** — Pressing E on the enterable wardrobe places the player inside in a static hidden state. A heartbeat audio cue escalates the longer they hide; exiting before the ghost passes is critical.
14. **Bookshelf Puzzle** — Pressing E on individual books in sequence triggers a puzzle interaction. Incorrect sequence plays a fail sound; correct sequence opens the hidden compartment.
15. **Mirror Interaction** — Pressing E on the fogged bathroom mirror wipes it clean, revealing written text and collecting a narrative clue.
16. **Shower Curtain Pull** — Pressing E on the shower curtain triggers a pull animation, potentially revealing the ghost (scripted scare).
17. **Light Switch Toggle** — Pressing E on the hallway light switch simultaneously activates/deactivates all hallway sconces.
18. **Ghost Proximity Detection (TV Warning)** — The television automatically activates when the ghost enters an 8 m radius — a passive mechanic that gives the player visual and audio feedback without requiring player input.
19. **Health System** — Player health decreases when in contact with the ghost (or via unimplemented environmental hazards). Consumables restore health. Death returns the player to the start of the current run.
20. **Key Combination / Crafting** — When both Master Key Fragments are in the player's inventory, they automatically combine into the *Master Key*, updating the HUD and enabling the front door win condition.

> ⚠️ **Missing Implementation Note:** Mechanics 6–14 and 16–20 are designed and planned but **not yet implemented** in the current prototype. Currently implemented: WASD locomotion (1), Sprint (2), Jump (3), Flashlight toggle (5, partially — no battery drain), and Door Interaction (7).

---

## 2. Movement Mechanics

**Walking (WASD):**
The player walks at 4 m/s in any direction relative to the camera facing. Movement uses the Unity Starter Assets FirstPersonController, which applies smooth acceleration/deceleration with a SpeedChangeRate of 10.0. Input is read from Unity's new Input System, supporting both keyboard and gamepad. The CharacterController handles collision against all static environment colliders.

**Running / Sprinting:**
Holding Shift activates sprint mode, increasing speed to 6 m/s. Sprint has no stamina system (by design — limiting sprint to stamina adds complexity that detracts from the moment-to-moment horror focus in this prototype phase). Sprinting produces louder footstep audio, increasing the chance of ghost detection. Sprint is available at all times except while crouching or hiding in the wardrobe.

**Jumping:**
Space triggers a vertical impulse of 1.2 m height. Gravity is set at -15 m/s² (slightly stronger than Unity's default -9.8 to feel more grounded and weight-appropriate for a horror game where the player should not feel acrobatic). The player cannot jump while in the air — no double jump. Jump has a 0.1-second cooldown and a 0.15-second fall timeout before the grounded state resets.

**Crouching:**
Pressing Ctrl reduces the CharacterController's height and shifts the Cinemachine camera target point downward, giving a natural visual crouch. Movement speed while crouching is reduced to 2 m/s (half walking speed). Crouching allows the player to hide behind furniture and move with less noise. Crouching cannot be combined with sprinting.

**Camera Look / Mouse Input:**
Mouse movement controls camera rotation. The mouse sensitivity is configurable (default: 1.0 multiplier on raw mouse delta). The look angle is clamped to -90° (looking straight down) to +90° (looking straight up) to prevent gimbal issues. The Cinemachine Virtual Camera receives input from the FirstPersonController's CameraAngleOverride property.

**Physics:**
The player is not a Rigidbody — all physics simulation is handled by the CharacterController and custom gravity code in FirstPersonController. This is the standard Unity FPS approach: more predictable, less prone to Rigidbody jitter, and easier to tune for indoor-scale environments. Terminal velocity is -53 m/s (effectively unreachable in a two-story house, present as a failsafe).

**Terrain Interaction:**
All movement occurs on flat indoor surfaces. The CharacterController's grounded sphere cast (radius 0.5 m, -0.14 m offset from capsule base) handles floor detection. Stairs are navigated via a ramp collider (35° incline). The NavMesh baked for the ghost's pathfinding uses the same geometry, ensuring the ghost and player navigate identically.

---

## 3. Interaction Mechanics

**Proximity-Based E-Key System:**
The primary interaction model is a proximity + direction check. When the player is within a defined range of an interactive object AND looking at it (a raycast from the camera hits the object's collider), an on-screen prompt appears: *"[E] Open"*, *"[E] Pick Up"*, *"[E] Examine"*, etc. Pressing E while the prompt is visible triggers the interaction.

The door system (currently implemented) uses a range of 20 m for the prompt display (generous, to ensure players don't miss the hint) with interaction triggering on E press regardless of exact facing angle, as long as the player is within range and tagged "Player".

**Picking Up Objects:**
Interactable items display a highlighted outline (via URP stencil buffer outline effect) when the player looks at them. Pressing E while the item is highlighted adds it to the inventory array, plays a pickup audio cue, briefly displays an inventory notification HUD element, and destroys (or deactivates) the GameObject in the world. Keys display their specific icon in the notification.

**Using Items:**
Items in inventory are used contextually: selecting a key in the inventory and pressing E near a compatible lock triggers the unlock animation. Consumables (first aid kit, canned goods) are used instantly by pressing Q while selected, playing a use animation and updating the health HUD. The matchbox requires the player to be facing the fireplace and pressing E while the matchbox is selected.

**Combining Items:**
Key fragment combination is automatic — when both fragments are collected, the inventory triggers a combination animation and replaces the two fragments with the *Master Key* item. No manual combination step is required, reducing UI friction at the climax of the game.

**Dialogue / Text Overlay:**
Narrative items (journal, photographs, hidden note) open a full-screen semi-transparent overlay displaying the text content. Pressing E or Escape closes it. Unlike many horror games, time does not pause during text examination — the ghost continues to patrol, creating tension during narrative delivery.

**NPC Interaction:**
Currently, there is no dialogue or trading system with any NPC. The ghost is entirely hostile and does not communicate. Future design consideration: adding a narrative apparition (a neutral ghost) that provides hints through whispered dialogue on proximity.

> ⚠️ **Missing Implementation Note:** All interaction mechanics beyond door open/close are **not yet implemented** in the current prototype. The item pickup system, inventory, item use, text overlays, and combination mechanics are designed but require scripting.

---

## 4. Navigation Across Terrains

**Indoor Flat Navigation:**
The majority of the game takes place on flat indoor tile and hardwood flooring. Navigation is unrestricted except by furniture and wall colliders. The player can weave between furniture pieces, creating organic evasion routes when the ghost pursues.

**Staircase Navigation:**
The staircase uses a ramp collider at 35°. The CharacterController naturally slides up ramps below its maximum slope limit (default 45°). Ascending is slightly slower than flat movement due to the vertical component; the game does not explicitly reduce speed on stairs (the CharacterController handles this implicitly through the slope geometry).

**Hallway Chokepoints:**
The 2 m wide hallway is the most challenging navigation environment. At normal walking speed, the ghost and player can pass each other with approximately 0.5 m clearance — technically possible but psychologically terrifying. Players learn to avoid the hallway when the ghost's position is unknown.

**Kitchen Counter Navigation:**
The counter island forces players into an L-shaped path around it. This creates a natural evasion loop — players being chased can run circles around the island to maintain distance from the ghost. The NavMesh agent of the ghost also navigates around the island, making this a cat-and-mouse dynamic.

**Item-Enabled Navigation Shortcuts:**
- The *Back Door Key* unlocks an alternate exit from the kitchen to the porch, providing a navigation shortcut that bypasses the dangerous hallway.
- The *Bedroom Key* unlocks direct bedroom access from the hallway — before finding it, the player must use the main front hallway approach.
- Toggling lights creates "safe zones" (lit areas where the player can see) versus "ghost lure zones" (lit areas that attract the ghost), turning the lighting system into a navigation planning tool.

**Vertical Navigation:**
Movement between floors is restricted to the single staircase. There is no elevator, ladder, or alternate vertical route. This is a deliberate design choice: the staircase is a chokepoint, and ascending to the master bedroom (where the ghost primarily patrols) is always a committed decision.

---

## 5. Weapon Usage

*The Haunting* deliberately avoids giving the player any weapon in the traditional sense — the ghost cannot be damaged or killed. The design philosophy is that powerlessness is the core horror mechanic. However, the flashlight functions as a **defensive / utility tool** with quasi-weapon-like mechanics:

**The Flashlight (Defensive Tool):**
- **Primary Function:** Illumination — allows the player to see in dark areas.
- **Secondary Function:** Ghost Deterrence (Planned) — in the full design, shining the flashlight directly at the ghost for 2+ continuous seconds causes it to recoil, briefly halting its movement. This "stuns" it without dealing damage, giving the player a short window to escape. *This mechanic is not yet implemented in the prototype.*
- **Battery Management:** Acts as ammunition — the flashlight can only be used until the battery depletes. This creates weapon-like resource tension.
- **Stealth Mode:** Turning the flashlight off reduces ghost detection range to 3 m (the player becomes nearly invisible in the dark). This is the equivalent of a stealth mechanic in a conventional combat game.

**Environmental "Weapons" (Ghost Manipulation):**
While the player has no weapon that affects the ghost directly, several environmental objects can be used to manipulate ghost behavior:
- **Toggleable Lamps:** Act as remote decoys — turning on a lamp in a room the player is not in draws the ghost there.
- **Hallway Light Switch:** Illuminating the hallway makes the ghost more likely to patrol there, which can clear other areas.
- **Fireplace:** Once lit, the living room's warm glow attracts the ghost periodically — timing navigation around the ghost's attraction to the fireplace is a mid-to-late game skill.

**Design Note:**
The absence of weapons that damage the ghost is a conscious genre decision, aligned with games like *Amnesia: The Dark Descent* and *Outlast*. The player's agency lies in information, stealth, and environmental manipulation — not combat. This design creates a fundamentally different relationship between player and threat, one centered on avoidance and fear rather than engagement and victory.

---

## 6. Consumables and Resource Management

**Health System:**
The player has a health bar displayed as a subtle vignette effect on the screen edges (dark red tint intensifies as health decreases), with a small numeric indicator in the corner. Maximum health is 100. Contact with the ghost deals 25 damage per second. At 0 health, the game displays a death screen and resets the current run (no checkpoints in the prototype; a checkpoint system at each key collect is planned).

**Consumable Items:**

| Item | Health Restored | Location | Notes |
|------|----------------|----------|-------|
| First Aid Kit | +50 HP | Bathroom medicine cabinet | Single-use. Requires dangerous detour. |
| Canned Goods | +25 HP | Kitchen refrigerator | Single-use. More accessible but weaker. |

**Flashlight Battery:**
The flashlight battery is treated as a resource bar (0–100%). It drains at 1% per 3 seconds while active (~5 minutes total use). The single *Flashlight Battery* consumable in the kitchen drawer fully refills it. Players must decide whether to use the battery while it is still partially charged (to ensure they have maximum capacity for a dangerous run to the master bedroom) or save it for when the flashlight is critically low.

**Strategic Importance of Resource Management:**
The game contains exactly one of each key consumable item (first aid kit, battery, canned goods). This scarcity is intentional — players must decide when to use their resources rather than hoarding them. Using the first aid kit early on a minor ghost graze wastes its value for the more dangerous master bedroom run. Similarly, using the battery when the flashlight is at 60% capacity is a judgment call: is the upcoming section dangerous enough to warrant a full tank now?

**No Respawning Resources:**
Once a consumable is picked up, it is permanently removed from the world. There are no respawn timers, no regenerating supplies, and no shop or trading system. Every resource the player has was found and kept through active exploration. This creates genuine investment in each item — losing health is costly because the healing items are limited and static.

> ⚠️ **Missing Implementation Note:** The health system, consumable mechanics, flashlight battery drain, and inventory system are **not yet implemented** in the current prototype. The flashlight is currently a static toggle (on/off only, no battery simulation). Health and damage mechanics require scripting in future iterations.

---

# PART 3: PROTOTYPE

---

## Prototype Overview

The current prototype establishes the foundational layer of *The Haunting*: a navigable first-person interior environment with a wandering AI ghost, an interactive door system, and URP-based rendering. The prototype runs in Unity 6 (version 6000.3.24f1) on the Universal Render Pipeline. The following sections document exactly what has been implemented, what is missing, and what needs to be added to meet the assignment's minimum requirements.

---

## 1. Player Actions (Minimum 5 Required)

### ✅ Action 1 — Door Open/Close (IMPLEMENTED)

**Trigger:** Player presses E while within 20 m of the *HouseDoor* prefab (tagged "Player" detection).

**Implementation:** `DoorInteraction.cs` script attached to the door GameObject. The script uses a door pivot child object rotated via `Quaternion.Slerp` at 3 units/second. The door toggles between closed (0°) and open (-180°) states. The door frame's collider is disabled when open (allowing passage) and re-enabled when closed.

**World State Change:** The door physically changes from blocking passage to open passage. A GUI label (`OnGUI`) displays "[E] Open" / "[E] Close" as contextual feedback. This is a clear cause-and-effect interaction that permanently changes traversal options.

---

### ❌ Action 2 — Item Pickup (NOT YET IMPLEMENTED)

**Design:** When the player looks at a collectible item within 2 m and presses E, the item is added to the inventory array, a HUD pickup notification appears, and the item's GameObject is deactivated in the scene.

**Required:** An `ItemPickup.cs` script with raycast-based detection, an inventory system (a `List<ItemData>` in a PlayerInventory component), HUD feedback (brief on-screen item name popup), and audio (a pickup SFX).

**World State Change:** The item disappears from the world and appears in the player's inventory. If the item is a key, a door HUD indicator updates to show it as collected.

---

### ❌ Action 3 — Light Switch Toggle (NOT YET IMPLEMENTED)

**Design:** When the player presses E on the hallway light switch, all hallway wall sconce Point Lights are simultaneously enabled/disabled. The switch's visual state (lever position) updates to match.

**Required:** A `LightSwitch.cs` script that holds references to an array of `Light` components and toggles their `enabled` state. The switch object needs a mesh with two rotation states.

**World State Change:** The hallway environment changes from near-dark to fully lit (or vice versa). This directly affects ghost patrol behavior (ghost is attracted to lit areas) and player visibility.

---

### ❌ Action 4 — Fireplace Ignition (NOT YET IMPLEMENTED)

**Design:** When the player has the Matchbox in inventory, selects it, and presses E while looking at the fireplace, the fireplace ignites. A particle system (fire/ember effect) activates, a Point Light with flickering intensity is enabled, crackling audio begins, and the wax seal on the nearby box begins a shader-animated melt.

**Required:** A `FireplaceInteraction.cs` script, a particle system on the fireplace, an animated Point Light component, a wax melt shader (or material lerp), and the inventory prerequisite check.

**World State Change:** The living room is permanently illuminated with warm dynamic light. The wax seal box becomes openable. The matchbox is removed from inventory.

---

### ❌ Action 5 — Wardrobe Hide / Unhide (NOT YET IMPLEMENTED)

**Design:** When the player presses E on the enterable wardrobe in the master bedroom, the player enters a static hidden state: movement is disabled, camera position moves to a fixed "inside wardrobe" position, and a heartbeat audio loop begins. The ghost's detection radius ignores the player's position while inside. Pressing E again exits the wardrobe.

**Required:** A `WardrobeHide.cs` script that disables the FirstPersonController's movement input, overrides the Cinemachine camera with a fixed "inside wardrobe" Virtual Camera (lower priority switches to it), and modifies the ghost's detection logic to exclude the player's position when in the hiding state.

**World State Change:** The player's visibility state changes from "detectable" to "hidden." The ghost's behavior does not target the player's position while hidden, effectively resetting the threat dynamic.

---

### Summary of Player Actions

| # | Action | Status | Script Required |
|---|--------|--------|-----------------|
| 1 | Door Open/Close | ✅ Implemented | `DoorInteraction.cs` |
| 2 | Item Pickup | ❌ Not Implemented | `ItemPickup.cs` |
| 3 | Light Switch Toggle | ❌ Not Implemented | `LightSwitch.cs` |
| 4 | Fireplace Ignition | ❌ Not Implemented | `FireplaceInteraction.cs` |
| 5 | Wardrobe Hide | ❌ Not Implemented | `WardrobeHide.cs` |

---

## 2. Camera Movements (Minimum 3 Required)

### ✅ Camera 1 — First-Person View (IMPLEMENTED)

**Implementation:** A Cinemachine Virtual Camera is configured to follow the player's `CameraRoot` transform (a child of the PlayerCapsule prefab positioned at eye height). The camera's rotation is driven by the FirstPersonController's mouse input, which directly sets `CinemachineCamera.transform.rotation`. FOV is 60°, clip planes are 0.3 m (near) to 1000 m (far). The main Unity camera has `Clear Flags: Depth Only` to render on top of the Cinemachine output.

**Effect:** Full first-person perspective. The player sees exactly what the character would see from eye level, with smooth mouse-driven look rotation. This is the primary and default camera for 100% of standard gameplay.

---

### ❌ Camera 2 — Third-Person / Over-Shoulder View (NOT YET IMPLEMENTED)

**Design:** A secondary Cinemachine Virtual Camera configured as a Cinemachine Third Person Follow camera, positioned 2 m behind and 1 m above the player. This camera would activate during specific moments — for example, when the player enters the wardrobe hiding state, providing an external view of the wardrobe door with the ghost approaching.

**Required:** A second Cinemachine Virtual Camera with Cinemachine Third Person Follow body and Cinemachine POV aim, set to a lower priority than the FPS camera. Activation via `CinemachineVirtualCamera.Priority` manipulation in the WardrobeHide script (raise priority to switch to this camera). Cinemachine's camera blend system handles the smooth transition.

**Effect:** Demonstrates the Cinemachine priority blending system. Provides dramatic framing for the wardrobe hiding mechanic. Third-person perspective gives spatial context that is lost in first-person.

---

### ❌ Camera 3 — Cinematic Event Camera (NOT YET IMPLEMENTED)

**Design:** A Cinemachine Virtual Camera that activates during the game's win-state sequence when the player uses the Master Key on the front door. This camera pulls back and pans upward in a cinematic sweep (dolly track or animation path) before the fade-to-white ending overlay appears.

**Required:** A Cinemachine Dolly Track with a `CinemachinePath` defining the cinematic arc. A `CinemachineTrackedDolly` body on the Virtual Camera. A C# trigger in the `FrontDoorWin.cs` script that raises the cinematic camera's priority when the win condition is met. The blend from FPS to cinematic uses Cinemachine's `CinemachineBrain` blend settings (e.g., `EaseInOut`, 1.5 s blend time).

**Effect:** Provides a satisfying narrative punctuation to the game's climax. Demonstrates Cinemachine's blending and path-following capabilities. Transitions from first-person to cinematic framing conveys the player character's relief at escaping.

---

### Summary of Camera Movements

| # | Camera Behavior | Status | Implementation |
|---|-----------------|--------|----------------|
| 1 | First-Person View | ✅ Implemented | Cinemachine Virtual Camera + FirstPersonController |
| 2 | Third-Person / Wardrobe Camera | ❌ Not Implemented | Cinemachine Third Person Follow + Priority Blend |
| 3 | Win-State Cinematic Camera | ❌ Not Implemented | Cinemachine Dolly Track + Path Animation |

---

## 3. Lighting (Minimum 3 Required)

### ✅ Lighting 1 — Static Ambient / Directional Night Light (IMPLEMENTED)

**Implementation:** A single Directional Light at rotation (50°, -30°, 0°), intensity 2.0, hard shadows, mixed baking mode. The ambient lighting is configured in the URP Lighting Settings with an ambient sky color of RGB(0.212, 0.227, 0.259) — a cool, desaturated blue-grey that suggests moonlit night. This light illuminates the entire scene from a consistent angle, casting long directional shadows from furniture.

**Effect:** Establishes the base nighttime atmosphere. All rooms that are not further illuminated by player-controlled lights receive only this cool, low-intensity ambient. The directional angle creates dramatic furniture shadows that reinforce the horror atmosphere.

---

### ❌ Lighting 2 — Player-Controlled Flashlight (NOT YET IMPLEMENTED)

**Design:** A Unity `SpotLight` component attached to the player's camera (as a child of the `CameraRoot` transform), configured with a 35° spot angle, 15 m range, and intensity 3.0. The `FlashlightController.cs` script handles F-key toggle, battery drain (reducing intensity from 3.0 to 0 over 5 minutes of active use), and low-battery flickering (randomized intensity oscillation when below 20% battery).

**Required:** A `SpotLight` component child of the camera. A `FlashlightController.cs` script (F-key input, battery float field updated in Update, intensity mapped to battery percentage via AnimationCurve). The battery drain connects to the resource management system.

**Effect:** The primary interactive lighting mechanic. Turning the flashlight off is the core stealth decision. Battery management creates moment-to-moment tension. Flickering at low battery is one of the game's most effective tension cues.

---

### ❌ Lighting 3 — Event-Triggered Lighting Change (NOT YET IMPLEMENTED)

**Design:** Two planned event-triggered lighting changes:

**3a — Fireplace Light Activation:** When the player ignites the fireplace, a `PointLight` (warm orange, intensity 3.0, range 8 m) on the fireplace is enabled with a flickering intensity animation. This adds a second dynamic light source to the scene and fundamentally changes the living room's visual character from the darkest room to the warmest.

**3b — Hallway Sconce Activation:** When the player activates the hallway light switch, all six wall sconce `PointLight` components in the hallway are simultaneously enabled/disabled. Each sconce light has a range of 3 m and warm white color (RGB 1.0, 0.95, 0.8), creating a trail of warm pools of light down the hallway.

**Required:** Point Light GameObjects parented to the fireplace and each wall sconce mesh. A `LightSwitch.cs` script referencing all sconce lights. A `FireplaceInteraction.cs` script enabling the fireplace Point Light and its `FlickerLight.cs` component (randomizes intensity between 2.5–3.5 at 0.05 s intervals).

**Effect:** Demonstrates environment-triggered lighting changes. The hallway switch creates a clear cause-and-effect visual change across the most traversed area of the map. The fireplace ignition is the most dramatic single lighting event in the game.

---

### Summary of Lighting Behaviors

| # | Lighting Behavior | Status | Implementation |
|---|-------------------|--------|----------------|
| 1 | Static Directional Night Light | ✅ Implemented | Directional Light, URP Ambient Settings |
| 2 | Player-Controlled Flashlight | ❌ Not Implemented | SpotLight + FlashlightController.cs |
| 3a | Fireplace Dynamic Light | ❌ Not Implemented | PointLight + FireplaceInteraction.cs + FlickerLight.cs |
| 3b | Hallway Light Switch Array | ❌ Not Implemented | PointLight array + LightSwitch.cs |

---

## Prototype — Overall Missing Requirements Summary

The following table summarizes all minimum requirements from the assignment specification against the current prototype state:

| Requirement | Minimum | Implemented | Missing |
|-------------|---------|-------------|---------|
| Player Actions | 5 | 1 (Door Interaction) | 4 (Pickup, Switch, Fireplace, Wardrobe) |
| Camera Movements | 3 | 1 (First-Person) | 2 (Third-Person, Cinematic) |
| Lighting Behaviors | 3 | 1 (Directional Static) | 2 (Flashlight, Event-Triggered) |

### Priority Implementation Order (Recommended):

1. **Flashlight (SpotLight + toggle script)** — Highest impact per effort. Simple to implement, immediately transforms the game's atmosphere.
2. **Hallway Light Switch** — Simple PointLight array toggle. Meets the event-triggered lighting requirement.
3. **Item Pickup + Inventory** — Foundation for all other gameplay mechanics.
4. **Cinemachine Third-Person Camera** — Add a second Virtual Camera and test priority blending.
5. **Fireplace Ignition** — Requires inventory prerequisite check, particle system, and PointLight.
6. **Wardrobe Hide** — Requires movement disable and camera switch.
7. **Cinematic Win Camera** — Requires Dolly Track setup; implement last once win condition exists.

---

*End of Assignment 2: Game World and Game Mechanics*
*Game: The Haunting | Engine: Unity 6 URP | Student: NourelNagar*
