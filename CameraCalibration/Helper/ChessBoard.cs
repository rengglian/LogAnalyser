using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CameraCalibration.Helper
{
    public class ChessBoard
    {
   
        public static Dictionary<string, double> Find(Mat img, System.Drawing.Point squares, System.Drawing.Point roiCenter, int roiSize, bool export)
        {
            var _img = img.Clone();
            int im_width = _img.Cols / 2;
            int im_height = _img.Rows / 2;
            
            var roi = new Rect(im_width-roiCenter.X - roiSize/2, im_height - roiCenter.Y - roiSize / 2, roiSize, roiSize);

            var _img_crop = new Mat(_img, roi);
            var _img_gray = _img_crop.Clone();
            _img_crop.ConvertTo(_img_gray, -1, 1, 0);
            
            Cv2.CvtColor(_img_gray, _img_gray, ColorConversionCodes.BGR2GRAY);

            int chessboardCornersPerCol = squares.X - 1;
            int chessboardCornersPerRow = squares.Y - 1;
            var board_sz = new Size(chessboardCornersPerRow, chessboardCornersPerCol);
            
            bool found = Cv2.FindChessboardCorners(_img_gray, board_sz, out Point2f[] corners, ChessboardFlags.AdaptiveThresh | ChessboardFlags.NormalizeImage);

            CameraProperties cam = new CameraProperties();

            if (found)
            {
                var termcrit = new TermCriteria(CriteriaType.Eps | CriteriaType.Count, 30, 0.001);
                Point2f[] cornerSubPix = Cv2.CornerSubPix(_img_gray, corners, new Size(11, 11), new Size(-1, -1), termcrit);

                Cv2.DrawChessboardCorners(_img_crop, board_sz, cornerSubPix, found);

                var matCorners = new Mat(rows: chessboardCornersPerRow, cols: chessboardCornersPerCol, type: MatType.CV_32FC2, data: cornerSubPix);

                var pointRow = matCorners.Reduce(ReduceDimension.Row, ReduceTypes.Avg, -1);
                var pointCol = matCorners.Reduce(ReduceDimension.Column, ReduceTypes.Avg, -1);
                var matCenter = pointCol.Reduce(ReduceDimension.Row, ReduceTypes.Avg, -1);

                if(export)
                {
                    _ = PrintMatAsync(matCorners);
                }


                var vecRow = pointRow.At<Point2f>(0,pointRow.Cols - 1) - pointRow.At<Point2f>(0,0);
                var vecCol = pointCol.At<Point2f>(pointCol.Rows - 1, 0) - pointCol.At<Point2f>(0, 0);
                double rotationAngleRadians = Math.Abs(vecRow.X) > Math.Abs(vecCol.X) ? Math.Atan(vecRow.Y / vecRow.X) : Math.Atan(vecCol.Y / vecCol.X);

                cam.Rotation = 180 * rotationAngleRadians / Math.PI;

                float[] arrRow = { vecRow.X, vecRow.Y };
                float[] arrCol = { vecCol.X, vecCol.Y };

                var res_x = 1.0 / Math.Sqrt(Cv2.Norm(InputArray.Create(arrRow)) / (chessboardCornersPerRow - 1) * Cv2.Norm(InputArray.Create(arrRow)) / (chessboardCornersPerRow - 1));
                var res_y = 1.0 / Math.Sqrt(Cv2.Norm(InputArray.Create(arrCol)) / (chessboardCornersPerCol - 1) * Cv2.Norm(InputArray.Create(arrCol)) / (chessboardCornersPerCol - 1));

                cam.Resolution = new System.Windows.Point(res_x, res_y);

                var center = new Point2f(matCenter.At<Point2f>(0).X, matCenter.At<Point2f>(0).Y);

                cam.Center = new System.Windows.Point((center.X - roi.Width / 2)* cam.Resolution.X, (center.Y - roi.Height / 2) * cam.Resolution.Y);

                //CameraCalibration(_img_crop, board_sz, cornerSubPix);

                var roi_img = new Mat(img, roi);
                _img_crop.CopyTo(roi_img);

            }

            Cv2.Rectangle(img, roi, new Scalar(0, 0, 255));

            var dict = new Dictionary<string, double>
            {
                { "X um / px", cam.Resolution.X * 1000 },
                { "Y um / px", cam.Resolution.Y * 1000 },
                { "Rotation [*]", cam.Rotation },
                { "Center X [um]", cam.Center.X * 1000 },
                { "Center Y [um]", cam.Center.Y * 1000 }
            };

            return dict;
        }

        private static async Task PrintMatAsync(Mat mat)
        {
            List<string> cornerList= new List<string>();
            for (var rowIndex = 0; rowIndex < mat.Rows; rowIndex++)
            {
                for (var colIndex = 0; colIndex < mat.Cols; colIndex++)
                {
                    var strToJson = mat.At<Point2f>(rowIndex, colIndex).X.ToString();
                    strToJson += ",";
                    strToJson += mat.At<Point2f>(rowIndex, colIndex).Y.ToString();
                    cornerList.Add(strToJson);
                }
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            using (FileStream fs = File.Create(@".\ChessboardCorners.txt"))
            {
                await JsonSerializer.SerializeAsync(fs, cornerList, options);
            }
        }

        private static void CameraCalibration(Mat _img_crop, Size board_sz, Point2f[] cornerSubPix)
        {
            float cellSize = 0.001f; // in metres

            List<Point3f> chessboard_coords = new List<Point3f>();
            for (int i = 0; i < board_sz.Height; i++)
                for (int j = 0; j < board_sz.Width; j++)
                    chessboard_coords.Add(new Point3f(j, i, 0) * cellSize);
            var objectPoints = new List<IEnumerable<Point3f>> { chessboard_coords };
            var imagePoints = new List<IEnumerable<Point2f>> { cornerSubPix };

            double[,] cameraMatrix = new double[3, 3];
            double[] distCoefficients = new double[5];
            Vec3d[] rvecs, tvecs;

            Cv2.CalibrateCamera(objectPoints, imagePoints, _img_crop.Size(), cameraMatrix, distCoefficients, out rvecs, out tvecs);

            Debug.WriteLine(
                cameraMatrix[0, 0] + ", " + cameraMatrix[0, 1] + ", " + cameraMatrix[0, 2] + "\n" +
                cameraMatrix[1, 0] + ", " + cameraMatrix[1, 1] + ", " + cameraMatrix[1, 2] + "\n" +
                cameraMatrix[2, 0] + ", " + cameraMatrix[2, 1] + ", " + cameraMatrix[2, 2]
            );

            Debug.WriteLine(tvecs[0].Item0 + ", " + tvecs[0].Item1 + ", " + tvecs[0].Item2);
        }
    }
}
