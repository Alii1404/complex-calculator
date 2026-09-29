using System;
using System.Drawing;
using System.Windows.Forms;

namespace ders4
{
    public partial class Form1 : Form
    {
        TextBox ekran;
        ListBox tarixce;

        double ilkEded = 0;
        string emeliyyat = "";
        bool yeniEded = true;

        public Form1()
        {
            InitializeComponent();

            Text = "Kalkulyator";
            Size = new Size(700, 570);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // EKRAN
            ekran = new TextBox();

            ekran.Location = new Point(20, 20);
            ekran.Size = new Size(370, 60);
            ekran.Font = new Font("Arial", 24);
            ekran.Text = "0";
            ekran.TextAlign = HorizontalAlignment.Right;
            ekran.ReadOnly = true;

            Controls.Add(ekran);

            // TARİXÇƏ BAŞLIĞI
            Label basliq = new Label();

            basliq.Text = "TARİXÇƏ";
            basliq.Font = new Font(
                "Arial",
                16,
                FontStyle.Bold
            );

            basliq.Location = new Point(430, 25);
            basliq.Size = new Size(200, 30);

            Controls.Add(basliq);

            // TARİXÇƏ
            tarixce = new ListBox();

            tarixce.Location = new Point(430, 65);
            tarixce.Size = new Size(230, 390);
            tarixce.Font = new Font(
                "Arial",
                12
            );

            Controls.Add(tarixce);

            // DÜYMƏLƏR
            string[,] duymeler =
            {
                { "7", "8", "9", "÷", "DEL" },
                { "4", "5", "6", "×", "√" },
                { "1", "2", "3", "-", "x²" },
                { ".", "0", "=", "+", "C" }
            };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button button = new Button();

                    button.Text = duymeler[i, j];

                    button.Size = new Size(70, 70);

                    button.Location = new Point(
                        20 + j * 75,
                        100 + i * 75
                    );

                    button.Font = new Font(
                        "Arial",
                        14,
                        FontStyle.Bold
                    );

                    // RƏNGLƏR
                    if (button.Text == "=")
                        button.BackColor =
                            Color.LightGreen;

                    if (button.Text == "C")
                        button.BackColor =
                            Color.LightCoral;

                    if (button.Text == "DEL")
                        button.BackColor =
                            Color.LightYellow;

                    if (button.Text == "√" ||
                        button.Text == "x²")
                    {
                        button.BackColor =
                            Color.LightBlue;
                    }

                    button.Click += Button_Click;

                    Controls.Add(button);
                }
            }

            // TARİXÇƏNİ SİL
            Button tarixceSil = new Button();

            tarixceSil.Text = "Tarixçəni sil";
            tarixceSil.Font = new Font(
                "Arial",
                11,
                FontStyle.Bold
            );

            tarixceSil.Size = new Size(230, 45);
            tarixceSil.Location = new Point(
                430,
                465
            );

            tarixceSil.Click += TarixceSil_Click;

            Controls.Add(tarixceSil);
        }

        private void Button_Click(
            object sender,
            EventArgs e)
        {
            Button button = (Button)sender;

            string text = button.Text;

            // RƏQƏMLƏR
            if (text.Length == 1 &&
                text[0] >= '0' &&
                text[0] <= '9')
            {
                if (yeniEded ||
                    ekran.Text == "0")
                {
                    ekran.Text = text;
                    yeniEded = false;
                }
                else
                {
                    ekran.Text += text;
                }

                return;
            }

            // NÖQTƏ
            if (text == ".")
            {
                if (!ekran.Text.Contains("."))
                {
                    ekran.Text += ".";
                    yeniEded = false;
                }

                return;
            }

            // C
            if (text == "C")
            {
                ekran.Text = "0";

                ilkEded = 0;
                emeliyyat = "";
                yeniEded = true;

                return;
            }

            // DEL
            if (text == "DEL")
            {
                if (ekran.Text.Length > 1)
                {
                    ekran.Text =
                        ekran.Text.Substring(
                            0,
                            ekran.Text.Length - 1
                        );
                }
                else
                {
                    ekran.Text = "0";
                    yeniEded = true;
                }

                return;
            }

            double eded;

            if (!double.TryParse(
                ekran.Text,
                out eded))
            {
                return;
            }

            // KÖK
            if (text == "√")
            {
                if (eded < 0)
                {
                    ekran.Text = "Error";
                    yeniEded = true;
                    return;
                }

                double cavab =
                    Math.Sqrt(eded);

                ekran.Text =
                    cavab.ToString();

                // TARİXÇƏ
                tarixce.Items.Insert(
                    0,
                    "√" + eded + " = " + cavab
                );

                yeniEded = true;

                return;
            }

            // KVADRAT
            if (text == "x²")
            {
                double cavab =
                    eded * eded;

                ekran.Text =
                    cavab.ToString();

                // TARİXÇƏ
                tarixce.Items.Insert(
                    0,
                    eded + "² = " + cavab
                );

                yeniEded = true;

                return;
            }

            // ƏMƏLİYYAT
            if (text == "+" ||
                text == "-" ||
                text == "×" ||
                text == "÷")
            {
                ilkEded = eded;

                emeliyyat = text;

                yeniEded = true;

                return;
            }

            // =
            if (text == "=")
            {
                double ikinciEded;

                if (!double.TryParse(
                    ekran.Text,
                    out ikinciEded))
                {
                    return;
                }

                double cavab = 0;

                if (emeliyyat == "+")
                {
                    cavab =
                        ilkEded + ikinciEded;
                }

                else if (emeliyyat == "-")
                {
                    cavab =
                        ilkEded - ikinciEded;
                }

                else if (emeliyyat == "×")
                {
                    cavab =
                        ilkEded * ikinciEded;
                }

                else if (emeliyyat == "÷")
                {
                    if (ikinciEded == 0)
                    {
                        ekran.Text = "Error";
                        yeniEded = true;
                        return;
                    }

                    cavab =
                        ilkEded / ikinciEded;
                }

                // EKRANA NƏTİCƏ
                ekran.Text =
                    cavab.ToString();

                // TARİXÇƏYƏ YAZ
                string hesab =
                    ilkEded + " " +
                    emeliyyat + " " +
                    ikinciEded +
                    " = " + cavab;

                tarixce.Items.Insert(
                    0,
                    hesab
                );

                emeliyyat = "";
                yeniEded = true;
            }
        }

        private void TarixceSil_Click(
            object sender,
            EventArgs e)
        {
            tarixce.Items.Clear();
        }
    }
}
