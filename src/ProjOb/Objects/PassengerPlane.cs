﻿using ProjOb.Media;

namespace ProjOb
{
    public class PassengerPlane : Plane, IReportable
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;
    }
}