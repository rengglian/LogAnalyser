using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCalibration.Helper
{
    public class ChessBoard
    {
        public static void Find(Mat img)
        {
            var _img = img.Clone();
            var roi = new Rect();
            bool found = Cv2.FindChessboardCorners(img_gray, board_sz, corners, cv::CALIB_CB_ADAPTIVE_THRESH | cv::CALIB_CB_NORMALIZE_IMAGE);
        }
    }
}
