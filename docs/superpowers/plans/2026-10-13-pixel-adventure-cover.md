# PIXEL ADVENTURE Cover Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Generate and save a polished 16:9 pixel-art start-screen cover that matches the visual language of the existing 2Dpixel levels.

**Architecture:** Use the supplied gameplay screenshot only as a style and subject reference, then generate a new cover composition with the built-in image-generation tool. Visually inspect the result, make at most one focused correction if needed, and place the approved raster asset in the Unity project without changing scene or UI code.

**Tech Stack:** Built-in image generation, Unity raster asset workflow, PNG.

---

### Task 1: Generate the cover artwork

**Files:**
- Reference: `C:/Users/13604/AppData/Local/Temp/codex-clipboard-9b5c5c9d-e914-4586-8115-af908b9e04b5.png`
- Create: `Assets/Art/UI/pixel-adventure-cover.png`

- [ ] **Step 1: Inspect the reference image**

Confirm the visual language: light-gray diamond background, bright green grass, warm brown soil blocks, white pixel character, apples, coins, spikes, and finish gate.

- [ ] **Step 2: Generate one landscape cover**

Use the approved specification from `docs/superpowers/specs/2026-10-13-pixel-adventure-cover-design.md`. Require a 16:9 landscape image, exact title `PIXEL ADVENTURE`, a jumping white character near center-left, staggered platforms, apples, coins, spikes, a finish gate, and clean lower-center space for Unity UI.

- [ ] **Step 3: Inspect the generated image**

Verify that the title is legible and correctly spelled; the image has no editor chrome, watermark, extra text, photorealism, or baked-in menu buttons; and all required level motifs are recognizable.

- [ ] **Step 4: Make one focused correction if required**

If a validation item fails, regenerate or edit only that issue while preserving the approved composition and style.

### Task 2: Save and verify the Unity asset

**Files:**
- Create: `Assets/Art/UI/pixel-adventure-cover.png`

- [ ] **Step 1: Copy the selected image into the project**

Create `Assets/Art/UI` if it does not exist and save the final PNG as `pixel-adventure-cover.png` without overwriting unrelated assets.

- [ ] **Step 2: Verify the saved file**

Open the saved PNG from its project path and confirm it is readable, landscape, visually identical to the selected output, and suitable for use as a Unity start-screen background.

- [ ] **Step 3: Report the result**

Provide a clickable path to `Assets/Art/UI/pixel-adventure-cover.png`, state that the built-in image generator was used, and include the final production prompt.
