using System.Data;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        bool isTopActive = true;  // 위 박스 기본 선택
        double firstValue = 0;
        string op = "";
        bool opClicked = false; // 연산자 누른 후 숫자 새 입력 상태

        public Form1()
        {
            InitializeComponent();

            txtTop.Clear();
            txtBottom.Text = "";

            // 클릭 이벤트 연결
            txtTop.Click += txtTop_Click;
            txtBottom.Click += txtBottom_Click;

            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
                                 button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };
            foreach (var btn in buttons)
            {
                btn.Click += button_Click;
            }
        }

        private void txtTop_Click(object sender, EventArgs e)
        {
            isTopActive = true;
        }

        private void txtBottom_Click(object sender, EventArgs e)
        {
            isTopActive = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string t = btn.Text;

            // CE / C
            if (btn.Name == "button1" || btn.Name == "button2")
            {
                txtTop.Clear();
                txtBottom.Text = "0";
                firstValue = 0;
                op = "";
                opClicked = false;
                return;
            }

            // del → 전체 초기화
            if (btn.Name == "button3")
            {
                txtTop.Clear();
                txtBottom.Text = "0";
                firstValue = 0;
                op = "";
                opClicked = false;
                return;
            }

            // = 버튼
            if (btn.Name == "button20")
            {
                try
                {
                    string exp = txtTop.Text.Replace("x", "*").Replace("÷", "/");
                    var result = new DataTable().Compute(exp, null);
                    txtTop.Text += " = " + result.ToString();
                    txtBottom.Text = result.ToString();
                }
                catch
                {
                    txtTop.Text = "Error";
                    txtBottom.Text = "0";
                }
                firstValue = 0;
                op = "";
                opClicked = false;
                return;
            }

            // 연산자 버튼
            if (t == "+" || t == "-" || t == "x" || t == "÷")
            {
                txtTop.Text += " " + t + " ";
                op = t;
                firstValue = double.Parse(txtBottom.Text);  // 아래 계산용 초기 값
                opClicked = true;
                return;
            }

            // 숫자 및 . 버튼
            if (double.TryParse(t, out double num) || t == ".")
            {
                // 아래 박스 새 숫자 입력 시 초기화
                if (opClicked || txtBottom.Text == "0")
                {
                    txtBottom.Text = t;
                    opClicked = false;
                }
                else
                {
                    txtBottom.Text += t;
                }

                // 위 박스는 항상 누적 표시
                txtTop.Text += t;
            }
        }
    }
}
