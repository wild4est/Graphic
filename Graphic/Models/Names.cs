namespace Graphic.Models
{
    public class Names
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Points { get; set; }
        public double Thickes { get; set; }
        public string Color_stroke { get; set; }
        public string Color_fill { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        //public Names() { }

        public Names(string nam, string type, string start, string end, double thic, string stroke) // line
        {
            Name = nam;
            StartPoint = start;
            EndPoint = end;
            Thickes = thic;
            Color_stroke = stroke;
            Type = type;
        }

        public Names(string nam, string type, string point, double thic, string stroke) // polyline
        {
            Name = nam;
            Points = point;
            Thickes = thic;
            Color_stroke = stroke;
            Type = type;
        }

        public Names(string name, string type, string stroke, string fill, double strokeThickness, double width, double height) // rectangle, elipse
        {
            Name = name;
            Color_stroke = stroke;
            Color_fill = fill;
            Thickes = strokeThickness;
            Width = width;
            Height = height;
            Type = type;
        }

        public Names(string name, string type, string stroke, string fill, string temp_all_point, double strokeThickness) // polygon, path
        {
            Name = name;
            Color_stroke = stroke;
            Color_fill = fill;
            Points = temp_all_point;
            Thickes = strokeThickness;
            Type = type;
        }
    }
}
