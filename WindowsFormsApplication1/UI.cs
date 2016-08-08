using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;
using ClassLibrary1.Instruments;
using System.Windows.Forms.DataVisualization.Charting;
using OMLib.Conventions;
namespace WindowsFormsApplication1
{
    public partial class UI : Form
    {

        public UI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Payment scheme
            double initialNotional = Convert.ToDouble(tb_notional.Text);
            bool isScopeSpread = String.IsNullOrWhiteSpace(tb_spread.Text); // text for spread textbox
            bool isScopeUpfront = String.IsNullOrWhiteSpace(tb_quotedupfront.Text);
            double upfront = 0.0;
            double spread = 0.0;

            if (isScopeSpread == false)
            {
                spread = Convert.ToDouble(tb_spread.Text) / 10000;

            }
            if (isScopeUpfront == false)
            {
                upfront = Convert.ToDouble(tb_quotedupfront.Text) / 100;
            }
            //The trade level, if spread not equal coupon rate, then upfront fee is paid
            double coupon_rate = Convert.ToDouble(tb_fixrate.Text) / 10000; //The coupon paid by premium buyer
            string frequency = cmb_frequency.Text;//   LB_frequency.Text;
            Int16 settle = Convert.ToInt16(tb_cashsettle.Text);


            //Key dates
            DateTime Maturity = Convert.ToDateTime(tb_maturity.Text);
            DateTime firstpayday = Convert.ToDateTime(tb_firstpayday.Text); //the first premium payment date
            DateTime formerpayday = Convert.ToDateTime(tb_formerpayday.Text); //the first premium payment date
            DateTime tradedate = Convert.ToDateTime(tb_tradeDate.Text); //the first premium payment date

            //Data to construct yield curve and credit curve
            double RecoveryRate = Convert.ToDouble(tb_recovery.Text);
            //Running spread of comparable products, assuming zero entry fee and same payment schema as the current product
            List<double> QuotedSpread = new List<double> { Convert.ToDouble(qs_6M.Text), Convert.ToDouble(qs_1Y.Text), Convert.ToDouble(qs_3Y.Text)
                        ,Convert.ToDouble(qs_5Y.Text),Convert.ToDouble(qs_7Y.Text),Convert.ToDouble(qs_10Y.Text)};
            //Benchmark interest rates, consisting of Libor, Deposit and SWAP
            List<double> QuotedSpot = new List<double> {Convert.ToDouble(ir_1M.Text), Convert.ToDouble(ir_2M.Text), Convert.ToDouble(ir_3M.Text),
                Convert.ToDouble(ir_6M.Text),Convert.ToDouble(tb_9M.Text), Convert.ToDouble(ir_1Y.Text),
                Convert.ToDouble(ir_2Y.Text),Convert.ToDouble(ir_3Y.Text),Convert.ToDouble(ir_4Y.Text), Convert.ToDouble(ir_5Y.Text),Convert.ToDouble(ir_6Y.Text),
                Convert.ToDouble(ir_7Y.Text),Convert.ToDouble(ir_8Y.Text),Convert.ToDouble(ir_9Y.Text),Convert.ToDouble(ir_10Y.Text),Convert.ToDouble(ir_11Y.Text),Convert.ToDouble(ir_12Y.Text),
                Convert.ToDouble(ir_15Y.Text),Convert.ToDouble(ir_20Y.Text),Convert.ToDouble(ir_25Y.Text),Convert.ToDouble(ir_30Y.Text)};

            // decide which scope we should use for computation

            String usecreditcurve = Convert.ToString(tb_usecreditcurve.Checked);

            switch (usecreditcurve)
            {
                case "True":
                    //Use credit curve predefined
                    break;
                case "False":
                    QuotedSpread = null;
                    break;
                default:
                    break;
            }


            //Input & Output
            //Case 1: Input: coupon rate, credit curve, standard contract
            //      Output: upfront
            //Case 2: Input: coupon rate, upfront
            //      Output: spread, net amount paid
            //Case 2: Input: coupon rate, upfront
            //      Output: upfront, net amount paid
            CDS cds = new CDS( coupon_rate,  initialNotional, Maturity, firstpayday, tradedate,
            formerpayday, frequency, RecoveryRate, settle,3);
            cds.Curve_Building(QuotedSpot, QuotedSpread, spread, upfront);
            cds.Pricing();

            dataGridView_fix.DataSource = cds.FixLeg;
            lb_zeroRates.DataSource = cds.zero_rates;
            
            accrued.Text = Convert.ToString(cds.accruedday);
            accruedamt.Text = Convert.ToString(cds.accruedamt);

            tb_upfront.Text = Convert.ToString(cds.pv / initialNotional * 100);
            market_value.Text = Convert.ToString(cds.pv);
            clean_price.Text = Convert.ToString((1 - cds.pv / initialNotional) * 100);
            //chart1.DataSource= cds.yield_series;
            this.chart2.Palette = ChartColorPalette.SeaGreen;
            // Set title.
            //this.chart1.Titles.Add("Pets");

            // Add series.
            for (int i = 1; i < cds.survival_prob.Count; i++)
            {
                // Add series.
                Series series = this.chart2.Series.Add(i.ToString());

                // Add point.
                series.Points.AddXY(i, cds.survival_prob[i]);
            }
            
           
            // Set palette.
            this.chart1.Palette = ChartColorPalette.SeaGreen;
            // Add series.
            for (int i = 1; i < cds.yield_series.Count; i++)
            {
                // Add series.
                Series series1 = this.chart1.Series.Add(i.ToString());

                // Add point.
                series1.Points.AddXY(i, cds.yield_series[i]);
            }
            //Form2 frm = new Form2();


        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void qs_1Y_TextChanged(object sender, EventArgs e)
        {

        }

        private void qs_2Y_TextChanged(object sender, EventArgs e)
        {

        }

        private void qs_6M_TextChanged(object sender, EventArgs e)
        {

        }

        private void qs_7Y_TextChanged(object sender, EventArgs e)
        {

        }

        private void qs_10Y_TextChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void ir_1Y_TextChanged(object sender, EventArgs e)
        {

        }

        private void ir_1Y_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label57_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void tb_upfront_TextChanged(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void clean_price_TextChanged(object sender, EventArgs e)
        {

        }

        private void market_value_TextChanged(object sender, EventArgs e)
        {

        }

        private void accrued_TextChanged(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void accruedamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void ir_2M_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load_2(object sender, EventArgs e)
        {

        }

    }
}
