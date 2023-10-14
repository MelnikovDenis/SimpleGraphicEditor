﻿using SimpleGraphicEditor.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleGraphicEditor.Models;

public class SgePoint : INotifyPropertyChanged, IMoveable
{
    private double x = 0d;
    private double y = 0d;
    public LinkedList<SgeLine> AttachedLines { get; } = new LinkedList<SgeLine>();
    public double X 
    {
        get 
        {
            return x;
        }
        set 
        {
            x = value;
            OnPropertyChanged();
        }
    }
    public double Y 
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
            OnPropertyChanged();
        }
    }
    public SgePoint(Point point)
    {
        X = point.X;
        Y = point.Y;
    }
    public void Move(Point delta) 
    {
        X += delta.X;
        Y += delta.Y;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    public override bool Equals(object? obj) =>
        Equals(obj as SgePoint);

    public bool Equals(SgePoint? other) =>
        X == other?.X && Y == other?.Y;

    public override int GetHashCode() =>
       HashCode.Combine(x, y);

    
}