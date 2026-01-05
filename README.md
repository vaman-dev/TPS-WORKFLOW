# 🎮 Third-Person Character Controller – Tech Demo

## 📌 Project Overview
This project is a **Third-Person Character Controller Tech Demo** built in **Unity**, focused on **clean architecture, modular design, and scalable gameplay systems** rather than content.

The goal of this project is to demonstrate:

- Proper separation of gameplay systems  
- Event-driven animation flow  
- Combat-ready architecture  
- Expandable movement and interaction mechanics  

This is a **single-player, non-multiplayer** prototype intended for **learning, experimentation, and portfolio use**.

---

## 🧠 Core Features

### 🎮 Player Movement
- Idle / Walk / Run (camera-relative)
- Jump
- Front Flip
- Crouch (toggle-based)

---

### ⚔️ Combat System
- Sword-based melee combat
- 3-hit combo attack system (`Attack1`, `Attack2`, `Attack3`)
- Layer-based animation blending for combat
- Input-buffered combo chaining

---

### 🎭 Animation System
- Animator-driven **Finite State Machine (FSM)**
- Trigger-based animation transitions
- Animation Events for gameplay timing (e.g., sword visibility)
- Separate combat animation layer

---

### 🦵 Procedural Interaction *(Planned / Optional)*
- Two-Bone IK via **Animation Rigging**
- Environment interaction via rig weight blending
- Smooth IK activation and deactivation

---

## 🧩 Architecture Highlights
- Event-driven input system (Unity Input System)
- Animation FSM decoupled from gameplay logic
- Modular combat controller
- Composition over inheritance
- Scalable design for future weapons and abilities

---

## 🏗️ Design Patterns Used
- Finite State Machine (FSM)
- Event-Driven Architecture
- Strategy Pattern
- Interface-Based Programming
- Composition Over Inheritance
- Separation of Concerns (SoC)
- Command Pattern *(implicit)*
- Observer Pattern *(implicit)*

---

## 📂 Project Structure (Key Scripts)

Assets/

└── Scripts/

└── Player/

├── Core/

│ ├── PlayerController.cs

│ ├── PlayerInputReader.cs

│ ├── AnimationFSM.cs

│ ├── SwordAttackController.cs

│
├── Animation/

│ └── AnimationEventReceiver.cs

│

└── IK/

└── (Procedural IK scripts)
