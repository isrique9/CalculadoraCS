using System;
using System.Windows.Forms;

public class CalculadoraGui : Form
{
    private TextBox display;
    private Button[] buttons;
    private string operador;
    private double resultado;
    private bool novaEntrada;

    public CalculadoraGui()
    {
        this.Text = "Calculadora";
        this.Size = new System.Drawing.Size(250, 350);
        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        display = new TextBox();
        display.ReadOnly = true;
        display.Size = new System.Drawing.Size(200, 30);
        display.Location = new System.Drawing.Point(10, 10);
        this.Controls.Add(display);

        //botões em geral

        buttons = new Button[17];
        string[] texts = { "7", "8", "9", "/",
                           "4", "5", "6", "*",
                           "1", "2", "3", "-",
                           "0", "C", "=", "+" ,
                           };

        for (int i = 0; i < 16; i++)
        {
            buttons[i] = new Button();
            buttons[i].Text = texts[i];
            buttons[i].Size = new System.Drawing.Size(45, 45);
            buttons[i].Location = new System.Drawing.Point(10 + (i % 4) * 50, 50 + (i / 4) * 50);
            buttons[i].Click += new EventHandler(Button_Click);
            this.Controls.Add(buttons[i]);
        }

        operador = "";
        resultado = 0;
        novaEntrada = true;
    }

    private void Button_Click(object sender, EventArgs e)
    {

        //botão de apagar tudo
        Button btn = sender as Button;

        if (char.IsDigit(btn.Text, 0))
        {
            if (novaEntrada)
            {
                display.Text = "";
                novaEntrada = false;
            }
            display.Text += btn.Text;
        }
        else if (btn.Text == "C")
        {
            display.Text = "";
            operador = "";
            resultado = 0;
            novaEntrada = true;
        }
        else if (btn.Text == "=")
        {
            Calcular();
            operador = "";
            novaEntrada = true;
        }
        else
        {
            if (!novaEntrada)
            {
                Calcular();
            }
            operador = btn.Text;
            resultado = double.Parse(display.Text);
            novaEntrada = true;
        }
    }

    private void Calcular()
    {

        //Botões de operação 
        double num = double.Parse(display.Text);

        switch (operador)
        {
            case "+":
                resultado += num;
                break;
            case "-":
                resultado -= num;
                break;
            case "*":
                resultado *= num;
                break;
            case "/":
                if (num != 0)
                    resultado /= num;
                else
                    MessageBox.Show("Erro: Divisão por zero");
                break;
            default:
                resultado = num;
                break;
        }

        display.Text = resultado.ToString();
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new CalculadoraGui());
    }
}