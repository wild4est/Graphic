using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Graphic.Models;
using Graphic.ViewModels.Pages;
using Graphic.Views;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Graphic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // peremenie
        private ObservableCollection<Shape> figures_colection;
        private ViewModelBase[] content_colection;
        private ObservableCollection<Names> figures_name;
        private ViewModelBase curent_content;
        private MainWindow mainWindow;
        private Canvas canvas;
        private int listbox_index = 0, combobox_index = 0, count_figure = 0;


        // constr
        public MainWindowViewModel(MainWindow mw)
        {
            figures_colection = new ObservableCollection<Shape>();
            figures_name = new ObservableCollection<Names>();

            mainWindow = mw;
            canvas = mainWindow.Find<Canvas>("canvas");

            content_colection = new ViewModelBase[]
            {
                new LineViewModel(ref figures_colection, ref figures_name),
                new PolyLineViewModel(ref figures_colection, ref figures_name),
                new PolygonViewModel(ref figures_colection, ref figures_name),
                new RectangleViewModel(ref figures_colection, ref figures_name),
                new EllipseViewModel(ref figures_colection, ref figures_name),
                new PathViewModel(ref figures_colection, ref figures_name)
            };
            curent_content = content_colection[0];

            Line line = new Line();
            line.StartPoint = new Avalonia.Point(10, 10);
            line.EndPoint = new Avalonia.Point(100, 10);
            line.Name = "lin";
            line.Stroke = SolidColorBrush.Parse("Blue");
            line.StrokeThickness = 1;
            figures_colection.Add(line);
            Names nev = new Names(line.Name, "line", "10,10", "100,10", line.StrokeThickness, "blue");
            figures_name.Add(nev);


            DeleteBut = ReactiveCommand.Create(() =>
            {
                figures_colection.RemoveAt(Listbox_index);
                figures_name.RemoveAt(Listbox_index);
            });
        }

        /*public async void LoadXML()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string[]? result = await openFileDialog.ShowAsync(mainWindow);
            figures_name.Clear();
            XmlSerializer xs = new XmlSerializer(figures_name.GetType());
            using (StreamReader rd = new StreamReader(result[0]))
            {
                figures_name = xs.Deserialize(rd) as ObservableCollection<Names>;
            }
            Convert name -> shape
        }*/

        public async void SaveXML()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string? result = await saveFileDialog.ShowAsync(mainWindow);
            string xmlstring = Convert(figures_name);
            XElement xElement = XElement.Parse(xmlstring);
            xElement.Save(result);
        }
        static string Convert(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public ObservableCollection<Names> Figures_name
        {
            get => figures_name;
            set
            {
                this.RaiseAndSetIfChanged(ref figures_name, value);
            }
        }


        // public ReactiveCommand<Unit, Unit> LoadXML { get; }
        public ReactiveCommand<Unit, Unit> DeleteBut { get; }

        // methods for intefrace
        public ObservableCollection<Shape> Figures_colection
        {
            get => figures_colection;
            set
            {
                this.RaiseAndSetIfChanged(ref figures_colection, value);
            }
        }

        public int Listbox_index
        {
            get => listbox_index;
            set
            {
                this.RaiseAndSetIfChanged(ref listbox_index, value);
            }
        }

        public int Combobox_index
        {
            get => combobox_index;
            set
            {
                combobox_index = value;
                Content_colection = content_colection[value];
            }
        }

        public ViewModelBase Content_colection
        {
            get => curent_content;
            set
            {
                this.RaiseAndSetIfChanged(ref curent_content, value);
            }
        }
    }
}