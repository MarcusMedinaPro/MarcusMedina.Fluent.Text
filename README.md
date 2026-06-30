# MarcusMedina.Fluent.Text

[![NuGet](https://img.shields.io/nuget/v/MarcusMedina.Fluent.Text.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Text/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MarcusMedina.Fluent.Text.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MarcusMedina.Fluent.Text/)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](#)
[![.NET](https://img.shields.io/badge/.NET-10.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)
![Open Source](https://raw.githubusercontent.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/main/assets/open-source.svg)
[![Build](https://img.shields.io/github/actions/workflow/status/MarcusMedinaPro/MarcusMedina.Fluent.Text/release.yml?branch=main&label=Build&style=for-the-badge&logo=github)](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/actions)
[![Signed](https://img.shields.io/badge/Signed-Sigstore-green?style=for-the-badge&logo=linux)](https://docs.sigstore.dev)



**Fluent builder for Text manipulation**

## Installation

```bash
dotnet add package MarcusMedina.Fluent.Text
```

## Status

This package is under active development in `_WIP_`.

## Documentation

- [Getting Started](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/blob/main/csharp/Manual/GettingStarted.md)
- [API Overview](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/blob/main/csharp/Manual/API.md)
- [Advanced Usage](https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/blob/main/csharp/Manual/Advanced.md)

## Development

```bash
cd csharp
dotnet build -c Release
```

## Package integrity

All releases are signed with [cosign](https://docs.sigstore.dev) (Sigstore keyless signing).

To verify a downloaded package, download both the `.nupkg` and its `.sigstore.json` bundle from the [GitHub Release](../https://github.com/MarcusMedinaPro/MarcusMedina.Fluent.Text/releases), then run:

```bash
cosign verify-blob <package.nupkg> \
  --bundle <package.nupkg.sigstore.json> \
  --certificate-identity-regexp "https://github.com/MarcusMedinaPro/.*/release.yml" \
  --certificate-oidc-issuer https://token.actions.githubusercontent.com
```

Expected output: `Verified OK`


## Built with Human + AI Collaboration

This library was written by **Marcus Medina** together with **Claude Code** (Anthropic) — not through "vibe coding" where you just describe and accept, but through genuine collaboration: planning together, reviewing each other's decisions, pushing back when something felt wrong, and iterating until the result felt right.

The goal was always to write code worth reading — the kind a student can open, understand, and learn from. AI was a partner in that process, not a shortcut around it.

If you're curious about this way of working, the source code and git history are open. Every decision has a reason behind it.

## Made for Curious Minds

This library was built with students in mind — not as a black box to copy and paste, but as a real-world example of how clean, purposeful code is written and shared.

Whether you're discovering C# for the first time, need a reliable helper for your school project, or are simply trying to fall in love with writing code — you're exactly who this was made for.

The source is open. Read it, fork it, break it, improve it. That's the whole point.

And if this library saved you an afternoon, or made something click that didn't before — that's everything.

*Non-students are equally welcome. Good code doesn't care about your diploma.*

⭐ If this helped you, consider starring the project on GitHub — it helps other students find it too.

💬 Have an idea, a feature request, or just want to say hi? Open an issue on GitHub — I'd love to hear from you.
