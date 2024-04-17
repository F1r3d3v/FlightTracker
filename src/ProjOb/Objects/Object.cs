﻿using System.Text.Json.Serialization;
using NetworkSourceSimulator;
using ProjOb.Events;
using ProjOb.IO;

namespace ProjOb
{
    public abstract class Object : IExpandable, IExpandable<string>, IObjectObserver
    {
        [JsonPropertyOrder(-8)]
        public UInt64 ID { get; set; }

        public abstract void Apply(IComponent component);
        public abstract string Apply(IComponent<string> component);

        public virtual void OnIDChanged(object sender, IDUpdateArgs args)
        {
            Logger.Info($"Object ID {ID}:");
            Logger.Info($"  ID {ID} -> {args.NewObjectID}");
            ID = args.NewObjectID;
        }
        public virtual void OnPositionChanged(object sender, PositionUpdateArgs args) { }
        public virtual void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args) { }
    }
}