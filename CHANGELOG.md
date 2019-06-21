# Change Log
## [1.0.1.1] - 2019-06-19
### Added
- MINOR Can now check if the user has an internet connection. Could be used in the future.

### Changed

### Fixed
- PATCH Sound no longer plays when the MainForm opens (caused by setting the useanalog checked event link and then setting the value to loaded data which triggered it).
- MINOR Skips checking for a new version if there is no internet connection.

## [1.9.0.0] - 2019-06-20
### Added
- MINOR "ACTIONS PERFORMED EVERYTIME AT LAUNCH" text added to the LAUNCH OPTIONS menu to explain what each option does to avoid appending "on play" to the end of every option.
- MINOR Button that opens the download page for Livesplit.
- MINOR Direct the program to where Livesplit is installed and open Livesplit when the user presses play.
- MINOR Button that opens the Super Meat Boy page on Speedrun.com.

### Changed
- MINOR "DELETE DATA ON PLAY (ANY% RUNS)" to "DELETE SAVE DATA (ANY% RUNS)".
- MINOR "cbDeleteDataOnPlay" to "cbDeleteSaveData".
- MINOR "CbDeleteDataOnPlay_CheckedChanged" to "CbDeleteSaveData_CheckedChanged".
- MINOR Added an affiliation notice to the disclaimer.
- MINOR Moved the disclaimer text.

### Fixed

## [2.21.0.3] - 2019-06-21
### Added
- MINOR Button that opens the SMB Discord.
- MINOR Cfu form's progress bar shows overall progress.
- MINOR Cfu form icon.
- MINOR MainForm icon.

### Changed
- MINOR Check for internet connection moved to CheckForUpdatesForm.
- MINOR Reduced image sizes to match control sizes.
- MINOR Cfu form's appearance.
- MINOR Title of each form.
- MAJOR Assembly name to "SMB Launcher".
- MINOR Cfu form launches in the middle of the screen.
- MINOR LblStatus replaced by "Status:" label so is all displayed in the one label.
- MINOR Removed the close button from cfu form.
- MINOR Faster method of checking for an internet connection.

### Fixed
- PATCH Checking for updates is moved to a different thread - fixed CHUform not loading.
- PATCH Removed default text from lblStatus.
- PATCH Changing background image of mute button instead of the image.

## IN PROGRESS
### Added
- MINOR Minimise button.
- MINOR Closes when SMB is launched.
- MINOR Text describing each combo box in the controls menu.

### Changed
- MINOR MainForm opens in the middle of the screen.
- MINOR Livesplit launch removed.
- MINOR Launch list for launching multiple programs at launch.
- MINOR Relocated control menu combo boxes sleightly.

### Fixed
- PATCH reimplemented autosearch.