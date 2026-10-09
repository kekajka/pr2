using System;
using System.Windows;
using Microsoft.Win32;
using System.Data;
using System.Collections.Generic;
using LibMas;
using Lib_14;

namespace Pr2
{
    public partial class MainWindow : Window
    {
        private int[,] matrix;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Заполнить_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(diapazon.Text, out int randMax))
            {
                MessageBox.Show("Диапазон должен быть");
                return;
            }

            if (!int.TryParse(rowCount.Text, out int rows) || rows <= 0)
            {
                MessageBox.Show("Количество строк должно быть положительным числом");
                return;
            }

            if (!int.TryParse(columnCount.Text, out int cols) || cols <= 0)
            {
                MessageBox.Show("Количество столбцов должно быть положительным числом");
                return;
            }

            Massiv.InitMatrix(out matrix, rows, cols, randMax);
            dataGrid.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
        }

        private void Рассчитать_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Матрица пуста");
                return;
            }

            DataView view = (DataView)dataGrid.ItemsSource;
            DataTable table = view.Table;

            matrix = new int[table.Rows.Count, table.Columns.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (table.Rows[i][j] != DBNull.Value)
                    {
                        if (!int.TryParse(table.Rows[i][j].ToString(), out matrix[i, j]))
                        {
                            MessageBox.Show("некорректные данные");
                            return;
                        }
                    }
                }
            }
            int sum = Calculation.GetSumLessThan8(matrix);
            rez.Text = sum.ToString();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (matrix == null)
            {
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true)
            {
                Massiv.SaveMatrix(matrix, save.FileName);
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            if (open.ShowDialog() == true)
            {
                Massiv.LoadMatrix(out matrix, open.FileName);
                rowCount.Text = Convert.ToString(matrix.GetLength(0));
                columnCount.Text = Convert.ToString(matrix.GetLength(1));
                dataGrid.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
            }
        }

        private void MenuClear_Click(object sender, RoutedEventArgs e)
        {
            matrix = null;
            dataGrid.ItemsSource = null;
            rez.Clear();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №2 Вариант 14 Выполнил Клюев");
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}