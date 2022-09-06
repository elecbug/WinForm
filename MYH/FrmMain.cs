using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYH
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private int[] intData = new int[26];
        private int intClick = 0;
        private bool bolIndex = false;

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private void ImageSwaper(ref Button btn1, ref Button btn2)
        {
            Image image = btn1.Image;
            btn1.Image = btn2.Image;
            btn2.Image = image;
        }

        private void ButtonClicker(int i, Button btn1)
        {
            Button btn2 = new Button();

            if (!(btn1 == button1 || btn1 == button2 || btn1 == button3 || btn1 == button4 || btn1 == button5))
            {
                btn2 = Btn2ReturnerUp(btn1);
                if (intData[i - 5] == 0)
                {
                    Swap(ref intData[i], ref intData[i - 5]);
                    ImageSwaper(ref btn1, ref btn2);
                }
            }
            if (!(btn1 == button1 || btn1 == button6 || btn1 == button11 || btn1 == button16 || btn1 == button21))
            {
                btn2 = Btn2ReturnerLeft(btn1);
                if (intData[i - 1] == 0)
                {
                    Swap(ref intData[i], ref intData[i - 1]);
                    ImageSwaper(ref btn1, ref btn2);
                }
            }
            if (!(btn1 == button5 || btn1 == button10 || btn1 == button15 || btn1 == button20 || btn1 == button25))
            {
                btn2 = Btn2ReturnerRight(btn1);
                if (intData[i + 1] == 0)
                {
                    Swap(ref intData[i], ref intData[i + 1]);
                    ImageSwaper(ref btn1, ref btn2);
                }
            }
            if (!(btn1 == button21 || btn1 == button22 || btn1 == button23 || btn1 == button24 || btn1 == button25))
            {
                btn2 = Btn2ReturnerDown(btn1);
                if (intData[i + 5] == 0)
                {
                    Swap(ref intData[i], ref intData[i + 5]);
                    ImageSwaper(ref btn1, ref btn2);
                }
            }
            if (bolIndex)
            {
                IndexMaker();
            }
            if (intClick > 0)
            {
                ClearCheaker();
            }
        }

        private Button Btn2ReturnerUp(Button btn1)
        {
            for (int i = 1; i <= 25; i++)
            {
                if (btn1.Name == $"button{i}")
                {
                    return (Button)this.Controls.Find($"button{i - 5}", true)[0];
                } 
            }
            return new Button();
        }

        private Button Btn2ReturnerLeft(Button btn1)
        {
            for (int i = 1; i <= 25; i++)
            {
                if (btn1.Name == $"button{i}")
                {
                    return (Button)this.Controls.Find($"button{i - 1}", true)[0];
                }
            }
            return new Button();
        }

        private Button Btn2ReturnerRight(Button btn1)
        {
            for (int i = 1; i <= 25; i++)
            {
                if (btn1.Name == $"button{i}")
                {
                    return (Button)this.Controls.Find($"button{i + 1}", true)[0];
                }
            }
            return new Button();
        }

        private Button Btn2ReturnerDown(Button btn1)
        {
            for (int i = 1; i <= 25; i++)
            {
                if (btn1.Name == $"button{i}")
                {
                    return (Button)this.Controls.Find($"button{i + 5}", true)[0];
                }
            }
            return new Button();
        }

        private void ButtonImageSetter()
        {
            button1.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_001;
            button2.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_002;
            button3.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_003;
            button4.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_004;
            button5.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_005;
            button6.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_006;
            button7.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_007;
            button8.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_008;
            button9.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_009;
            button10.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_010;
            button11.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_011;
            button12.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_012;
            button13.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_013;
            button14.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_014;
            button15.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_015;
            button16.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_016;
            button17.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_017;
            button18.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_018;
            button19.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_019;
            button20.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_020;
            button21.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_021;
            button22.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_022;
            button23.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_023;
            button24.Image = Properties.Resources.artworks_CtLYWZyAvh6GooAy_ucLd3g_t500x500_024;
            button25.Image = null;
        }

        private void RandomClicker()
        {
            for (int k = 0; k < 100; k++)
            {
                for (int i = 1; i <= 25; i++)
                {
                    FrmMain frmMain = new FrmMain();
                    Random random = new Random();
                    Type type = frmMain.GetType();

                    if (intData[i] == 0)
                    {
                    Re:
                        int j = random.Next(1, 5);

                        switch (j)
                        {
                            case 1:
                                j = i - 5;
                                break;
                            case 2:
                                j = i - 1;
                                break;
                            case 3:
                                j = i + 1;
                                break;
                            case 4:
                                j = i + 5;
                                break;
                            default:
                                MessageBox.Show("Error");
                                break;
                        }
                        try
                        {
                            ButtonClicker(j, (Button)this.Controls.Find($"button{j}", true)[0]);
                        }
                        catch 
                        { 
                            goto Re;
                        }
                    }
                }
            }
        }

        private void ClearCheaker()
        {
            bool bolChaeker = false;
            for (int i = 1; i < 25; i++)
            {
                if (intData[i] == i)
                {
                    bolChaeker = true;
                }
                else
                {
                    bolChaeker = false;
                    break;
                }
            }
            if (bolChaeker && intData[25] == 0)
            {
                MessageBox.Show($"Game Claer! ({intClick + 1} Click)");
            }
        }

        private void IndexMaker()
        {
            for (int i = 1; i <= 25; i++)
            {
                ((Button)this.Controls.Find($"button{i}", true)[0]).Text = $"{intData[i]}";
                if (intData[i] == 0)
                {
                    ((Button)this.Controls.Find($"button{i}", true)[0]).Text = $"";
                }
            }
        }
        
        private void IndexCleaner()
        {
            for (int i = 1; i <= 25; i++)
            {
                ((Button)this.Controls.Find($"button{i}", true)[0]).Text = $"";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicker(1, button1);
            intClick++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicker(2, button2);
            intClick++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonClicker(3, button3);
            intClick++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ButtonClicker(4, button4); 
            intClick++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ButtonClicker(5, button5);
            intClick++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ButtonClicker(6, button6);
            intClick++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ButtonClicker(7, button7);
            intClick++;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ButtonClicker(8, button8);
            intClick++;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ButtonClicker(9, button9);
            intClick++;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ButtonClicker(10, button10);
            intClick++;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ButtonClicker(11, button11);
            intClick++;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ButtonClicker(12, button12);
            intClick++;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ButtonClicker(13, button13);
            intClick++;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ButtonClicker(14, button14);
            intClick++;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ButtonClicker(15, button15);
            intClick++;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ButtonClicker(16, button16);
            intClick++;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ButtonClicker(17, button17);
            intClick++;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ButtonClicker(18, button18);
            intClick++;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ButtonClicker(19, button19);
            intClick++;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ButtonClicker(20, button20);
            intClick++;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ButtonClicker(21, button21);
            intClick++;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ButtonClicker(22, button22);
            intClick++;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ButtonClicker(23, button23);
            intClick++;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ButtonClicker(24, button24);
            intClick++;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ButtonClicker(25, button25);
            intClick++;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 25; i++)
            {
                intData[i] = i;
            }
            intData[25] = 0;

            ButtonImageSetter();
            RandomClicker();
        }

        private void BtnIndex_Click(object sender, EventArgs e)
        {
            if (!bolIndex)
            {
                bolIndex = true;
                IndexMaker();
            }
            else if(bolIndex)
            {
                bolIndex = false;
                IndexCleaner();
            }
        }
    }
}
