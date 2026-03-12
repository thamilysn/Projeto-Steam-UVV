using System.Drawing;
using System.Windows.Forms;

namespace Street_Fighter_Demo_MOO_ICT
{
    public partial class Form1 : Form
    {
        // --- VARIÁVEIS DO MENU ---
        Image menuBackground = null!;
        Image menuCursor = null!;
        Image[] menuButtons = new Image[3];

        bool menuActive = true;
        int menuSelection = 0;
        int btnWidth = 200;
        int btnHeight = 40;

        Point[] buttonPositions = new Point[3];

        // --- MÉTODOS DE INICIALIZAÇÃO E CONFIGURAÇÃO ---
        private void SetUpMenu()
        {
            // Carregamento de Ativos
            menuBackground = Image.FromFile("menu/BackgroundPhoto.png");
            menuCursor = Image.FromFile("menu/NaveOption.png");

            menuButtons[0] = ResizeImage(Image.FromFile("menu/StartButton.png"), 200, 40);
            menuButtons[1] = ResizeImage(Image.FromFile("menu/OptionButton.png"), 200, 40);
            menuButtons[2] = ResizeImage(Image.FromFile("menu/QuitButton.png"), 200, 40);

            // Cálculos de Posicionamento Centralizado
            int centerX = (this.ClientSize.Width / 2) - (btnWidth / 2);
            int startY = 350;
            int spacing = 10;

            for (int i = 0; i < 3; i++)
            {
                buttonPositions[i] = new Point(centerX, startY + (i * (btnHeight + spacing)));
            }
        }

        // Função utilitária usada pelo menu para padronizar os botões
        public Image ResizeImage(Image img, int width, int height)
        {
            if (img == null) return new Bitmap(width, height);

            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return b;
        }

        // --- LÓGICA DE ENTRADA E RENDERIZAÇÃO ---

        private void HandleMenuInput(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                // Navega para cima ou volta para o fim da lista
                menuSelection = (menuSelection > 0) ? menuSelection - 1 : 2;
            }
            if (e.KeyCode == Keys.Down)
            {
                // Navega para baixo ou volta para o início da lista
                menuSelection = (menuSelection < 2) ? menuSelection + 1 : 0;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (menuSelection == 0) menuActive = false; // Inicia o jogo
                if (menuSelection == 1) { /* Opções - Futuro */ }
                if (menuSelection == 2) Application.Exit(); // Sai do jogo
            }
            this.Invalidate(); // Força o Redraw para atualizar a posição do cursor
        }

        private void DrawMenu(Graphics g)
        {
            // Desenha o fundo do menu esticado para o tamanho da janela
            g.DrawImage(menuBackground, 0, 0, this.Width, this.Height);

            // Desenha os três botões (Start, Option, Quit)
            for (int i = 0; i < 3; i++)
            {
                g.DrawImage(menuButtons[i], buttonPositions[i]);
            }

            // Desenha o cursor (Nave) alinhado ao botão selecionado
            int cursorX = buttonPositions[menuSelection].X - 40;
            int cursorY = buttonPositions[menuSelection].Y - 2;
            g.DrawImage(menuCursor, cursorX, cursorY);
        }
    }
}
