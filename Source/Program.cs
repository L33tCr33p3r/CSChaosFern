using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace CSChaosFern.Source {
    internal static class Program {
        const int WIDTH = 640 * 2;
        const int HEIGHT = 480 * 2;
        const float SCALE = 50;


        private static Random rand { get; set; } = new Random();

        static Vector2f NextCoord(Vector2f p) {
            double region = rand.NextDouble();

            Vector2f output = new Vector2f();

            if (region < 0.01) {
                output.X = 0.0f;
                output.Y = p.Y * 0.16f;
            } else if (region < 0.86) {
                output.X = 0.85f * p.X + 0.04f * p.X;
                output.Y = -0.05f * p.X + 0.85f * p.Y + 1.6f;
            } else if (region < 0.93) {
                output.X = 0.20f * p.X - 0.26f * p.Y;
                output.Y = 0.23f * p.X + 0.22f * p.Y + 1.6f;
            } else {
                output.X = -0.15f * p.X + 0.28f * p.Y;
                output.Y = 0.26f * p.X + 0.24f * p.Y + 0.44f;
            }
            return output;
        }

        static Vector2f ViewTransform(Vector2f point, Vector2f offset, float scale = 50) {
            return new Vector2f( 
                (scale * point.X) + (WIDTH / 2) + offset.X, 
                HEIGHT - (scale * point.Y) + offset.Y
            );
        }

        static public void Main() {
            RenderWindow window = new RenderWindow(new VideoMode(WIDTH, HEIGHT), "CSChaosFern");
            window.Closed += new EventHandler(OnClose);

            Vector2f OFFSET = -NextCoord(new Vector2f(0, 0));
            
            Vector2f point = new Vector2f(0, 0);
            Vector2f drawPoint;

            RectangleShape pixel = new RectangleShape {
				Size = new Vector2f(1, 1),
				FillColor = new Color(0x44, 0xaa, 0x22),
			};

            while (window.IsOpen) {
                window.DispatchEvents();
                
                for (int i = 0; i < 100000; i++) {
                    point = NextCoord(point);
                    drawPoint = ViewTransform(point, OFFSET, SCALE);
                    pixel.Position = drawPoint;
                    window.Draw(pixel);
                }
                
                window.Display();
            }
        }
        
        static void OnClose(object? Sender, EventArgs e) {
            if (Sender != null) {
                RenderWindow Window = (RenderWindow)Sender;
			    Window.Close();
            }
		}
    }
}