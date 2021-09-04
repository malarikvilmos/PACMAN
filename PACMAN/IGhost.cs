using System;
using System.Timers;

namespace PACMAN
{
    interface IGhost
    {
        void Chase(Object source, ElapsedEventArgs e);

        void Scatter(Object source, ElapsedEventArgs e);

        void SwitchModes(Object source, ElapsedEventArgs e);
    }
}
