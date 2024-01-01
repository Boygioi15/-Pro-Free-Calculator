using System;
using System.Drawing;
using System.Windows.Forms;
using CustomUserControls;

/// <summary>
/// Support only one animation for each control
/// </summary>
enum AnimationType
{
    Slide
};
internal class Animator
{
    //duration in milisec
    static public void CreateAnimationForControl(Control control, AnimationType type)
    {

    }
    static public void StartAnimation(Control control)
    {

    }
    static public void StopAnimation(Control control)
    {

    }
    static public void Slide(Control control, Point start, Point end, int deltaT, int duration)
    {
        //1 step is 10ms
        int step = duration / deltaT;
        int offsetXForEachStep = (start.X-end.X)/ step;
        int offsetYForEachStep = (start.Y - end.Y) / step;

        Timer tmr = new Timer();
        tmr.Interval = deltaT;
        tmr.Tick += (sender, args) =>
        {
            int currentX = control.Location.X + offsetXForEachStep;
            int currentY = control.Location.Y + offsetYForEachStep;
            control.Location = new Point(currentX, currentY);
            if (step < 1)
            {
                control.Location = end;
                tmr.Stop();
                tmr.Dispose();
                return;
            }
            step--;
            
        };
        tmr.Enabled = true;
        tmr.Start();  
    }
}