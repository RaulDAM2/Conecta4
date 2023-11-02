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

namespace Conecta4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

   
    public partial class MainWindow : Window
    {
        private Button[,] board; // Matriz para representar el tablero.
        private int currentPlayer; // 0 para jugador 1, 1 para jugador 2.
        private List<string> Cells { get; set; } // Colección para almacenar los valores de las celdas

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Define el tamaño del tablero (6 filas y 7 columnas).
            int numRows = 6;
            int numCols = 7;

            Cells = new List<string>(numRows * numCols);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    Cells.Add(""); // Inicializa todas las celdas con contenido vacío.
                }
            }

            // Asocia la colección Cells con el DataContext del control ItemsControl en el XAML.
            gameGrid.DataContext = Cells;
        }

        private void ColumnButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int col = Grid.GetColumn(button); // Obtiene la columna asociada al botón.

            // Encuentra la primera fila vacía en la columna seleccionada.
            int emptyRow = GetEmptyRow(col);

            if (emptyRow != -1)
            {
                // Comprueba el jugador actual (0 o 1) y establece el contenido del botón en consecuencia.
                if (currentPlayer == 0)
                {
                    button.Content = "X"; // Jugador 1
                }
                else
                {
                    button.Content = "O"; // Jugador 2
                }

                // Cambia al siguiente jugador.
                SwitchPlayer();
            }
        }

        private int GetEmptyRow(int col)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (string.IsNullOrEmpty(Cells[row * 7 + col]))
                {
                    return row;
                }
            }
            return -1; // La columna está llena.
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            // Borra el contenido de las celdas y restablece el jugador actual.
            for (int i = 0; i < Cells.Count; i++)
            {
                Cells[i] = "";
            }

            foreach (Button button in gameGrid.Children.OfType<Button>())
            {
                button.Content = "";
            }

            currentPlayer = 0;
        }
        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 0) ? 1 : 0; // Cambia entre el jugador 0 y el jugador 1.
        }


    }
}

