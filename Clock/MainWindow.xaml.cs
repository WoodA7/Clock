using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransformGroup _transformGroup;
        private RotateTransform _rotateTrsf;
        private DoubleAnimation _rotateAnimation;

        private TransformGroup _transformGroup1;
        private RotateTransform _rotateTrsf1;

        private TransformGroup _transformGroup2;
        private RotateTransform _rotateTrsf2;

        private int currsec = DateTime.Now.Second;
        private int currmin = DateTime.Now.Minute;
        private int currhour = DateTime.Now.Hour;

        public MainWindow()
        {
            InitializeComponent();
            _transformGroup = new TransformGroup();
            _rotateTrsf = new RotateTransform();
            _transformGroup.Children.Add(_rotateTrsf);

            _transformGroup1 = new TransformGroup();
            _rotateTrsf1 = new RotateTransform();
            _transformGroup1.Children.Add(_rotateTrsf1);

            _transformGroup2 = new TransformGroup();
            _rotateTrsf2 = new RotateTransform();
            _transformGroup2.Children.Add(_rotateTrsf2);

            Canv.RenderTransform = _transformGroup;
            Canv1.RenderTransform = _transformGroup1;
            Canv2.RenderTransform = _transformGroup2;

            _rotateAnimation = new DoubleAnimation();
            _rotateAnimation.From = 0.0;
            _rotateAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.0));

            _rotateAnimation.To = currsec * 6.0;
            _rotateTrsf.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);

            _rotateAnimation.To = currmin * 6.0;
            _rotateTrsf1.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);

            _rotateAnimation.To = currhour > 11 ? (currhour - 12) * 30 : currhour * 30;
            _rotateTrsf2.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);

            new Thread(Rotate).Start();
        }

        private void Rotate()
        {

            while (true)
            {
                try
                {
                    Canv.Dispatcher.Invoke(UpdateAngle);
                    currsec++;
                    if (currsec % 61 == 0)
                    {
                        Canv1.Dispatcher.Invoke(UpdateAngle1);
                        currsec = 0;
                    }
                    //if (currsec % 3600 == 0)
                    //        Canv2.Dispatcher.Invoke(UpdateAngle2);
                }
                catch (Exception)
                {
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        private void UpdateAngle()
        {
            _rotateAnimation.To = Math.Floor(_rotateTrsf.Angle / 6 + 1) * 6;
            _rotateTrsf.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);
            _rotateAnimation.From = _rotateAnimation.To;
        }

        private void UpdateAngle1()
        {
            _rotateAnimation.To = Math.Floor(_rotateTrsf1.Angle / 6 + 1) * 6;
            _rotateTrsf1.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);
            _rotateAnimation.From = _rotateAnimation.To;
        }

        private void UpdateAngle2()
        {
            _rotateAnimation.To = Math.Floor(_rotateTrsf2.Angle / 6 + 1) * 6;
            _rotateTrsf2.BeginAnimation(RotateTransform.AngleProperty, _rotateAnimation);
            _rotateAnimation.From = _rotateAnimation.To;
        }
    }

}
