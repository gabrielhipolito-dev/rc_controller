# 🚗 RC Controller App (C# + Arduino)

This project is a **WPF (Windows Presentation Foundation)** application written in **C# (.NET)** for controlling hardware components such as motors and sensors connected to an **Arduino** through **serial communication**.

It allows you to send commands using **buttons** or **keyboard keys** (`W`, `A`, `S`, `D`, etc.) and display live feedback from the Arduino inside the app.

---

## 🧠 Features

- 🎮 Control movement using buttons or keyboard (`W`, `A`, `S`, `D`, `Z`, `C`)
- 🔌 Communicates with Arduino via serial port (COM3, 9600 baud)
- 📊 Displays real-time Arduino data (e.g., voltage, humidity, temperature)
- 🪟 Simple WPF interface built with Visual Studio
- ⚙️ Works on .NET 6 or .NET 8

---

## 🧩 Requirements

Before you start, make sure you have:
- 🧰 **Visual Studio 2019 / 2022**
- 💾 **.NET 6 or .NET 8 SDK**
- 🔌 **Arduino IDE**
- 🧠 Basic knowledge of serial communication
- 🪫 Arduino connected to your PC via USB

---

## 🧭 How to Run the Project

### 🔹 Step 1: Clone the Repository
Open **Visual Studio** and do this:

1. Click **Clone a repository**
2. In **Repository location**, paste:
   ```bash
   https://github.com/YOUR_USERNAME/rc_controller.git
