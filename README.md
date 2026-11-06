# Mad Byte Labs Patterns

A Unity Package Manager package with eleven reusable game programming patterns. All runtime source files and their assembly definition are directly in the flat `Patterns` folder, without subfolders. All runtime types use the `MadByteLabs.Patterns` namespace. Every C# file contains exactly one class or interface, including helpers, samples, and tests. All types and methods have English XML documentation; parameters and non-void return values are documented individually.

## Installation

1. Keep this package folder in a permanent location outside your project's Assets folder.
2. Open **Window → Package Manager** in Unity.
3. Select **+ → Install package from disk** (or **Add package from disk**).
4. Select this folder's `package.json`.

Alternatively, copy the whole folder into your project's `Packages` directory as an embedded package.

The intended API baseline is Unity 2022.3 or later.

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

## Documentation

[View the documentation](https://mad-byte-labs.github.io/com.madbytelabs.unity.patterns/).

The documentation includes an API reference generated from the C# XML comments and guides for using the patterns. It is built with Doxygen and published to GitHub Pages automatically after each push to `main`.

To generate the documentation locally, install Doxygen and run the following command from the package root:

    doxygen Doxyfile

Open `docs-build/html/index.html` to browse the generated documentation.

## Code conventions

This package follows the [Mad Byte Labs Code Conventions](https://github.com/Mad-Byte-Labs/coding-standards/blob/main/CODE_CONVENTIONS.md), retrieved on November 5, 2026. The official `.editorconfig` is included at the package root.

## License

Copyright (c) 2026 Mad Byte Labs. Licensed under the [MIT License](LICENSE.md).

Commercial use, modification, distribution, and sublicensing are permitted. Include the copyright and permission notices in all copies or substantial portions of the software. The software is provided without warranty.
