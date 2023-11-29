using GardenPlotPlanner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GardenPlotPlanner.ViewModel
{
    public class AppVM : INotifyPropertyChanged
    {
        public AppVM()
        {
            Models = new ObservableCollection<BuildingModel>();
            _createModel(30, 30, 140, 130);
            _createModel(66, 77, 120, 130);
            _createModel(370, 45, 140, 130);
            _createModel(33, 45, 110, 130);
            _createModel(22, 90, 120, 150);
            ActiveModelsVM = new ObservableCollection<BuildingModelVM>();
            OnPreviewMouseDown = new RelayCommand(PreviewMouseDown);
            OnPreviewMouseMove = new RelayCommand(PreviewMouseMove);
            OnPreviewMouseUp = new RelayCommand(PreviewMouseUp);
            OnChangeTypeBuilding = new RelayCommand(ChangeTypeBuilding);
        }

        private void _createModel(double x, double y, double width, double height)
        {
            var model = new BuildingModel();
            var modelVM = new BuildingModelVM(x, y, width, height, @"\Images\modeldom.png", _makeModelActive);
            model.DataContext = modelVM;
            Models.Add(model);
        }

        private Point? _startPoint;
        private bool _isChangingPosition = false;


        #region Buildings
        public ICommand OnChangeTypeBuilding { get; }

        public void ChangeTypeBuilding(object arg)
        {

            switch (arg)
            {
                case "1":
                    Buildings = new ObservableCollection<AppVMBuilding>();
                    Buildings.Add(new AppVMBuilding("Дом одноэтажный", @"\Images\modeldom.png"));
                    break;
            }
        }

        private ObservableCollection<AppVMBuilding> _buildings;
        public ObservableCollection<AppVMBuilding> Buildings
        {
            get => _buildings;
            set
            {
                _buildings = value;
                NotifiyPropertyChanged(nameof(Buildings));
            }
        }
        #endregion

        #region Mouse

        public ICommand OnPreviewMouseLeave { get; }
        public ICommand OnPreviewMouseDown { get; }
        public ICommand OnPreviewMouseMove { get; }
        public ICommand OnPreviewMouseUp { get; }

        public void PreviewMouseDown(object arg)
        {
            var targetElement = Mouse.DirectlyOver as UIElement;
            if (targetElement.GetType() != typeof(Canvas))
            {
                var e = (MouseButtonEventArgs)arg;
                _startPoint = e.GetPosition(e.Source as IInputElement);
                //foreach (var modelVM in ActiveModelsVM)
                //{
                //    modelVM.OldPoint = new Point(modelVM.RectX, modelVM.RectY);
                //}
            }
            else
            {
                foreach (var modelVM in ActiveModelsVM)
                {
                    modelVM.Active = false;
                }
                ActiveModelsVM.Clear();
            }
        }

        public void PreviewMouseMove(object arg)
        {
            if (_isChangingPosition && ActiveModelsVM.Count != 0)
            {
                var e = (MouseEventArgs)arg;
                var pos = e.GetPosition(e.Source as IInputElement);
                var p = pos - _startPoint.Value;
                foreach (var modelVM in ActiveModelsVM)
                {
                    if (modelVM.OldPoint != null)
                    {
                        modelVM.CordX = modelVM.OldPoint.Value.X + p.X;
                        modelVM.CordY = modelVM.OldPoint.Value.Y + p.Y;
                    }
                }
            }
        }

        public void PreviewMouseUp(object arg)
        {
            var e = (MouseButtonEventArgs)arg;
            foreach (var modelVM in ActiveModelsVM)
            {
                if (modelVM.OldPoint != null)
                {
                    modelVM.OldPoint = null;
                }
            }
            _isChangingPosition = false;
        }

        #endregion

        #region Models
        private ObservableCollection<BuildingModel> _models;
        public ObservableCollection<BuildingModel> Models
        {
            get => _models;
            set
            {
                _models = value;
                NotifiyPropertyChanged(nameof(Models));
            }
        }

        private ObservableCollection<BuildingModelVM> _activeModelsVM;
        public ObservableCollection<BuildingModelVM> ActiveModelsVM
        {
            get => _activeModelsVM;
            set
            {
                _activeModelsVM = value;
                NotifiyPropertyChanged(nameof(ActiveModelsVM));
            }
        }
        #endregion

        private void _makeModelActive(BuildingModelVM modelVM, bool active, MouseButtonEventArgs e)
        {
            if (active)
            {
                if (!ActiveModelsVM.Contains(modelVM))
                {
                    if (ActiveModelsVM.Count == 0)
                    {
                        _isChangingPosition = true;
                        modelVM.OldPoint = new Point(modelVM.CordX, modelVM.CordY);
                    }
                    ActiveModelsVM.Add(modelVM);
                }
                else
                {
                    _isChangingPosition = true;
                    //_startPoint = e.GetPosition(e.Source as IInputElement);
                    foreach (var modelVM1 in ActiveModelsVM)
                    {
                        modelVM1.OldPoint = new Point(modelVM1.CordX, modelVM1.CordY);
                    }
                }
            }
            else
                ActiveModelsVM.Remove(modelVM);
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifiyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }


    public class AppVMBuilding
    {
        public AppVMBuilding(string name, string imageSource)
        {
            Name = name;
            ImageSource = new BitmapImage(new Uri(imageSource, UriKind.Relative));
        }

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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifiyPropertyChanged(nameof(Name));
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
