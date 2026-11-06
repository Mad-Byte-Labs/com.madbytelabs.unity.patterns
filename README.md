# Mad Byte Labs Patterns

A Unity Package Manager package with eleven reusable game programming patterns. All runtime source files and their assembly definition are directly in the flat `Patterns` folder, without subfolders. All runtime types use the `MadByteLabs.Patterns` namespace. Every C# file contains exactly one class or interface, including helpers, samples, and tests. All types and methods have English XML documentation; parameters and non-void return values are documented individually.

## Installation

1. Keep this package folder in a permanent location outside your project's Assets folder.
2. Open **Window → Package Manager** in Unity.
3. Select **+ → Install package from disk** (or **Add package from disk**).
4. Select this folder's `package.json`.

Alternatively, copy the whole folder into your project's `Packages` directory as an embedded package.

The intended API baseline is Unity 2022.3 or later. Validation uses Unity 6000.6.3f1; Unity 2022.3 and player builds have not been tested directly.

## Patterns and namespaces

All runtime types use `MadByteLabs.Patterns` and live directly in `Patterns/`.

| Pattern | API |
| --- | --- |
| Command | `ICommand`, `CommandHistory` |
| Flyweight | `FlyweightCache<TKey, TValue>` |
| Observer | `Signal<T>`; internal `Subscription` helper |
| Prototype | `IPrototype<T>`, `Prototype.Clone` |
| Singleton | `Singleton<T>` |
| State | `IState`, `StateMachine` |
| Event Queue | `EventQueue<T>` |
| Service Locator | `ServiceLocator` |
| Object Pool | `ObjectPool<T>`; internal reference comparer |
| Decorator | `IStat`, `ConstantStat`, `StatDecorator`, `AddStat`, `MultiplyStat` |
| Factory | `IFactory<T>`, `DelegateFactory<T>`, `PrefabFactory<T>` |

Samples and tests retain their separate assemblies outside `Patterns` so test and demo dependencies are excluded from the runtime assembly.

## Demo

Select the package in Package Manager, open **Samples**, and import **Patterns Demo**. Add `PatternsDemo` to an empty GameObject in a scene and enter Play mode. The Console prints results for all eleven patterns once. The demo owns and destroys its temporary Unity objects and disposes its subscriptions and pool.

## Usage

Use `using MadByteLabs.Patterns;` to import the runtime API. In a namespace that contains a conflicting name, use the fully qualified type, such as `MadByteLabs.Patterns.ServiceLocator`.

See `Documentation~/Patterns.md` for contracts, ownership, timing, and cleanup rules. The demo's helper types are separate files in feature folders; do not combine them into a single listing.

## Code conventions

This package follows the [Mad Byte Labs Code Conventions](https://github.com/Mad-Byte-Labs/coding-standards/blob/main/CODE_CONVENTIONS.md), retrieved on November 5, 2026. The official `.editorconfig` is included at the package root.

The project additionally requires exactly one type per file, including supporting types that the company standard would otherwise allow to be nested. English XML documentation is mandatory for every type and public method. Methods without parameters have no `param` tags; void methods and constructors have no `returns` tag.

## Tests

Install Unity Test Framework in the consuming project. Add `"com.madbytelabs.patterns"` to the top-level `testables` array in `Packages/manifest.json`, preserving existing entries. Open **Window → General → Test Runner**.

- **EditMode:** run `MadByteLabs.Patterns.Tests` for twelve tests covering commands, flyweights, subscriptions, cloning, states, queue dispatch, services, pooling, decorators, and factories.
- **PlayMode:** run `MadByteLabs.Patterns.PlayModeTests` for the singleton initialization and destruction test.

Tests and their helper types are organized by pattern, with exactly one type in every source file. See `Documentation~/Validation.md` for the recorded validation scope.

## Migration from 2.0.0

Version 3.0.0 restores one shared runtime namespace. Replace per-pattern imports such as `MadByteLabs.Patterns.Command` or `MadByteLabs.Patterns.Observer` with `MadByteLabs.Patterns`. Shorten fully qualified runtime types accordingly. Runtime files moved from `Runtime/<Pattern>/` to `Patterns/`; their existing Unity metadata GUIDs are preserved. Reimport the updated sample after removing the old imported demo folder.

The eleven-pattern scope and one-type-per-file rule are unchanged. When migrating from 1.0.0, also update IState.Tick implementations and named arguments to `deltaTimeSeconds`.

## License

Copyright (c) 2026 Mad Byte Labs. Licensed under the [MIT License](LICENSE.md).

Commercial use, modification, distribution, and sublicensing are permitted. Include the copyright and permission notices in all copies or substantial portions of the software. The software is provided without warranty.
