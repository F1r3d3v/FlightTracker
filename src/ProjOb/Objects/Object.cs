﻿using NetworkSourceSimulator;
using ProjOb.Events;
using ProjOb.IO;
using System.Text.Json.Serialization;

namespace ProjOb
{
    public abstract class Object : IExpandable, IObjectObserver
    {
        [JsonPropertyOrder(-8)]
        public UInt64 ID { get; set; }

        public abstract void Apply(IComponent component);
        public abstract T Apply<T>(IComponent<T> component);

        public virtual void OnIDChanged(object sender, IDUpdateArgs args)
        {
            ID = args.NewObjectID;
            Logger.InfoAsync($"Object ID {ID}:");
            Logger.InfoAsync($"  ID {ID} -> {args.NewObjectID}");
        }
        public virtual void OnPositionChanged(object sender, PositionUpdateArgs args) { }
        public virtual void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args) { }
    }
}