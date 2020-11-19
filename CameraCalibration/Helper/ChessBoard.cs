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
            
            var roi = new Rect(im_width-roiCenter.X - roiSize/2, im_height - roiCenter.Y - roiSize / 2, roiSize, roiSize);

            var _img_crop = new Mat(_img, roi);
            var _img_gray = _img_crop.Clone();
            //_img_crop.ConvertTo(_img_gray, -1, 2, 0);
            
            Cv2.CvtColor(_img_gray, _img_gray, ColorConversionCodes.BGR2GRAY);

            int chessboardCornersPerCol = squares.X - 1;
            int chessboardCornersPerRow = squares.Y - 1;
            int chessboardCornersTotal = chessboardCornersPerCol * chessboardCornersPerRow;
            var board_sz = new Size(chessboardCornersPerRow, chessboardCornersPerCol);
            
            bool found = Cv2.FindChessboardCorners(_img_gray, board_sz, out Point2f[] corners, ChessboardFlags.AdaptiveThresh | ChessboardFlags.NormalizeImage);
            
            if(found)
            {
                var termcrit = new TermCriteria(CriteriaType.Eps | CriteriaType.Count, 30, 0.1);
                Point2f[] cornerSubPix = Cv2.CornerSubPix(_img_gray, corners, new Size(11, 11), new Size(-1, -1), termcrit);

                Cv2.DrawChessboardCorners(_img_crop, board_sz, cornerSubPix, found);

                var matCorners = new Mat(rows: chessboardCornersPerRow, cols: chessboardCornersPerCol, type: MatType.CV_32FC1, data: cornerSubPix);

                var pointRow = matCorners.Reduce(ReduceDimension.Row, ReduceTypes.Avg, -1);
                var pointCol = matCorners.Reduce(ReduceDimension.Column, ReduceTypes.Avg, -1);
                var matCenter = pointCol.Reduce(ReduceDimension.Row, ReduceTypes.Avg, -1);

                var vecRow = pointRow.At<Point2f>(pointRow.Cols - 1) - pointRow.At<Point2f>(0);
                var vecCol = (pointCol.At<Point2f>(pointCol.Rows - 1) - pointCol.At<Point2f>(0));
                var center = new Point2f(matCenter.At<Point2f>(0).X, matCenter.At<Point2f>(0).Y);

                float[] arrRow = { vecRow.X, vecRow.Y };
                float[] arrCol = { vecCol.X, vecCol.Y };

                var rotationAngleRadians = Math.Abs(vecRow.X) > Math.Abs(vecCol.X) ? Math.Atan(vecRow.Y / vecRow.X) : Math.Atan(vecCol.Y / vecCol.X);

                var rotationAngleDegree = 180 * rotationAngleRadians / Math.PI;
                var x_pixel_size_mm = 1.0 / Math.Sqrt(Cv2.Norm(InputArray.Create(arrRow)) / (chessboardCornersPerRow - 1) * Cv2.Norm(InputArray.Create(arrRow)) / (chessboardCornersPerRow - 1));
                var y_pixel_size_mm = 1.0 / Math.Sqrt(Cv2.Norm(InputArray.Create(arrCol)) / (chessboardCornersPerCol - 1) * Cv2.Norm(InputArray.Create(arrCol)) / (chessboardCornersPerCol - 1));

                var roi_img = new Mat(img, roi);
                _img_crop.CopyTo(roi_img);

                Cv2.ImShow("draw", _img_crop);
                Cv2.ImShow("test", img);

            }
        }
    }
}
