﻿using System;
using DbModel.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Location.TModel.Tools;
using IModel.Tools;
using IModel;

namespace DbModel.Location.AreaAndDev
{
    /// <summary>
    ///     边界信息 地图和区域
    /// </summary>
    [DataContract]
    public class Bound
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DataMember]
        [Display(Name = "主键Id")]
        public int Id { get; set; }

        /// <summary>
        /// 最小X值
        /// </summary>
        [DataMember]
        [Display(Name = "最小X值")]
        public float MinX { get; set; }

        /// <summary>
        /// 最大X值
        /// </summary>
        [DataMember]
        [Display(Name = "最大X值")]
        public float MaxX { get; set; }

        /// <summary>
        /// 最小Y值
        /// </summary>
        [DataMember]
        [Display(Name = "最小Y值")]
        public float MinY { get; set; }

        /// <summary>
        /// 最大Y值
        /// </summary>
        [DataMember]
        [Display(Name = "最大Y值")]
        public float MaxY { get; set; }

        /// <summary>
        /// 最小Z值
        /// </summary>
        [DataMember]
        [Display(Name = "最小Z值")]
        public float MinZ { get; set; }

        /// <summary>
        /// 最大Z值
        /// </summary>
        [DataMember]
        [Display(Name = "最大Z值")]
        public float MaxZ { get; set; }

        /// <summary>
        /// 是否长方形
        /// </summary>
        [DataMember]
        [Display(Name = "是否长方形")]
        public bool IsRectangle { get; set; }

        /// <summary>
        /// 是否相对坐标
        /// </summary>
        [DataMember]
        [Display(Name = "是否相对坐标")]
        public bool IsRelative { get; set; }

        /// <summary>
        /// 位置点
        /// </summary>
        [DataMember]
        [Display(Name = "位置点")]
        public List<Point> Points { get; set; }

        public Point GetCenter()
        {
            double x = 0;
            double y = 0;
            double z = 0;
            if (Points != null)
            {
                foreach (Point point in Points)
                {
                    x += point.X;
                    y += point.Y;
                    z += point.Z;
                }
                x /= Points.Count;
                y /= Points.Count;
                z /= Points.Count;
            }
            return new Point(x, y, z, 0);
        }

        //[DataMember]
        //public int ParentId { get; set; }

        public Bound()
        {
            MaxZ = 1;
            IsRelative = true;
        }

        public Bound(float x1, float y1, float x2, float y2, float bottomHeightT, float thicknessT, bool isRelative) : this()
        {
            SetInitBound(x1, y1, x2, y2, bottomHeightT, thicknessT);
            IsRectangle = true;
            IsRelative = isRelative;
        }

        public Bound(Point[] points, float bottomHeightT, float thicknessT, bool isRelative) : this()
        {
            SetInitBound(points, bottomHeightT, thicknessT);
            IsRectangle = false;
            IsRelative = isRelative;
        }

        /// <summary>
        /// 用两点(对角点)初始化区域范围
        /// </summary>
        public void SetInitBound(float x1, float y1, float x2, float y2, float bottomHeightT, float thicknessT)
        {
            MinX = float.MaxValue;
            MinY = float.MaxValue;
            MaxX = float.MinValue;
            MaxY = float.MinValue;
            MinZ = 0 + bottomHeightT;
            MaxZ = thicknessT + bottomHeightT;

            if (x1 < MinX)
            {
                MinX = x1;
            }
            if (x2 < MinX)
            {
                MinX = x2;
            }

            if (y1 < MinY)
            {
                MinY = y1;
            }
            if (y2 < MinY)
            {
                MinY = y2;
            }

            if (x1 > MaxX)
            {
                MaxX = x1;
            }
            if (x2 > MaxX)
            {
                MaxX = x2;
            }


            if (y1 > MaxY)
            {
                MaxY = y1;
            }
            if (y2 > MaxY)
            {
                MaxY = y2;
            }

            //double pX = (MinX + MaxX)/2.0;
            //double pY = (MinY + MaxY)/2.0;
            //double pZ = (MinZ + MaxZ)/2.0;
            Points = new List<Point>();
            Points.Add(new Point(MinX, MinY, 0));
            Points.Add(new Point(MaxX, MinY, 1));
            Points.Add(new Point(MaxX, MaxY, 2));
            Points.Add(new Point(MinX, MaxY, 3));
            //Points.Add(new Point(MinX - MinX, MinY - MinY, 0));
            //Points.Add(new Point(MaxX - MinX, MinY - MinY, 1));
            //Points.Add(new Point(MaxX - MinX, MaxY - MinY, 2));
            //Points.Add(new Point(MinX - MinX, MaxY - MinY, 3));
        }

        /// <summary>
        /// 用两点(对角点)初始化区域范围
        /// </summary>
        public void SetInitBound(Point[] points, float bottomHeightT, float thicknessT)
        {
            Init(bottomHeightT, thicknessT);

            SetInitBound(points);

            //double pX = (MinX + MaxX)/2.0;
            //double pY = (MinY + MaxY)/2.0;
            //double pZ = (MinZ + MaxZ)/2.0;
        }

        private void Init(float bottomHeightT, float thicknessT)
        {
            Points = new List<Point>();

            MinX = float.MaxValue;
            MinY = float.MaxValue;
            MaxX = float.MinValue;
            MaxY = float.MinValue;
            MinZ = 0 + bottomHeightT;
            MaxZ = thicknessT + bottomHeightT;
        }

        public void SetInitBound(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                point.Index = i;
                if (point.X < MinX)
                {
                    MinX = point.X;
                }
                if (point.Y < MinY)
                {
                    MinY = point.Y;
                }
                if (point.X > MaxX)
                {
                    MaxX = point.X;
                }
                if (point.Y > MaxY)
                {
                    MaxY = point.Y;
                }
            }

            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                //point.X -= MinX;
                //point.Y -= MinY;
                Points.Add(new Point(point));
            }
        }

        private List<Bound> childrenBounds = new List<Bound>();

        public void Combine(Bound bound)
        {
            if (bound == null) return;
            if (Points == null)
            {
                Init(0, 0);
            }
            childrenBounds.Add(bound);
            SetInitBound(bound.GetPoints2D().ToArray());
        }

        public void AddPoint(Point point)
        {
            if (Points == null)
            {
                Points=new List<Point>();
            }
            Points.Add(point);
        }

        public List<Point> GetPoints2D()
        {
            List<Point> points = new List<Point>();
            if (Points != null && Points.Count > 0)
            {
                points.AddRange(Points);
            }
            else
            {
                points.Add(new Point(MinX, MinY,0,0));
                points.Add(new Point(MinX, MaxY, 0, 1));
                points.Add(new Point(MaxX, MaxY, 0, 2));
                points.Add(new Point(MaxY, MinY, 0, 3));
            }
            return points;
        }

        public List<Point> GetPointsByPointList(List<Point> pointlist)
        {
            List<Point> points = pointlist.FindAll(p => p.BoundId == Id);
            if (points == null)
            {
                points = new List<Point>();
                points.Add(new Point(MinX, MinY, 0, 0));
                points.Add(new Point(MinX, MaxY, 0, 1));
                points.Add(new Point(MaxX, MaxY, 0, 2));
                points.Add(new Point(MaxY, MinY, 0, 3));
            }
            
            return points;
        }

        public Bound Clone()
        {
            Bound copy = new Bound();
            copy = this.CloneObjectByBinary();
            return copy;
        }

        public double GetWidth()
        {
            return MaxX - MinX;
        }

        public double GetLength()
        {
            return MaxY - MinY;
        }

        public double GetHeight()
        {
            return MaxZ - MinZ;
        }

        public bool Contains(double x, double y)
        {
            if (Points != null && Points.Count > 4)//不规则多边形
            {
                return MathTool.IsInRegion(new Point(x, y, 0, 0), Points.ConvertAll<IVector2>(i=>i));
            }
            return x >= MinX && x <= MaxX && y >= MinY && y <= MaxY;
        }

        public bool ContainsSimple(double x, double y)
        {
            return x >= MinX && x <= MaxX && y >= MinY && y <= MaxY;
        }


        public Point GetLeftBottomPoint()
        {
            //Point leftBottom = null;
            //var minLenth = double.MaxValue;
            //foreach (var point in GetPoints2D())
            //{
            //    var length = (point.X - MinX)*(point.X - MinX) + (point.Y - MinY)*(point.Y - MinY);
            //    if (length < minLenth)
            //    {
            //        minLenth = length;
            //        leftBottom = point;
            //    }
            //}
            //return leftBottom;

            return GetClosePoint(MinX, MinY);
        }

        /// <summary>
        /// 得到最近的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public Point GetClosePoint(double x, double y)
        {
            Point closePoint = null;
            var minLenth = double.MaxValue;
            foreach (var point in GetPoints2D())
            {
                var length = (point.X - x) * (point.X - x) + (point.Y - y) * (point.Y - y);
                if (length < minLenth)
                {
                    minLenth = length;
                    closePoint = point;
                }
            }
            return closePoint;
        }
    }
}