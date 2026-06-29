<p align="center">

# FluentBuilders.Text

[![NuGet](https://img.shields.io/nuget/v/MarcusMedina.Fluent.Text.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Text/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MarcusMedina.Fluent.Text.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Text/)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](#)
[![.NET](https://img.shields.io/badge/.NET-10.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)
![Open Source](https://badges.frapsoft.com/os/v3/open-source.svg?v=103)
[![Build](https://img.shields.io/github/actions/workflow/status/MarcusMedinaPro/FluentBuilders.Text/release.yml?branch=main&label=Build&style=for-the-badge&logo=github)](https://github.com/MarcusMedinaPro/FluentBuilders.Text/actions)
[![Signed](https://img.shields.io/badge/Signed-Sigstore-green?style=for-the-badge&logo=linux)](https://docs.sigstore.dev)

</p>

**Fluent builder for Text manipulation**

## Installation

```bash
dotnet add package MarcusMedina.Fluent.Text.Core.Core
```

## Status

This package is under active development in `_WIP_`.

## Documentation

- [Getting Started](./csharp/Manual/GettingStarted.md)
- [API Overview](./csharp/Manual/API.md)
- [Advanced Usage](./csharp/Manual/Advanced.md)

## Development

```bash
cd csharp
dotnet build -c Release
```

## Package integrity

All releases are signed with [cosign](https://docs.sigstore.dev) (Sigstore keyless signing).

To verify a downloaded package, download both the `.nupkg` and its `.sigstore.json` bundle from the [GitHub Release](../../releases), then run:

```bash
cosign verify-blob <package.nupkg> \
  --bundle <package.nupkg.sigstore.json> \
  --certificate-identity-regexp "https://github.com/MarcusMedinaPro/.*/release.yml" \
  --certificate-oidc-issuer https://token.actions.githubusercontent.com
```

Expected output: `Verified OK`

