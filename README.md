# DesktopPet (CatPet)

[English](#english) | [中文](#chinese)

---

<a name="english"></a>
## 🇬🇧 English

### Introduction
DesktopPet is a Unity-based desktop companion application that brings a cute, interactive cat to your Windows desktop. The application features a transparent, click-through window using Windows API, allowing the pet to coexist with your other open windows without interference. It serves as both a fun toy and a helpful utility.

### Features
*   **Transparent Overlay**: Uses `User32.dll` to create a borderless, transparent window that sits on top of your desktop. The window is "click-through" unless you are interacting with the pet or UI.
*   **Interactive Pet**:
    *   **Drag & Drop**: Easily move the cat anywhere on your screen using the mouse.
    *   **Touch & Sound**: Click the cat to hear it meow and see it react.
    *   **Auto-Play**: The cat automatically chases balls generated in the scene (optimized with Object Pooling).
*   **Utilities**:
    *   **Water Reminder**: Built-in health assistant that reminds you to drink water at customizable intervals.
    *   **Digital Clock**: Displays real-time clock on the desktop.
*   **Configuration**:
    *   **Settings Panel**: Configure reminder intervals (minutes) and active hours (start/end time).
    *   **Audio Control**: Toggle sound effects on/off.
    *   **Persistence**: All settings are automatically saved.

### Requirements
*   **Operating System**: Windows 10 / Windows 11
*   **Development Environment**: Unity 2021.3 LTS or later

### Installation & Development
1.  Clone the repository:
    ```bash
    git clone https://github.com/GoldBean216/DesktopPet.git
    ```
2.  Open the project folder in Unity Hub.
3.  Open the `SampleScene` (or main scene) in the Editor.
4.  Press **Play** to test in the editor, or go to **File -> Build Settings** to build for Windows Platform.

### Controls
*   **Left Click (Hold & Drag)**: Move the cat around the screen.
*   **Left Click (Tap)**: Interact with the cat.
*   **UI Interaction**: Click buttons on the settings panel to configure the app.

---

<a name="chinese"></a>
## 🇨🇳 中文

### 简介
DesktopPet (桌面宠物) 是一款基于 Unity 开发的 Windows 桌面伴侣应用。它在你的桌面上生成一只可爱的互动猫咪。通过调用 Windows API，程序实现了无边框透明窗口效果，让猫咪仿佛生活在你的壁纸上，同时支持鼠标穿透功能，不会影响你的正常工作。

### 功能特性
*   **透明窗口技术**: 利用 `User32.dll` 实现背景透明和鼠标穿透。只有当你把鼠标移动到猫咪或 UI 上时，窗口才会拦截点击事件，否则你可以直接点击猫咪身后的图标。
*   **互动体验**:
    *   **拖拽移动**: 使用鼠标可以随意将猫咪拖动到屏幕的任何位置。
    *   **点击反馈**: 点击猫咪会播放可爱的叫声。
    *   **自动玩耍**: 系统会自动生成毛球，猫咪会自主追逐玩耍（使用了对象池技术优化性能）。
*   **实用工具**:
    *   **喝水提醒**: 内置健康助手，支持自定义提醒间隔，定时提醒你休息喝水。
    *   **数字时钟**: 实时显示当前时间。
*   **设置系统**:
    *   **参数配置**: 可自由设置提醒间隔（分钟）以及提醒生效的时间段（开始/结束小时）。
    *   **音频管理**: 一键开启或关闭音效。
    *   **自动保存**: 所有的设置更改都会自动保存到本地。

### 系统要求
*   **操作系统**: Windows 10 / Windows 11
*   **开发环境**: Unity 2021.3 LTS 或更高版本

### 安装与开发
1.  克隆仓库到本地:
    ```bash
    git clone https://github.com/GoldBean216/DesktopPet.git
    ```
2.  使用 Unity Hub 打开项目文件夹。
3.  打开 `SampleScene` (或主场景)。
4.  点击 **Play** 运行测试，或通过 **File -> Build Settings** 打包为 Windows 应用程序 (`.exe`)。

### 操作说明
*   **鼠标左键长按拖拽**: 移动猫咪的位置。
*   **鼠标左键点击**: 与猫咪互动。
*   **鼠标右键点击**: 呼出设置菜单/UI面板。
*   **UI 交互**: 点击设置面板上的按钮进行配置。
