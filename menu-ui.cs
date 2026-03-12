using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Street_Fighter_Demo_MOO_ICT
{
    public partial class Form1 : Form
    {
        // ── Menu Assets ──────────────────────────────────────────────
        Image menuBackground = null!;
        Image menuCursor     = null!;
        Image logoSFII       = null!;
        Image[] menuButtons  = new Image[3];

        // ── Menu State ───────────────────────────────────────────────
        bool menuActive    = true;
        int  menuSelection = 0;

        // ── Button Layout ────────────────────────────────────────────
        int   btnWidth  = 200;
        int   btnHeight = 40;
        Point logoPosition;
        Point[] buttonPositions = new Point[3];

        // ─────────────────────────────────────────────────────────────
        //  Initialisation
        // ─────────────────────────────────────────────────────────────

        private void SetUpMenu()
        {
            menuBackground = Image.FromFile("menu/BackgroundPhoto.png");
            menuCursor     = Image.FromFile("menu/NaveOption.png");

            menuButtons[0] = ResizeImage(Image.FromFile("menu/StartButton.png"),  200, 40);
            menuButtons[1] = ResizeImage(Image.FromFile("menu/OptionButton.png"), 200, 40);
            menuButtons[2] = ResizeImage(Image.FromFile("menu/QuitButton.png"),   200, 40);

            int centerX = (this.ClientSize.Width / 2) - (btnWidth / 2);
            int startY  = 350;
            int spacing = 10;

            for (int i = 0; i < 3; i++)
            {
                buttonPositions[i] = new Point(centerX, startY + (i * (btnHeight + spacing)));
            }
        }

        // ─────────────────────────────────────────────────────────────
        //  Input
        // ─────────────────────────────────────────────────────────────

        private void HandleMenuInput(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                menuSelection = (menuSelection > 0) ? menuSelection - 1 : 2;
            }

            if (e.KeyCode == Keys.Down)
            {
                menuSelection = (menuSelection < 2) ? menuSelection + 1 : 0;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (menuSelection == 0) menuActive = false;   // Start
                if (menuSelection == 1) { /* Options – not yet implemented */ }
                if (menuSelection == 2) Application.Exit();   // Quit
            }

            this.Invalidate();
        }

        // ─────────────────────────────────────────────────────────────
        //  Rendering
        // ─────────────────────────────────────────────────────────────

        private void DrawMenu(Graphics g)
        {
            g.DrawImage(menuBackground, 0, 0, this.Width, this.Height);

            for (int i = 0; i < 3; i++)
            {
                g.DrawImage(menuButtons[i], buttonPositions[i]);
            }

            int cursorX = buttonPositions[menuSelection].X - 40;
            int cursorY = buttonPositions[menuSelection].Y - 2;
            g.DrawImage(menuCursor, cursorX, cursorY);
        }

        // ─────────────────────────────────────────────────────────────
        //  Helpers
        // ─────────────────────────────────────────────────────────────

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
    }
}
