using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace thrown_body
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        GeometryDrawing draw = new GeometryDrawing();
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double v0 = Convert.ToDouble(start_speed.Text);
            double alpha = Convert.ToDouble(angle.Text); ;
            double h0 = Convert.ToDouble(start_height.Text); ;
            double kr = Convert.ToDouble(resistance_k.Text); ;
            double m = Convert.ToDouble(mass.Text);

            const double g = 9.8;
            const double dt = 0.05;
            
            double time = 0;
            double throw_angle = Math.PI / 180 * alpha;

            int point_number = 300;

            double[] x = new double[point_number];
            double[] y = new double[point_number];

            double[] vx = new double[point_number];
            double[] vy = new double[point_number];

            vx[0] = v0 * Math.Cos(throw_angle);
            vy[0] = v0 * Math.Sin(throw_angle);
            x[0] = 0;
            y[0] = h0;
            double f_length=0;
            double y_max = h0;

            for (int i = 1; i < point_number; ++i)
            {
                time += dt;

                vx[i] = vx[i - 1] - (kr / m) * vx[i - 1] * dt;
                vy[i] = vy[i - 1] - (g + kr / m * vy[i - 1]) * dt;

                x[i] = x[i - 1] + vx[i - 1] * dt;
                y[i] = y[i - 1] + vy[i - 1] * dt;

                if(y[i]>y_max) y_max = y[i];

                if (y[i] < 0)
                {
                    f_length = x[i];
                    break;
                }
            }

            fly_time.Text = Math.Round(time, 3).ToString();
            fly_length.Text = Math.Round(f_length, 3).ToString();
            max_height.Text = Math.Round(y_max, 3).ToString();

           

           /*
            Polyline polyline = new Polyline();
            polyline.Points = new PointCollection();
            polyline.Points.Add(new Point(210, 400));
            polyline.Points.Add(new Point(210, 450));
            polyline.Points.Add(new Point(320, 450));
            polyline.Stroke = Brushes.Black;
            */
        }
    }
}
