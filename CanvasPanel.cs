using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BpmnEditor.WinForms // Не забудьте заменить на ваше пространство имен!
{
    public partial class CanvasPanel : UserControl
    {
        // Свойства сетки
        public Color GridColor { get; set; } = Color.LightGray;
        public int GridSize { get; set; } = 20;

        // Свойства для перетаскивания
        private bool _isDragging = false;
        private Point _lastLocation;
        private PointF _currentOffset = PointF.Empty;

        public CanvasPanel()
        {
            InitializeComponent();
            // Ключевые настройки для избежания мерцания
            DoubleBuffered = true;
            // Сетка рисуется на белом фоне
            BackColor = Color.White;
        }

        // Метод для отрисовки сетки с учетом смещения
        protected override void OnPaint(PaintEventArgs e)
{
    base.OnPaint(e);

    // Применяем текущее смещение
    e.Graphics.TranslateTransform(_currentOffset.X, _currentOffset.Y);

    using (Pen gridPen = new Pen(GridColor))
    {
        // 1. НАХОДИМ КРАЙНИЕ ТОЧКИ ВИДИМОЙ ОБЛАСТИ в МИРОВЫХ КООРДИНАТАХ (с учетом смещения)
        // Левая граница видимой области в мировых координатах
        float visibleLeft = -_currentOffset.X;
        // Верхняя граница видимой области в мировых координатах  
        float visibleTop = -_currentOffset.Y;
        // Правая граница видимой области в мировых координатах
        float visibleRight = visibleLeft + Width;
        // Нижняя граница видимой области в мировых координатах
        float visibleBottom = visibleTop + Height;

        // 2. ОПРЕДЕЛЯЕМ ПЕРВУЮ ЛИНИЮ СЕТКИ, которую нужно нарисовать
        // Находим первую вертикальную линию слева от видимой области
        float firstVerticalLine = (float)(Math.Floor(visibleLeft / GridSize) * GridSize);
        // Находим первую горизонтальную линию сверху от видимой области
        float firstHorizontalLine = (float)(Math.Floor(visibleTop / GridSize) * GridSize);

        // 3. РАССЧИТЫВАЕМ, СКОЛЬКО ЛИНИЙ нужно нарисовать
        // Количество вертикальных линий = ширина_области_видимости / шаг_сетки + 1
        int verticalLineCount = (int)Math.Ceiling((visibleRight - firstVerticalLine) / GridSize);
        // Количество горизонтальных линий = высота_области_видимости / шаг_сетки + 1
        int horizontalLineCount = (int)Math.Ceiling((visibleBottom - firstHorizontalLine) / GridSize);

        // 4. РИСУЕМ ВЕРТИКАЛЬНЫЕ ЛИНИИ (сверху вниз через всю видимую область)
        for (int i = 0; i < verticalLineCount; i++)
        {
            float x = firstVerticalLine + (i * GridSize);
            e.Graphics.DrawLine(gridPen, x, visibleTop, x, visibleBottom);
        }

        // 5. РИСУЕМ ГОРИЗОНТАЛЬНЫЕ ЛИНИИ (слева направо через всю видимую область)
        for (int j = 0; j < horizontalLineCount; j++)
        {
            float y = firstHorizontalLine + (j * GridSize);
            e.Graphics.DrawLine(gridPen, visibleLeft, y, visibleRight, y);
        }
    }
}

        // Обработка событий мыши для навигации
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {
                _isDragging = true;
                _lastLocation = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging)
            {
                int deltaX = e.X - _lastLocation.X;
                int deltaY = e.Y - _lastLocation.Y;

                _currentOffset.X += deltaX;
                _currentOffset.Y += deltaY;

                _lastLocation = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isDragging = false;
            Cursor = Cursors.Default;
        }
    }
}