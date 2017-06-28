# Changelog
## v0.5.0b
### Changes
- Did a little housekeeping on the Main Menu. Moved the version text to the bottom right, added copyright information, added a little notification for which Steam account is playing the game, changed the way things scale a little bit and replaced the music.
- Made some minor changes to the way that the font size on Main Menu buttons scale.

## v0.4.0a
### Changes
- Implemented new sprites and animations for Drones, Speedsters and Savages (names are also new).
- Updated the basic map.
- Enemies now using pathfinding to walk around obstacles and get to the player.
- Made the music volume setting persistent.
- Added a new Multiplayer menu.
- You can now Host & Join games. The current form that it's in won't stay. We're working hard on a final lobby system.
- The camera now follows the player in multiplayer.
- Re-vamped the splash screen.
- Items now have icons both in the store and in the toolbar.
- Steam is now implemented into the game. The game will use your Steam Display Name as your in=game name.
- New bullet sprites that rotate to face the direction they are fired in.
- Added Mines as items. They can be purchased through the store.

### Bugfixes
- Menu selection no longer bugs out if the "maps" folder doesn't exist.
- Fixed the HUD from re-appearing after exiting the pause menu.
- Fixed the blur disappearing when opening the settings menu.
- Fixed an issue where the coins variable wouldn't reset after restarting a game in singleplayer.
- Fixed player not spawning after leaving and re-joining singleplayer game from pause menu.
- Pressing escape to leave the settings menu no longer causes a weird glitch with the pause menu.
- The settings menu is now accessible from the menu.

## v0.3.0a
### Changes
- Added Main Menu music.
- Added randomly shuffling music to the game.
- Enemies now collide with each other, but not the player.
- Added a new combat system. Now you aim using the mouse or the left thumbstick and fire using the left mouse button or the right trigger.
- Added a notification for when a new song plays that displays the song name and artist.
- Removed the old camera and replaced it with a smooth camera that only follows the player.
- Added health bars to enemies.
- Health bars only show up after hitting an enemy, and disappear after 5 seconds.
- You can use the ESC key to close the store as well.
- Coins now "burst" out from a dying enemy.
- Removed the "Not Basic" map.
- Renamed "Basic" to "Testing Map."

### Bugfixes
- Toolbar is no longer briefly visible at the beginning of a game.
- Bullets now go through coins.
- Bullets also no longer collide with or hurt the shooting entity.
- Store exit mechanics are a bit smarter now.
- Game UI properly hides and unhides based on the state of the Pause Menu.
- Health can no longer go past maxHealth.
- Fixed issues with melee attacks.
- You can no longer buy as many items as you want (e.g. added check for coin count)
