<p align="center">
  <img src="https://github.com/JinkiJung/Tasc-Unity/blob/master/Assets/Resources/Tasc/Texture/Tasc-logo-small.png?raw=true">
</p>

# Tasc-Unity: Unity implementation of Tasc
Tasc is procedure modeling that describes and implements a procedure. Tasc aims to support easier implementation of a scenario and a procedural task such as tutorial, virtual reality based training, e-Learning, game, presentation, and simultation, through the unified structure of the task script. This repository provides an Unity package of Tasc, that consumes the Tasc script.

## Download
[Tasc unitypackage v0.1.1](https://github.com/JinkiJung/Tasc-Unity/raw/master/Tasc0.1.1.unitypackage)

## Feature
- Easy, reusable (modular) implementation of a scenario
- Support of a sequential and conditional scenario flow
- Logging for user performance evaluation
- multi-platform support, currently a desktop and Steam VR
- Easy implementation of visual and audio guidance
- Windows and OSX support
- And it’s free to use!

## A brief history

When [Jinki Jung](https://jinkijung.github.io/) and [Hyeopwoo Lee](https://github.com/orgs/VirtualityForSafety/people/opo6954) conducted researches on virtual training, they found out there are no generalized framework in procedure implementation even though it has huge impacts to various industry. The initial idea was to develop an authoring tool for converting the training scenario (mostly formed as a manuscript or dialogs) to an executable training program. Throughout their publications and researches they formerly invented PAUT (pairwise authoring tool) and ACTA (Actor-Condition-Terminus-Action). Jinki who is the inventor of Tasc, continues his path to unify those efforts and implement Tasc from scratch again to make it more generic and useful. 

## Development environment
* Unity 2018 4.19f1
* Windows 10 
* Mac OSX 10.15.3

## Dependency
- [Unity Windows Text-To-Speech](https://github.com/VirtualityForSafety/UnityWindowsTTS)
- SteamVR Plugin 2.5.0 (sdk 1.8.19)
- Unity Standard Assets 1.1.5

## How to use it
1. Create a new Unity project
2. Download and import [Unity Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351) from AssetStore
3. Download and import [SteamVR Plugin](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647)
4. Download and import the Tasc unitypackage
5. Implement a Tasc scenario.
6. Make a try!

## Reference implementation
- [CraneManipulation3D](https://github.com/VirtualityForSafety/CraneManipulation3D)
- [StretchingExercise3D](https://github.com/VirtualityForSafety/StretchingExercise3D)
- [MazeEscape3D](https://github.com/VirtualityForSafety/MazeEscape3D)

## Related works
- [Annotation vs. Virtual Tutor: Comparative Analysis on the Effectiveness of Visual Instructions in Immersive Virtual Reality](https://www.researchgate.net/publication/336592427_Annotation_vs_Virtual_Tutor_Comparative_Analysis_on_the_Effectiveness_of_Visual_Instructions_in_Immersive_Virtual_Reality), presented at [IEEE ISMAR 2019](https://www.ismar19.org/)

## Maintainer
- [Jinki Jung](https://jinkijung.github.io/)

## License: MIT license
 - You may use this product for personal and/or commercial use.
 - You may make modifications to the source code
 - You may distribute the source and/or compiled code
 - The work is provided "as is". You may not hold me the author liable
 - You must include copyright information in all copies and uses of this work
 - You must include the license notice in all copies or uses of this work
 
