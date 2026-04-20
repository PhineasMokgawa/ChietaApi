# Scenario Instructions: .NET Version Upgrade

## Scenario
- **ID**: dotnet-version-upgrade
- **Goal**: Upgrade ChietaApi solution from .NET 6 to .NET 10.0 (LTS)
- **Solution**: `C:\Users\dmawasha\source\ChietaApi\CHIETAMIS.sln`

## Preferences

### Flow Mode
**Automatic** — Run end-to-end, only pause when blocked or needing user input.

### Source Control
- Source branch: `master`
- Working branch: `upgrade-to-NET10`

### Technical Preferences
- **Target Framework**: `net10.0` (LTS)

### Custom Instructions
- **DO NOT** alter login logic — it is working and must be preserved exactly as-is.
- **Ensure working flows** for: Register, Reset Password, OTP, and New Password Update.
- Authentication/identity code changes must be limited to breaking-change fixes only (package updates, namespace changes, API renames). No logic restructuring.

## Key Decisions Log
- 2025-07-10: User confirmed net10.0 (LTS) as upgrade target.
- 2025-07-10: Flow mode set to Automatic.
- 2025-07-10: User explicitly requested login logic be preserved; register/reset-password/OTP/new-password flows must remain functional.
