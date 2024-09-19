# DiceMaster's Grimoire

<img src="DiceMasters.Grimoire/Assets/logo_source.png" width="200">

## Introduction

DiceMaster's Grimoire is a digital solution designed to streamline the most frequent and time-consuming aspect of tabletop role-playing games: dice rolling and result calculation.

In a traditional tabletop RPG session, players and Dungeon Masters often find themselves bogged down by the mechanics of dice rolling:

1. Selecting the correct dice for each action (d4, d6, d8, d10, d12, d20)
2. Rolling multiple dice for a single action (e.g., 3d6 for damage)
3. Adding or subtracting modifiers to the roll (e.g., +5 for skill bonuses)
4. Calculating the final result
5. Repeating this process multiple times for different actions or characters

For complex characters or creatures with multiple abilities, this process is repeated numerous times during a single encounter. A simple combat round might involve:

- Rolling for initiative (d20 + modifier)
- Rolling to hit (d20 + attack bonus)
- Rolling for damage (varied dice + strength modifier)
- Rolling for special abilities (custom dice combinations)

Each of these steps takes time, breaks the flow of the game, and can lead to errors in calculation, especially when players are tired or distracted.

DiceMaster's Grimoire transforms this entire process into a streamlined, one-click operation. With this application, you can:

- Set up dice roll configurations for different actions and abilities
- Include all necessary modifiers in the configuration
- Perform complex dice rolls instantly with a single click
- Get accurate results without manual calculation

By digitizing the dice rolling and calculation process, DiceMaster's Grimoire significantly reduces the time spent on these mechanical aspects of the game. This allows players and Dungeon Masters to maintain focus on the narrative, make decisions quickly, and keep the game flowing smoothly.

Whether you're making a simple skill check or resolving a complex spell effect, DiceMaster's Grimoire provides the speed and accuracy you need. Say goodbye to fumbling with physical dice and manual calculations, and hello to more dynamic and immersive role-playing sessions.

## Features

### Area Management

DiceMaster's Grimoire allows you to create, edit, and organize your game world into distinct areas.

- **Adding Areas**: Click the "Add Area" button at the top of the window to create a new area.
- **Renaming Areas**: Double-click on an area's name to edit it. Press Enter, tab or click outside the text box to save changes.
- **Cancel Renaming**: Press ESC to cancel name editing.
- **Removing Areas**: Click the "X" button next to an area's name to delete it, confirming the action in the dialog box.

![Area Management](/Images/area-management.gif)

### Creature Management

Within each area, you can add and manage creatures, representing NPCs, monsters, or player characters.

- **Adding Creatures**: Click the "Add Creature" button within an area to create a new creature.
- **Naming Creatures**: Edit the creature's name in the text box provided.
- **Removing Creatures**: Click the "Remove" button next to a creature to delete it from the area.
- **Expanding/Collapsing Creature Details**: Click on the expander arrow next to a creature's name to show or hide its details.

![Creature Management](/Images/creature-management.gif)

### Dice Management

For each creature, you can set up and manage multiple dice configurations for various actions or abilities.

- **Adding Dice**: Click the "Add Dice" button within a creature's expanded view to add a new dice configuration.
- **Configuring Dice**: Set the quantity, number of sides, modifier, and description for each dice configuration.
- **Rolling Dice**: Click the "Roll" button next to a dice configuration to generate a random result.
- **Removing Dice**: Click the "Remove" button next to a dice configuration to delete it.

![Dice Management](/Images/dice-management.gif)

### Saving and Loading Game States

DiceMaster's Grimoire allows you to save your entire game state and load it later, ensuring continuity between sessions.

- **Saving**: Click the "Save" button at the top of the window. Choose a location and filename to save your game state.
- **Loading**: Click the "Load" button at the top of the window. Select a previously saved file to restore your game state.

![Save and Load](/Images/save-load.gif)

## Installation

1. Visit the [Releases](https://github.com/TosiRikhard/DiceMasters.Grimoire/releases/) page of the DiceMaster's Grimoire repository.
2. Download the latest version
3. Extract the downloaded archive to your preferred location.
4. Run the `DiceMasters.Grimoire` executable to start the application.

## Usage Guide

### Main Window

The main window of DiceMaster's Grimoire is divided into several key areas:

1. **Toolbar**: Located at the top, containing "Add Area", "Save", and "Load" buttons.
2. **Area Tabs**: Below the toolbar, displaying all created areas as tabs.
3. **Area Content**: The main part of the window, showing the selected area's creatures and their dice.

### Working with Areas

- To switch between areas, click on the respective tab.
- To rename an area, double-click its tab name, edit, and press Enter tab or click outside the control.
- To cancel name edit press Esc
- To delete an area, click the "X" on its tab.

### Working with Creatures

- Each creature is displayed as an expandable panel within its area.
- Expand a creature's panel to view and manage its dice configurations.
- Use the "Add Creature" button to create new creatures in the current area.

### Managing Dice

- Within each expanded creature panel, you'll find its dice configurations.
- Use the "Add Dice" button to create new dice setups for the creature.
- Configure each dice by setting its quantity, sides, modifier, and description.
- Click "Roll" to generate a random result based on the dice configuration.

### Saving and Loading

- Use the "Save" button to store your current game state to a file.
- Use the "Load" button to restore a previously saved game state.

## Tips and Tricks

- Use descriptive names for areas and creatures to easily identify them.
- Utilize the dice description field to note the purpose of each roll (e.g., "Attack Roll", "Damage", "Skill Check").
- For complex encounters, create separate areas for different locations or groups of creatures.
- Save your game state frequently to prevent data loss.

## Troubleshooting

- **Application doesn't start**: Ensure you have the latest version and that your system meets the minimum requirements.
- **Changes not saving**: Make sure you have write permissions to the folder where you try to save the state
- **Dice rolls seem repetitive**: The application uses a random number generator. True randomness may sometimes appear pattern-like.

## For Developers

### Project Structure

DiceMaster's Grimoire is built using the following technologies:

- Avalonia UI for cross-platform desktop application development (Releases contains only Windows builds, no other platforms are tested)
- .NET 8.0 as the framework
- MVVM (Model-View-ViewModel) architectural pattern
- Community Toolkit MVVM for MVVM implementation
- Protobuf-net for efficient serialization

### Setting Up the Development Environment

1. Install the .NET 8.0 SDK from the [official .NET website](https://dotnet.microsoft.com/download).
2. Clone the repository: `git clone https://github.com/yourusername/dicemasters-grimoire.git`
3. Open the solution in your preferred IDE (Visual Studio, VS Code, or JetBrains Rider).
4. Restore NuGet packages.
5. Build and run the application.

### Contributing

We welcome contributions to DiceMaster's Grimoire! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them with clear, concise commit messages.
4. Push your changes to your fork.
5. Create a pull request with a detailed description of your changes.

### License

DiceMaster's Grimoire is released under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Thank you for using DiceMaster's Grimoire! We hope it enhances your tabletop gaming experience.
