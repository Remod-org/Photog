# Photog
Utilities for Rust photography

Run the /mf command to mark a photoframe, then use your instant camera to take a picture and post that to the frame.

Can be done from anywhere on the map.  At this time, you can only target one frame at a time.

## Commands

  - `mf` -- Mark or unmark a photoframe to receive the snapshot

## Configuration

```json
{
  "lockOnPaint": false,
  "leaveOpen": false,
  "debug": false,
  "Version": {
    "Major": 0,
    "Minor": 0,
    "Patch": 2
  }
}
```

If lockOnPaint is true, the photo frame will be locked after the snapshot is transferred.

If leaveOpen is true, the frame will continue to be targeted by subsequent snapshots.  Note that, if lockOnPaint is true, you will not be able to transfer a snapshot.

## TODO
  1. Provide more flexibility about how and when photos can be updated.
  2. Add permissions

