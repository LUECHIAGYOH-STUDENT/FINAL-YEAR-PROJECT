# Final Year Project - Parcel Rush

Parcel Rush is a third-person arcade delivery game built in Unity. The player collects parcels and delivers them around a small city before the round timer expires.

The project also includes a responsive companion website. It can display orders, the courier's position, scores and city gadget controls. A Node.js and Express backend shares game state between Unity and the website.

## Repository structure

- `Game/` - Unity game client
- `Backend/` - Node.js and Express API
- `DispatcherWeb/` - companion website
- `Documents/` - proposal, research, diagrams and testing records
- `Marketing/` - logo, screenshots and promotional material

## Team workflow

1. Pull the latest `main` branch before starting work.
2. Create a branch for one task.
3. Commit small, working changes with a clear message.
4. Push the branch and open a pull request.
5. Ask another team member to review it before merging.

## Suggested branches

- `feature/unity-gameplay`
- `feature/website-backend`
- `feature/art-ui-research`

## Important Unity rules

- Everyone must use the same Unity version.
- Keep Unity `.meta` files with their matching assets.
- Do not commit `Library`, `Temp`, `Logs`, `Obj` or `UserSettings`.
- Avoid editing the same Unity scene at the same time.

