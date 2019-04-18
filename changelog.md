# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased - 2019-01-01
- [Commits](https://github.com/unity-game-framework/ugf-types/compare/0.0.0...0.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/0?closed=1)

### Added
- Nothing.

### Changed
- Nothing.

### Deprecated
- Nothing.

### Removed
- Nothing.

### Fixed
- Nothing.

### Security
- Nothing.

## 2.1.0 - 2019-04-18
- [Commits](https://github.com/unity-game-framework/ugf-types/compare/1.4.0...1.4.1)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/4?closed=1)

### Added
- `TypesUtility.GetTypesAll` to enumerate through the all available types.
- `TypesAllEnumerable` to enumerate through the all available types. (#13)

### Changed
- Package dependencies:
    - `com.ugf.assemblies`: from `1.4.0` to `1.4.1`.

## 2.0.0 - 2019-04-17
- [Commits](https://github.com/unity-game-framework/ugf-types/compare/1.1.0...2.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/3?closed=1)

### Added
- `ITypeDefine`, `ITypeDefine<T>`, `TypeDefineBase<T>` and `TypeDefine<T>` to define identifiers for external types.
- `TypeDefineAttribute` to mark type defines.
- `TypesUtility.GetTypeDefines` to collect defines that contains type define attribute.
- `TypesUtility.GetTypes` overload to collect types that contains type identifier attribute.
- `TypesUtility.TryCreateType` as simplified way to create objects from the specified type.

### Changed
- Package dependencies:
    - `com.ugf.assemblies`: from `1.1.0` to `1.4.0`.
- `ITypeProvider`, `ITypeProvider<T>` and `TypeProvider<T>` have been refactored. (#9)
- `TypeIdentifierAttributeBase` has been rewritten and changed to non-abstract `TypeIdentifierAttribute`.
- `TypesUtility.GetTypes` overloads that works with provider, to support type defines.

### Removed
- `TypeIdentifierAttributeBase` and replaced by non-abstract `TypeIdentifierAttribute`.
- `TypesUtility.AddTypes` because it was useless.
- `TypesUtility.CollectTypes` because using standard ways more efficient.

## 1.1.0 - 2019-04-14
- [Commits](https://github.com/unity-game-framework/ugf-types/compare/1.0.0...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/2?closed=1)

### Added
- `TypesUtility.CollectTypes` to collect types with validation.
- `TypesEditorGUIUtility.GetTypesDropdown` to create an advanced editor dropdown that displays types selection menu.
- `TypesUtility.TryGetIdentifierFromType` to retrieve type identifier from attribute directly.

## 1.0.0 - 2019-03-24
- [Commits](https://github.com/unity-game-framework/ugf-types/compare/93305d0...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/1?closed=1)

### Added
- This is a initial release.

---
> Unity Game Framework | Copyright 2019
