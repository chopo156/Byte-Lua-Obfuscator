using System;
using System.Windows.Forms;
using MoonSharp.Interpreter;

namespace lua_obfuscator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Log(string text)
        {
            richTextBox1.Invoke(new MethodInvoker(delegate () { richTextBox1.AppendText(text + "\r\n"); richTextBox1.ScrollToCaret(); }));
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

		void ObfuscateCode()
		{
			string scriptCode = @"    
				x = '';
				
				bin = x;
				local fb = '';
				local dec
				
				function compileScript()
					for i = 1, string.len(bin) do
						dec, _ = ('\\%3d'):format(bin:sub(i,i):byte()):gsub(' ', '0');
						fb = fb..dec;
					end
					
					return fb;
				end
			";

			Script script = new Script();
			script.DoString(scriptCode);

			script.Globals["x"] =  richTextBox2.Text;
			script.Globals["bin"] = script.Globals["x"];

			DynValue res = script.Call(script.Globals["compileScript"]);

			richTextBox1.Text = "load(\"" + res.String + "\")()";
		}

		private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
			ObfuscateCode();

		}
    }
}
