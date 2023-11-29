using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GardenPlotPlanner.ViewModel
{
    public class BuildingModelVM : INotifyPropertyChanged
    {
        public BuildingModelVM(double cordX, double cordY, double width, double height, string imageSource, Action<BuildingModelVM, bool, MouseButtonEventArgs> makeActive)
        {
            CordX = cordX;
            CordY = cordY;
            Width = width;
            Height = height;
            ImageSource = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            Active = false;
            _makeActive = makeActive;
            OnMouseDownIcon = new RelayCommand(MouseDownIcon);
        }

        public Point? OldPoint;


        public ICommand OnMouseDownIcon { get; }
        public void MouseDownIcon(object arg)
        {
            Active = true;
            _makeActive.Invoke(this, Active, (MouseButtonEventArgs)arg);
        }

        public Action<BuildingModelVM, bool, MouseButtonEventArgs> _makeActive;

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                NotifiyPropertyChanged(nameof(ImageSource));
            }
        }

        #region X_Y
        private double _cordX;
        public double CordX
        {
            get => _cordX;
            set
            {
                _cordX = value;
                NotifiyPropertyChanged(nameof(CordX));
            }
        }

        private double _cordY;
        public double CordY
        {
            get => _cordY;
            set
            {
                _cordY = value;
                NotifiyPropertyChanged(nameof(CordY));
            }
        }

        private double _rotate;
        public double Rotate
        {
            get => _rotate;
            set
            {
                _rotate = value;
                NotifiyPropertyChanged(nameof(Rotate));
            }
        }
        #endregion

        #region Width_Height
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                NotifiyPropertyChanged(nameof(Width));
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                NotifiyPropertyChanged(nameof(Height));
            }
        }
        #endregion

        private bool _active;
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                NotifiyPropertyChanged(nameof(Active));
            }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifiyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
