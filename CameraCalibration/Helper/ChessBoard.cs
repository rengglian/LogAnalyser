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
   
        public static void Find(Mat img, System.Drawing.Point squares, System.Drawing.Point roiCenter, int roiSize)
        {
            var _img = img.Clone();
            int im_width = _img.Cols / 2;
            int im_height = _img.Rows / 2;
            
            var roi = new Rect(im_width-roiCenter.X - roiSize/2, im_width - roiCenter.Y - roiSize / 2, roiSize, roiSize);

            var _img_crop = new Mat(_img, roi);
            Cv2.CvtColor(_img_crop, _img_crop, ColorConversionCodes.BGR2GRAY);

            int chessboardCornersPerCol = squares.X - 1;
            int chessboardCornersPerRow = squares.Y - 1;
            int chessboardCornersTotal = chessboardCornersPerCol * chessboardCornersPerRow;
            var board_sz = new Size(chessboardCornersPerRow, chessboardCornersPerCol);
            
            bool found = Cv2.FindChessboardCorners(_img_crop, board_sz, out Point2f[] corners, ChessboardFlags.AdaptiveThresh | ChessboardFlags.NormalizeImage);
            
            if(found)
            {
                var termcrit = new TermCriteria(CriteriaType.Eps | CriteriaType.Count, 30, 0.1);
                Point2f[] cornerSubPix1 = _img_crop.CornerSubPix(corners, new Size(11, 11), new Size(-1, -1), termcrit);
                Point2f[] cornerSubPix2 = Cv2.CornerSubPix(_img_crop, corners, new Size(11, 11), new Size(-1, -1), termcrit);
                var matCorners = new Mat(corners);

            }
        }
    }
}
