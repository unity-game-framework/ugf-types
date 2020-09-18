# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.1.0-preview](https://github.com/unity-game-framework/ugf-types/releases/tag/3.1.0-preview) - 2020-09-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/8?closed=1)  
    

### Changed

- Update project to Unity 2020.1 ([#23](https://github.com/unity-game-framework/ugf-types/pull/23))

## [3.0.0-preview](https://github.com/unity-game-framework/ugf-types/releases/tag/3.0.0-preview) - 2019-11-09  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/2.2.0...3.0.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/7?closed=1)

### Added
- `ITypeValidation` to select target types using enumerable from `TypesUtility.GetTypesAll`.

### Changed
- Update to Unity 2019.3
- Moved all attributes relatives code under the `UGF.Types.Runtime.Attributes` namespace.
- `TypesUtility`: moved all attributes relative methods to `TypesIdentifierUtility` class.

### Removed
- All editor specific code. (Moved to [UGF.EditorTools](https://github.com/unity-game-framework/ugf-editortools))
- Removed TypeDefines.
- Package dependencies:
    - `com.ugf.assemblies`: `1.5.2`.

## [2.2.0](https://github.com/unity-game-framework/ugf-types/releases/tag/2.2.0) - 2019-05-20  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/2.1.1...2.2.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/6?closed=1)

### Added
- `TypeIdentifierInt32Attribute` and `TypeIdentifierStringAttribute` to support register types with identifier types as `Int32` or `String`.
- `TypesUtility.TryGetIdentifierFromType` overload with additional parameter that allow to specify identifier type.
- `TypesUtility.TryGetIdentifierAttribute` to get attribute that define the identifier of the specific type.
- `TypesUtility.TryCreateType` overloads with constructor arguments.

### Changed
- Package dependencies:
    - `com.ugf.assemblies`: from `1.5.1` to `1.5.2`.
- `TypesUtility` removed inherit parameter for the all methods that gather attribute types, now all attributes gathered without inheritance.

### Deprecated
- `TypesUtility.TryGetIdentifierFromType` overload with identifier as `object` has been deprecated, use overload with additional parameter that allow to specify identifier type.

## [2.1.1](https://github.com/unity-game-framework/ugf-types/releases/tag/2.1.1) - 2019-04-23  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/2.1.0...2.1.1)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/5?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.assemblies`: from `1.4.1` to `1.5.1`.

### Fixed
- `UGF.Types.Editor.asmdef` targeting to any platform what cause build errors.

## [2.1.0](https://github.com/unity-game-framework/ugf-types/releases/tag/2.1.0) - 2019-04-18  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/2.0.0...2.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/4?closed=1)

### Added
- `TypesUtility.GetTypesAll` to enumerate through the all available types.
- `TypesAllEnumerable` to enumerate through the all available types. (#13)

### Changed
- Package dependencies:
    - `com.ugf.assemblies`: from `1.4.0` to `1.4.1`.

## [2.0.0](https://github.com/unity-game-framework/ugf-types/releases/tag/2.0.0) - 2019-04-17  

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

## [1.1.0](https://github.com/unity-game-framework/ugf-types/releases/tag/1.1.0) - 2019-04-14  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/1.0.0...1.1.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/2?closed=1)

### Added
- `TypesUtility.CollectTypes` to collect types with validation.
- `TypesEditorGUIUtility.GetTypesDropdown` to create an advanced editor dropdown that displays types selection menu.
- `TypesUtility.TryGetIdentifierFromType` to retrieve type identifier from attribute directly. (#4)

## [1.0.0](https://github.com/unity-game-framework/ugf-types/releases/tag/1.0.0) - 2019-03-24  

- [Commits](https://github.com/unity-game-framework/ugf-types/compare/93305d0...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-types/milestone/1?closed=1)

### Added
- This is a initial release.


