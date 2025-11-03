# 🌱🤖 Garden Analyzer & Botanist (GAB)

A smart agricultural robot designed to monitor plant health and assist in garden automation.  
GAB utilizes sensors, a robotic arm, and wireless control to gather environmental data and interact with plants.

---

### 👨‍💻 Developed By
- Jeff Bayhon  
- Lance Dador  
- Giuliano de Guzman  
- Gabriel Hipolito  

---

## 🧠 Languages Used
- **Arduino** (Nano microcontroller firmware)
- **C#** (Windows .NET Application)

---

## ✨ Features
- 2-Wheel Drive movement system  
- 4-DOF robotic servo arm  
- Bluetooth communication (Serial)  
- Live sensor readings:
  - Battery voltage  
  - Soil moisture  
  - Air humidity  
  - Air temperature  
- GUI desktop controller
- Keyboard controls for easy operation

---

## 🛠 Requirements

### Software
- **Visual Studio 2019 / 2022**
- **.NET 9*
- **Arduino IDE**

### Hardware
- Arduino Nano
- HC-05 / HC-06 Bluetooth Module
- 2WD Motor Driver (L298N or similar)
- 4x Servo motors
- Soil moisture sensor
- Voltage sensor
- DHT11 / DHT22 sensor (Humidity + Temp)

---

## ▶️ How to Run

1. Pair your PC with the robot via **Bluetooth**
2. Check the COM port assigned to the Bluetooth module (Device Manager)
3. Open the desktop app
4. Select the COM port and click connect
5. Control the robot using the on-screen buttons or keyboard shortcuts below:

### 🎮 Keyboard Controls

| Function | Keys |
|--------|------|
Forward / Back / Left / Right | `W` `S` `A` `D`  
Decrease / Increase Motor Speed | `Z` / `C`  
Battery Reading | `V`  
Soil Moisture Reading | `B`  
Humidity Reading | `N`  
Temperature Reading | `M`  

### 🦾 Servo Arm Controls

| Servo | Keys |
|-------|-----|
Servo A (X - AXIS) | `T` / `G`  
Servo B (Y - AXIS) | `Y` / `H`  
Servo R (Z - ROTATION) | `U` / `I`  

---

## 📥 Clone the Repository

### Using Visual Studio
1. Open **Visual Studio**
2. Click **Clone a repository**
3. Paste the repo link

```bash
https://github.com/YOUR_USERNAME/rc_controller.git
