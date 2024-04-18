# Photog
Utilities for Rust photography

Run the /mf command to mark a photoframe, then use your instant camera to take a picture and post that to the frame.  There is a slight delay but it should get pasted within 5-10 seconds.

Optionally, the name of the player who took the photo will be overlaid on the image.

![](https://i.imgur.com/nI5k950.jpeg)

![](https://i.imgur.com/g0IuPB1.jpeg)

Can be done from anywhere on the map.  At this time, you can only target one frame at a time.

## Commands

  - `mf` -- Mark or unmark a photoframe to receive the snapshot

## Permissions

  - `photog.use` -- If RequirePermission is set in the config, players must have this permission to select a frame.

## Configuration

```json
{
  "lockOnPaint": false,
  "leaveOpen": false,
  "debug": true,
  "RequirePermission": false,
  "overlayPhotographerName": true,
  "Version": {
    "Major": 1,
    "Minor": 0,
    "Patch": 1
  }
}
```

If lockOnPaint is true, the photo frame will be locked after the snapshot is transferred.

If leaveOpen is true, the frame will continue to be targeted by subsequent snapshots.  Note that, if lockOnPaint is true, you will not be able to transfer a snapshot.

If overlayPhotographerName is true, the Steam name of the photographer will be overlaid on the photo item and the frame.

## TODO
  1. Provide more flexibility about how and when photos can be updated.

