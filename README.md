Unity Editor Version : 6000.2.6f2

Playgama Starter Example

This project implements functionality and visuals according to the **Unity Example Project Challenge** requirements.  

The project is built using **Unity version 6000.2.6f2**.

👉 [Playgama Bridge Documentation](https://wiki.playgama.com/playgama/sdk/engines/unity)

Based on the original repository:  
👉 [playgama/bridge-unity-examples](https://github.com/playgama/bridge-unity-examples)

### Implemented Features

1. Integrated **Playgama Bridge** functionality.

2. Added a wrapper class **`PlaygamaManager`** for the Bridge.

3. Updated **Playgama Bridge plugin** to version **1.25**.

4. Implemented menus and UI according to the [Figma design](https://www.figma.com/design/X3B1Cp6b9eRZHbhKNKo6Ih/Dev-Sandbox-Playgama).


### Running the Project

To run the project in the Unity Editor:

- Set the **target platform** to **WebGL**.

- Some features may be unavailable in the Editor if the Bridge reports lack of platform support.


For example, if the `Player` menu displays:

```
IsAuthSupported = False
```

then the **Authorize** button will be disabled.
