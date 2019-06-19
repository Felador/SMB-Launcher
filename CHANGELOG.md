# Change Log
## [1.0.1.1] - 2019-06-19
### Added
- MINOR Can now check if the user has an internet connection. Could be used in the future.

### Changed

### Fixed
- PATCH Sound no longer plays when the MainForm opens (caused by setting the useanalog checked event link and then setting the value to loaded data which triggered it).
- MINOR Skips checking for a new version if there is no internet connection.

## [0.0.0.0] - 2019-06-XX
### Added
- MINOR "ACTIONS PERFORMED EVERYTIME AT LAUNCH" text added to the LAUNCH OPTIONS menu to explain what each option does to avoid appending "on play" to the end of every option.
- MINOR Button that opens the download page for Livesplit.
- MINOR Direct the program to where Livesplit is installed and open Livesplit when the user presses play.

### Changed
- MINOR "DELETE DATA ON PLAY (ANY% RUNS)" to "DELETE SAVE DATA (ANY% RUNS)".
- MINOR "cbDeleteDataOnPlay" to "cbDeleteSaveData".
- MINOR "CbDeleteDataOnPlay_CheckedChanged" to "CbDeleteSaveData_CheckedChanged".
- MINOR Added an affiliation notice to the disclaimer.
- MINOR Moved the disclaimer text.

### Fixed