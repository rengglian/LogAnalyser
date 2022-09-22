using OpenCvSharp;

namespace CameraCalibration.Helper;

class Crosshair
{
    public static void Draw(Mat img, Point center)
    {
        Point centerImg = new(img.Cols / 2, img.Rows / 2);

        Point h_start = centerImg + new Point(center.X - 50, center.Y);
        Point h_stop = centerImg + new Point(center.X + 50, center.Y);
        Point v_start = centerImg + new Point(center.X, center.Y - 50);
        Point v_stop = centerImg + new Point(center.X, center.Y + 50);
        Point c_start = centerImg + center;
        Cv2.Line(img, h_start, h_stop, new Scalar(0, 0, 255));
        Cv2.Line(img, v_start, v_stop, new Scalar(0, 0, 255));
        Cv2.Circle(img, c_start, 32, new Scalar(0, 255, 0));
    }
}
