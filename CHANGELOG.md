# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.1.3] - 2026-07-31
### Fixed
- correct innacurate TMP sample description

## [0.1.2] - 2026-07-31

### Fixed
- add ~ prefix to samples folder name preventing from pulling pacakged

## [0.1.1] - 2026-07-31 

### Fixed
- Missing `.meta` files preventing the package from being added via Package Manager


## [0.1.0] - 2026-07-29

### This is the first release of *\<UnityToolkit\>*.

### Added
- `GameEvent` ScriptableObject and `GameEventListener` component for editor-driven events
- `ScreenFader` singleton component (auto-created on first static call) and `ScreenFaderController`
- Optional TMP-dependent sample (`Samples~/TMPSupport`) with all TextMeshPro-specific scripts