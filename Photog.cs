using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Photog", "RFC1920", "0.0.1")]
    [Description("Paste photo from Instant Camera to a PhotoFrame")]
    internal class Photog : RustPlugin
    {
        private ConfigData configData;

        private Dictionary<ulong, NetworkableId> frames = new Dictionary<ulong, NetworkableId>();

        private void OnServerInitialized()
        {
            LoadConfigVariables();
            AddCovalenceCommand("mf", "cmdMarkFrame");
        }

        private void OnPhotoCaptured(PhotoEntity photo, Item item, BasePlayer player, byte[] numArray)
        {
            Puts($"{player.displayName} took a photo {item.name}");
            if (frames.ContainsKey(player.userID))
            {
                Puts("Try to copy photo");
                BaseNetworkable frame = BaseNetworkable.serverEntities.Find(frames[player.userID]);
                if (frame is PhotoFrame)
                {
                    Puts("Found a photo frame");
                    PhotoFrame photoFrame = (PhotoFrame)frame;
                    BaseNetworkable.LoadInfo info = new BaseNetworkable.LoadInfo()
                    {
                        msg = new ProtoBuf.Entity()
                        {
                            photoFrame = new ProtoBuf.PhotoFrame()
                            {
                                ShouldPool = true,
                                overlayImageCrc = photo.ImageCrc,
                                photoEntityId = photo.net.ID,
                                editHistory = photo.EditingHistory
                            }
                        }
                    };
                    Puts("Loading image to photo frame");
                    photoFrame.Load(info);
                    photoFrame.SendNetworkUpdateImmediate();
                    if (configData.lockOnPaint)
                    {
                        photoFrame.SetFlag(BaseEntity.Flags.Locked, true);
                    }
                }
                if (!configData.leaveOpen)
                {
                    frames.Remove(player.userID);
                }
            }
        }

        [Command("mf")]
        private void cmdMarkFrame(IPlayer iplayer, string command, string[] args)
        {
            BasePlayer player = iplayer.Object as BasePlayer;
            PhotoFrame frame = FindEntity(player) as PhotoFrame;
            if (frame != null)
            {
                if (frames.ContainsKey(player.userID))
                {
                    frames.Remove(player.userID);
                    iplayer.Reply("Frame has been unmarked");
                    return;
                }
                frames.Add(player.userID, frame.NetworkID);
                iplayer.Reply("Frame has been marked");
            }
        }

        private static BaseEntity FindEntity(BasePlayer player)
        {
            RaycastHit hit;
            if (!Physics.Raycast(player.eyes.HeadRay(), out hit)) return null;
            return hit.GetEntity();
        }

        public class ConfigData
        {
            public bool lockOnPaint;
            public bool leaveOpen;
            public VersionNumber Version;
        }

        private void LoadConfigVariables()
        {
            configData = Config.ReadObject<ConfigData>();

            configData.Version = Version;
            SaveConfig(configData);
        }

        private void SaveConfig(ConfigData config)
        {
            Config.WriteObject(config, true);
        }

        protected override void LoadDefaultConfig()
        {
            Puts("Creating new config file.");
            ConfigData config = new ConfigData
            {
                leaveOpen = false,
                lockOnPaint = false
            };
        }
    }
}
