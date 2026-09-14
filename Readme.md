# Master's Research Projects in Mixed Reality

This repository summarizes two research projects conducted independently by me during my Master's studies at TU Dresden, focusing on spatial alignment, user interaction and interface authoring in Mixed Reality.

The full academic reports are provided separately in this repository.

## Project 1 — Spatial Alignment and Labeling for Immersive Content Organization

### Overview

This project focused on investigating the alignment accuracy and stability of spatial anchors in large-scale Mixed Reality environments. Based on the experimental findings, I explored approaches to improve alignment stability without additional external tracking hardware, as well as spatial labeling techniques for immersive content organization.

### Spatial Anchor Drift Experiment

I designed a series of controlled experiments to investigate the positional and rotational drift of spatial anchors under different user movement conditions. Experimental variables were selected based on the underlying spatial anchor mechanism, particularly its dependence on reconstructed point cloud information from the surrounding environment.

The experiments examined how factors such as movement range, walking route, and environmental transitions affected alignment stability. The results were then used to identify conditions associated with larger spatial drift and to guide the subsequent stabilization design.

![Experimental Setting](Readme_Assets\reference.jpg)

![Experimental Results](Readme_Assets/drift.jpg)

### Alignment Improvements

Based on the experimental observations, I investigated two approaches for improving alignment stability without additional external tracking hardware:

**Dense Anchor Placement** - additional spatial anchors are distributed throughout the environment so that the system can rely on nearby reference anchors as the user moves.

**Periodic Anchor Reloading** - previously stored anchors are periodically reloaded to re-establish spatial references and reduce accumulated alignment error.

![Improvements Design](Readme_Assets\Dense Anchor.jpg)

I evaluated both approaches experimentally and integrated anchor storage/loading into the prototype.

![Improved Result with Dense Anchors](Readme_Assets\with dense anchor.jpg)

![Improved Result with Shared Anchors](Readme_Assets\with shared anchor.jpg)

### AOI / SOI Spatial Labeling

This project developed 3D Areas of Interest (AOI) and 2D Surfaces of Interest (SOI) for spatial content organization.

Areas of Interest (AOIs) provide volumetric regions for grouping and organizing immersive content in 3D space, while Surfaces of Interest (SOIs) provide 2D spatial regions associated with physical or virtual surfaces.

Both were integrated with spatial anchors to support persistent placement and retrieval of spatially organized content.

![AOI and SOI Prefabs](Readme_Assets\Aoi and Soi.jpg)

---

## Project 2 — In-Situ Authoring of Opportunistic Interfaces with Mobile Mixed Reality HMDs

### Overview

This project explored how users can author spatial interfaces in situ, directly within their physical environment using a standalone Mixed Reality headset and hand tracking, without relying on external authoring devices.

The prototype enables users to create, place, configure, and interact with spatial widgets through bare hand interaction and gestures, while using existing real-world objects and surfaces to provide passive haptic feedback.

### In-Situ Widget Authoring

I designed and implemented six representative spatial widgets to explore different forms of opportunistic interaction, including buttons, keyboards, drawing canvases, straight sliders, customised sliders, and media interfaces.

![Widgets Authoring](Readme_Assets\Slider-Vis1.jpg)

### Gesture Interaction

I implemented mid-air gesture interactions to support widget creation, placement, manipulation, and interaction directly within the MR environment. This allowed the authoring workflow to remain headset-based without requiring external controllers or desktop authoring tools. 

![Gesture Interaction](Readme_Assets\GestureGenerate.jpg)

### Reality-Based / Passive Haptic Interaction

A key challenge identified through the spatial-alignment experiments in my previous project was that small alignment errors can persist in large-scale Mixed Reality environments, and even minor errors can substantially affect passive haptic interaction. For example, when a virtual button is intended to be placed directly on a physical wall, a positional error of only a few centimeters may cause the button to appear embedded in the wall or floating in front of it. In either case, the visual interaction point no longer corresponds to the physical contact point.

To address this problem, I designed a touch-calibrated placement approach. Instead of positioning the virtual widget solely according to the reconstructed virtual representation of the environment, the system encourages users to “push” the widget toward the intended real world surface during authoring. When the user's fingertip physically contacts the surface, the fingertip position is used to determine the final placement of the virtual widget.

In this way, the authored interaction point is grounded in the user's actual physical contact with the environment. Even when small global alignment errors remain between the virtual and physical coordinate systems, the local interaction point can remain consistent with the physical surface, enabling more reliable passive haptic feedback.

![OffsetCancelOut](Readme_Assets\OffsetCancelOut.png)

---

## Technical Stack & Research Methods

- Unity / C#
- Mixed Reality prototyping
- Meta/Oculus HMDs
- Spatial anchors and persistent spatial content
- Hand tracking and mid-air gesture interaction
- Spatial UI and in-situ authoring
- Controlled experimental design
- Quantitative data collection and analysis

## Academic Reports

The full academic reports submitted during my Master's studies at TU Dresden are available in this repository:

- **Spatial Alignment and Labeling for Immersive Content Organization**
- **Immersive Authoring of Opportunistic Interfaces in Mixed Reality**

## Source Code

Selected Unity contents and C# scripts implemented during these projects are included in this repository. The complete original Unity project is not distributed because it contains laboratory and third-party assets.